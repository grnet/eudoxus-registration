using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal
{
    public partial class Default : BaseEntityPortalPage<Reporter>
    {
        protected override void Fill()
        {
            Entity = new ReporterRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Page.User.Identity.IsAuthenticated)
            {
                if (Entity.MustChangePassword)
                {
                    Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                }

                if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.PublisherUser))
                {
                    Response.Redirect("~/Secure/Publishers/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SecretaryUser))
                {
                    Response.Redirect("~/Secure/Secretaries/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.DistributionPointUser))
                {
                    Response.Redirect("~/Secure/DistributionPoints/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.PublicationsOfficeUser))
                {
                    Response.Redirect("~/Secure/PublicationsOffices/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.DataCenterUser))
                {
                    Response.Redirect("~/Secure/DataCenters/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.LibraryUser))
                {
                    Response.Redirect("~/Secure/Libraries/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.BookSupplierUser))
                {
                    Response.Redirect("~/Secure/BookSuppliers/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.PricingCommitteeUser))
                {
                    Response.Redirect("~/Secure/PricingCommittees/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.MinistryPaymentsUser))
                {
                    Response.Redirect("~/Secure/MinistryPayments/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.Helpdesk) ||
                         Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SuperHelpdesk) ||
                         Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.PublisherCommunicator) ||
                         Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SecretaryCommunicator) ||
                         Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.Supervisor))
                {
                    Response.Redirect("~/Helpdesk/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.Reports) ||
                        Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SuperReports))
                {
                    Response.Redirect("~/Reports/Default.aspx", true);
                }
                else if (Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SystemAdministrator))
                {
                    Response.Redirect("~/Admin/Default.aspx", true);
                }
                else
                {
                    Response.Redirect("~/Common/AccessDenied.aspx", true);
                }
            }

            LinkButton loginButton = login1.FindControl("LoginButton") as LinkButton;
            TextBox txtUserName = login1.FindControl("UserName") as TextBox;
            TextBox txtPassword = login1.FindControl("Password") as TextBox;

            txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(loginButton, string.Empty));
            txtPassword.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(loginButton, string.Empty));

            Form.DefaultButton = loginButton.UniqueID;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            TextBox txtUserName = login1.FindControl("UserName") as TextBox;
            TextBox txtPassword = login1.FindControl("Password") as TextBox;

            txtUserName.Focus();
            txtPassword.Text = "";
        }

        protected void login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            MembershipUser user = Membership.GetUser(login1.UserName);

            if (user != null && user.IsLockedOut)
            {
                login1.FailureText = "Ο χρήστης είναι κλειδωμένος. Αν δεν θυμάστε τον κωδικό πρόσβασης μπορείτε να ζητήσετε υπενθύμιση κωδικού, αλλιώς μπορείτε να επικοινωνήσετε με το Γραφείο Αρωγής.";
            }
        }
    }
}
