using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class Reports : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Secretary user
            if (Roles.IsUserInRole(RoleNames.Reports) || Roles.IsUserInRole(RoleNames.SuperReports))
            {
                Reporter reporter = new ReporterRepository().FindByUsername(Page.User.Identity.Name);

                if (reporter != null)
                {
                    if (reporter.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }
                }
            }

            lnkChangePassword.Attributes["onclick"] = string.Format("popUp.show('../Common/ChangePassword.aspx?r=true&username={0}','');", Page.User.Identity.Name);
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            LoginStatus1.LogoutPageUrl = Server.MapPath("~/Reports/Default.aspx");
        }
    }
}