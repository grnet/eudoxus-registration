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
    public class ServiceClient : ClientBase<IHelpDeskService>
    {
        public static void Update(PublisherDto publisher)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(publisher);
        }

        public static void Update(SecretaryDto secretary)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(secretary);
        }

        public static void Update(DistributionPointDto distributionPoint)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(distributionPoint);
        }

        public static void Update(PublicationsOfficeDto publicationsOffice)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(publicationsOffice);
        }

        public static void Update(DataCenterDto dataCenter)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(dataCenter);
        }

        public static void Update(LibraryDto library)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(library);
        }

        public static void Update(BookSupplierDto bookSupplier)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(bookSupplier);
        }

        public static void Update(PricingCommitteeDto pricingCommittee)
        {
            ServiceClient sc = new ServiceClient();
            sc.PrivateUpdate(pricingCommittee);
        }

        private ServiceClient() : base("endService") { }

        private void PrivateUpdate(PublisherDto publisher)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.Update(publisher);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(publisher.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), publisher);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was updated successfully,\nData:{1}", publisher.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("Publisher with ID {0} was created successfully,\nData:{1}", publisher.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(SecretaryDto secretary)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdateSecretary(secretary);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(secretary.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), secretary);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("Secretary with ID {0} was updated successfully,\nData:{1}", secretary.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("Secretary with ID {0} was created successfully,\nData:{1}", secretary.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(DistributionPointDto distributionPoint)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdateDistributionPoint(distributionPoint);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(distributionPoint.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), distributionPoint);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("DistributionPoint with ID {0} was updated successfully,\nData:{1}", distributionPoint.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("DistributionPoint with ID {0} was created successfully,\nData:{1}", distributionPoint.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(PublicationsOfficeDto publicationsOffice)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdatePublicationsOffice(publicationsOffice);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(publicationsOffice.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), publicationsOffice);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("PublicationsOffice with ID {0} was updated successfully,\nData:{1}", publicationsOffice.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("PublicationsOffice with ID {0} was created successfully,\nData:{1}", publicationsOffice.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(DataCenterDto dataCenter)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdateDataCenter(dataCenter);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(dataCenter.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), dataCenter);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("DataCenter with ID {0} was updated successfully,\nData:{1}", dataCenter.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("DataCenter with ID {0} was created successfully,\nData:{1}", dataCenter.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(LibraryDto library)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdateLibrary(library);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(library.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), library);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("Library with ID {0} was updated successfully,\nData:{1}", library.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("Library with ID {0} was created successfully,\nData:{1}", library.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(BookSupplierDto bookSupplier)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdateBookSupplier(bookSupplier);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(bookSupplier.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), bookSupplier);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("BookSupplier with ID {0} was updated successfully,\nData:{1}", bookSupplier.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("BookSupplier with ID {0} was created successfully,\nData:{1}", bookSupplier.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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

        private void PrivateUpdate(PricingCommitteeDto pricingCommittee)
        {
            try
            {
                Open();
                var proxy = CreateChannel();
                using (new OperationContextScope((IClientChannel)proxy))
                {
                    proxy.UpdatePricingCommittee(pricingCommittee);
                    var statusCode = WebOperationContext.Current.IncomingResponse.StatusCode;
                    StringBuilder sb = new StringBuilder();
                    new XmlSerializer(pricingCommittee.GetType()).Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), pricingCommittee);
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            LogHelper.LogMessage(string.Format("PricingCommittee with ID {0} was updated successfully,\nData:{1}", pricingCommittee.ID, sb.ToString()), this);
                            break;
                        case HttpStatusCode.Created:
                            LogHelper.LogMessage(string.Format("PricingCommittee with ID {0} was created successfully,\nData:{1}", pricingCommittee.ID, sb.ToString()), this);
                            break;
                        default:
                            //LogHelper.LogMessage(string.Format("Service return unexpected code {0}:{1}.", (int)statusCode, statusCode.ToString()), this);
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
