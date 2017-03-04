using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Net;

namespace Eudoxus.eService
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IHelpDeskServiceForPayment
    {
        [WebInvoke(UriTemplate = "api/publisher/", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdatePublisher(PublisherDto publisher);
    }
}
