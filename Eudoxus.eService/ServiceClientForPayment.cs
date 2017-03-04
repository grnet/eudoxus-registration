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

namespace Eudoxus.eService
{
    public class ServiceClientForPayment : ClientBase<IHelpDeskServiceForPayment>
    {
        public static void Update(PublisherDto publisher)
        {
            ServiceClientForPayment sc = new ServiceClientForPayment();
            sc.PrivateUpdate(publisher);
        }

        private ServiceClientForPayment() : base("endServiceForPayment") { }

        private void PrivateUpdate(PublisherDto publisher)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdatePublisher(publisher);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(publisher.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), publisher);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.NoContent:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was updated successfully,\nData:{1}", publisher.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was created successfully,\nData:{1}", publisher.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Forbidden:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was not updated at the payment database,\nData:{1}", publisher.ID, sb.ToString()), this);
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
            finally
            {
                Close();
            }
        }
    }
}
