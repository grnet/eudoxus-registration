using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if an Admin user
            if (Roles.IsUserInRole(RoleNames.SystemAdministrator))
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

            lnkChangePassword.Attributes["onclick"] = string.Format("popUp.show('../Common/ChangePassword.aspx?r=true&username={0}','Αλλαγή Κωδικού Πρόσβασης');", Page.User.Identity.Name);
        }

        protected bool ShowNode(SiteMapNode node)
        {
            if (node.Roles.Count == 0)
                return true;
            foreach (string r in Roles.GetRolesForUser())
            {
                if (node.Roles.Cast<string>().Contains(r, StringComparer.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            LoginStatus1.LogoutPageUrl = Server.MapPath("~/Admin/Default.aspx");
        }
    }
}