using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Eudoxus.BusinessModel;


namespace Eudoxus.Portal.Common
{
    public partial class ActivateUser : System.Web.UI.Page
    {
        private static string GetMsg(string Key)
        {
            return (string)HttpContext.GetGlobalResourceObject("ActivateUser", Key);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(id))
            {
                lblMessage.Text = GetMsg("InvalidUrl");
            }
            else
            {
                Guid guid = Guid.Empty;
                try
                {
                    guid = new Guid(id);
                }
                catch (Exception) { }

                if (guid == Guid.Empty)
                {
                    lblMessage.Text = GetMsg("InvalidUrl");
                }
                else
                {
                    if (User.Identity.IsAuthenticated)
                        FormsAuthentication.SignOut();

                    MembershipUser u = Membership.GetUser(guid);
                    enReporterType type = new ReporterRepository().FindReporterTypeByUsername(u.UserName);
                    ActivateUserResult r = ActivateUserResult.UserNotFound;
                    switch (type)
                    {
                        case enReporterType.Publisher:
                            r = ActivateHelper.Activate<Publisher>(u);
                            break;
                        case enReporterType.Secretary:
                            r = ActivateHelper.Activate<Secretary>(u);
                            break;
                        case enReporterType.DistributionPoint:
                            r = ActivateHelper.Activate<DistributionPoint>(u);
                            break;
                        case enReporterType.PublicationsOffice:
                            r = ActivateHelper.Activate<PublicationsOffice>(u);
                            break;
                        case enReporterType.DataCenter:
                            r = ActivateHelper.Activate<DataCenter>(u);
                            break;
                        case enReporterType.Library:
                            r = ActivateHelper.Activate<Library>(u);
                            break;
                        case enReporterType.BookSupplier:
                            r = ActivateHelper.Activate<BookSupplier>(u);
                            break;
                        case enReporterType.PricingCommittee:
                            r = ActivateHelper.Activate<PricingCommittee>(u);
                            break;
                        case enReporterType.MinistryPayments:
                            r = ActivateHelper.Activate<MinistryPaymentsUser>(u);
                            break;
                    }

                    lblMessage.Text = GetMsg(r.ToString());
                }
            }
        }
    }
}
