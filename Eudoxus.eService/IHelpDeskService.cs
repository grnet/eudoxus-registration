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
    public interface IHelpDeskService
    {
        [WebInvoke(UriTemplate = "rest/helpdesk/publisher", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void Update(PublisherDto publisher);

        [WebInvoke(UriTemplate = "rest/helpdesk/secretariat", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdateSecretary(SecretaryDto secretary);

        [WebInvoke(UriTemplate = "rest/helpdesk/distributionpoint", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdateDistributionPoint(DistributionPointDto distributionPoint);

        [WebInvoke(UriTemplate = "rest/helpdesk/publicationsoffice", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdatePublicationsOffice(PublicationsOfficeDto publicationsOffice);

        [WebInvoke(UriTemplate = "rest/helpdesk/datacenter", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdateDataCenter(DataCenterDto dataCenter);

        [WebInvoke(UriTemplate = "rest/helpdesk/library", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdateLibrary(LibraryDto library);

        [WebInvoke(UriTemplate = "rest/helpdesk/booksupplier", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdateBookSupplier(BookSupplierDto bookSupplier);

        [WebInvoke(UriTemplate = "rest/helpdesk/pricingcommittee", RequestFormat = WebMessageFormat.Xml, Method = "PUT")]
        void UpdatePricingCommittee(PricingCommitteeDto pricingCommittee);
    }
}
