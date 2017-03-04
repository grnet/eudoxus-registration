using System;
using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.MinistryPayments
{
    public partial class MinistryPayments : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Library's user
            if (Roles.IsUserInRole(RoleNames.MinistryPaymentsUser))
            {
                MinistryPaymentsUser MinistryPaymentsUser = new MinistryPaymentsUserRepository().FindByUsername(Page.User.Identity.Name);

                if (MinistryPaymentsUser != null)
                {
                    if (MinistryPaymentsUser.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    lblContactName.Text = MinistryPaymentsUser.ContactName;
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
