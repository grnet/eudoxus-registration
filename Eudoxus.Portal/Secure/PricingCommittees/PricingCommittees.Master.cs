using System;
using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.PricingCommittees
{
    public partial class PricingCommittees : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Library's user
            if (Roles.IsUserInRole(RoleNames.PricingCommitteeUser))
            {
                PricingCommittee pricingCommittee = new PricingCommitteeRepository().FindByUsername(Page.User.Identity.Name);

                if (pricingCommittee != null)
                {
                    if (pricingCommittee.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    lblContactName.Text = pricingCommittee.ContactName;
                }
            }
            else
            {
                lblContactName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
