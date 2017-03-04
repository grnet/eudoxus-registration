using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using System.Web.Security;
using Eudoxus.Utils;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Admin
{
    public partial class AddAdminUser : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Attributes["onblur"] = "RemoveTags(this)";
            txtPassword1.Attributes["onblur"] = "RemoveTags(this)";
            txtPassword2.Attributes["onblur"] = "RemoveTags(this)";
            txtEmail.Attributes["onblur"] = "RemoveTags(this)";
            txtContactName.Attributes["onkeyup"] = "StudentCard.Lib.ToUpperForNames(this)";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            AdminUser reporter = new AdminUser();
            
            reporter.ContactName = txtContactName.Text.ToNull();
            reporter.ContactMobilePhone = txtContactMobilePhone.Text.ToNull();
            reporter.IsApproved = true;

            string username = txtUsername.Text.ToNull();
            string password = txtPassword1.Text.ToNull();
            string email = txtEmail.Text.ToNull();

            reporter.UserName = username;
            reporter.Email = reporter.ContactEmail = email;

            if (Membership.GetUser(username) != null)
            {
                lblValidationErrors.Text = "Το Όνομα Χρήστη χρησιμοποιείται ήδη από κάποιο άλλο χρήστη του Πληροφοριακού Συστήματος. Παρακαλούμε επιλέξτε κάποιο άλλο.";
                return;
            }

            if (!string.IsNullOrEmpty(Membership.GetUserNameByEmail(email)))
            {
                lblValidationErrors.Text = "Το E-mail χρησιμοποιείται ήδη από κάποιο άλλο χρήστη του Πληροφοριακού Συστήματος. Παρακαλούμε επιλέξτε κάποιο άλλο.";
                return;
            }

            MembershipUser mu;

            try
            {
                MembershipCreateStatus status;
                mu = Membership.CreateUser(username, password, email, null, null, true, out status);

                if (mu == null)
                    throw new MembershipCreateUserException(status);
            }
            catch (MembershipCreateUserException ex)
            {
                LogHelper.LogError<AddAdminUser>(ex, this, string.Format("Error while creating User with username: {0}", username));
            }

            reporter.MustChangePassword = true;

            try
            {
                UnitOfWork.MarkAsNew(reporter);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                Membership.DeleteUser(username);
                throw;
            }

            UnitOfWork.Commit();

            if (chbxHelpdesk.Checked)
            {
                Roles.AddUserToRole(reporter.UserName, RoleNames.SuperHelpdesk);
            }

            if (chbxReports.Checked)
            {
                Roles.AddUserToRole(reporter.UserName, RoleNames.Reports);
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}