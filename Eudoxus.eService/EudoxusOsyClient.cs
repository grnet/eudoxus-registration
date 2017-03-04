using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Net;
using System.Web;
using Eudoxus.Utils;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Web.Script.Serialization;

namespace Eudoxus.eService
{
    public class EudoxusOsyClient
    {
        private static string eudoxusOsyServiceUrl = string.Empty;

        static EudoxusOsyClient()
        {
            eudoxusOsyServiceUrl = ConfigurationManager.AppSettings["EudoxusOsyServiceURL"];
        }

        public static void Update(PublisherEudoxusOsyDto publisher)
        {
            EudoxusOsyClient sc = new EudoxusOsyClient();
            sc.PrivateUpdate(publisher);
        }

        public static void Update(MinistryPaymentsUserDto ministryUser)
        {
            EudoxusOsyClient sc = new EudoxusOsyClient();
            sc.PrivateUpdate(ministryUser);
        }

        private void PrivateUpdate(PublisherEudoxusOsyDto publisher)
        {
            var endpoint = eudoxusOsyServiceUrl + "/SyncPublisher";
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(new JavaScriptSerializer().Serialize(publisher));
                requestWriter.Close();

                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var data = reader.ReadToEnd();
                    var responseData = new JavaScriptSerializer().Deserialize<EudoxusOsySyncResponse>(data);
                    var statusCode = responseData.StatusCode;

                    switch ((enEudoxusOsyStatusCode)statusCode)
                    {
                        case enEudoxusOsyStatusCode.SupplierCreated:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was created successfully,\nData:{1}", publisher.PublisherKpsID, publisher.ToString()), this);
                            break;
                        case enEudoxusOsyStatusCode.SupplierUpdated:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was updated successfully,\nData:{1}", publisher.PublisherKpsID, publisher.ToString()), this);
                            break;
                        case enEudoxusOsyStatusCode.Errors:
                            LogHelper.LogError(new Exception(), this, string.Format("Publisher with ID {0} was not updated at the eudoxusOsy database,\nErrorMessage:{1}", publisher.PublisherKpsID, responseData.StatusMessage));
                            break;
                        default:
                            throw new InvalidOperationException(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is WebException)
                {
                    var webEx = (System.Net.WebException)ex.InnerException;
                    if (webEx.Response != null)
                    {
                        string msg = HttpUtility.HtmlDecode(new System.IO.StreamReader(webEx.Response.GetResponseStream()).ReadToEnd());
                        string stripptedMsg = Regex.Replace(msg, @"<(.|\n)*?>", string.Empty);
                        LogHelper.LogError(ex, this, string.Format("Service Error: {0} ", stripptedMsg));
                        throw new Exception(string.Format("Service Error: {0} ", stripptedMsg), ex);
                    }
                }
                LogHelper.LogError(ex, this);
                throw;
            }
        }

        private void PrivateUpdate(MinistryPaymentsUserDto ministryUser)
        {
            var endpoint = eudoxusOsyServiceUrl + "/SyncMinistryPaymentsUser";
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(new JavaScriptSerializer().Serialize(ministryUser));
                requestWriter.Close();

                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var data = reader.ReadToEnd();
                    var responseData = new JavaScriptSerializer().Deserialize<EudoxusOsySyncResponse>(data);
                    var statusCode = responseData.StatusCode;

                    switch ((enEudoxusOsyStatusCode)statusCode)
                    {
                        case enEudoxusOsyStatusCode.SupplierCreated:
                            LogHelper.LogMessage(string.Format("MinistryPaymentsUser with ID {0} was created successfully,\nData:{1}", ministryUser.ID, ministryUser.ToString()), this);
                            break;
                        case enEudoxusOsyStatusCode.SupplierUpdated:
                            LogHelper.LogMessage(string.Format("MinistryPaymentsUser with ID {0} was updated successfully,\nData:{1}", ministryUser.ID, ministryUser.ToString()), this);
                            break;
                        case enEudoxusOsyStatusCode.Errors:
                            LogHelper.LogError(new Exception(), this, string.Format("MinistryPaymentsUser with ID {0} was not updated at the eudoxusOsy database,\nErrorMessage:{1}", ministryUser.ID, responseData.StatusMessage));
                            break;
                        default:
                            throw new InvalidOperationException(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is WebException)
                {
                    var webEx = (System.Net.WebException)ex.InnerException;
                    if (webEx.Response != null)
                    {
                        string msg = HttpUtility.HtmlDecode(new System.IO.StreamReader(webEx.Response.GetResponseStream()).ReadToEnd());
                        string stripptedMsg = Regex.Replace(msg, @"<(.|\n)*?>", string.Empty);
                        LogHelper.LogError(ex, this, string.Format("Service Error: {0} ", stripptedMsg));
                        throw new Exception(string.Format("Service Error: {0} ", stripptedMsg), ex);
                    }
                }
                LogHelper.LogError(ex, this);
                throw;
            }
        }
    }
}
