using System;

using System.Web;
using System.Web.Security;
using System.Linq;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class Helpdesk : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Secretary user
            if (Roles.IsUserInRole(RoleNames.Helpdesk) || Roles.IsUserInRole(RoleNames.SuperHelpdesk))
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
            LoginStatus1.LogoutPageUrl = Server.MapPath("~/Helpdesk/Default.aspx");
        }
    }
}