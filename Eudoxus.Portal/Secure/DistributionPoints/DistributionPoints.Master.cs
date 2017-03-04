using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.DistributionPoints
{
    public partial class DistributionPoints : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a DistributionPoint's user
            if (Roles.IsUserInRole(RoleNames.DistributionPointUser))
            {
                DistributionPoint distributionPoint = new DistributionPointRepository().FindByUsername(Page.User.Identity.Name);

                if (distributionPoint != null)
                {
                    if (distributionPoint.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    lblDistributionPointName.Text = distributionPoint.DistributionPointName;
                }
            }
            else
            {
                lblDistributionPointName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
