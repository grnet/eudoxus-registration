using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using System.Web.Security;

namespace Eudoxus.Portal.Admin
{
    public partial class EditAdminUser : BaseEntityPortalPage<AdminUser>
    {
        protected override void Fill()
        {
            Entity = new AdminUserRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["sID"]));
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUsername.Text = Entity.UserName;
                txtEmail.Text = Entity.ContactEmail;
                txtContactName.Text = Entity.ContactName;
                txtContactMobilePhone.Text = Entity.ContactMobilePhone;

                if (Roles.IsUserInRole(Entity.UserName, RoleNames.SuperHelpdesk))
                {
                    chbxHelpdesk.Checked = true;
                }

                if (Roles.IsUserInRole(Entity.UserName, RoleNames.Reports))
                {
                    chbxReports.Checked = true;
                }
            }

            base.OnPreRender(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Attributes["onblur"] = "RemoveTags(this)";
            txtEmail.Attributes["onblur"] = "RemoveTags(this)";
            txtContactName.Attributes["onkeyup"] = "StudentCard.Lib.ToUpperForNames(this)";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string oldEmail = Entity.Email;

            if (Entity.Email != txtEmail.Text.ToNull())
                Entity.Email = Entity.ContactEmail = txtEmail.Text.ToNull();

            if (Entity.ContactName != txtContactName.Text.ToNull())
                Entity.ContactName = txtContactName.Text.ToNull();

            if (Entity.ContactMobilePhone != txtContactMobilePhone.Text.ToNull())
                Entity.ContactMobilePhone = txtContactMobilePhone.Text.ToNull();

            if (chbxHelpdesk.Checked)
            {
                if (!Roles.IsUserInRole(Entity.UserName, RoleNames.SuperHelpdesk))
                {
                    Roles.AddUserToRole(Entity.UserName, RoleNames.SuperHelpdesk);
                }
            }
            else
            {
                if (Roles.IsUserInRole(Entity.UserName, RoleNames.SuperHelpdesk))
                {
                    Roles.RemoveUserFromRole(Entity.UserName, RoleNames.SuperHelpdesk);
                }
            }

            if (chbxReports.Checked)
            {
                if (!Roles.IsUserInRole(Entity.UserName, RoleNames.Reports))
                {
                    Roles.AddUserToRole(Entity.UserName, RoleNames.Reports);
                }
            }
            else
            {
                if (Roles.IsUserInRole(Entity.UserName, RoleNames.Reports))
                {
                    Roles.RemoveUserFromRole(Entity.UserName, RoleNames.Reports);
                }
            }

            string newEmail = txtEmail.Text.ToNull();

            if (oldEmail != newEmail && !string.IsNullOrEmpty(Membership.GetUserNameByEmail(newEmail)))
            {
                lblValidationErrors.Text = "Το E-mail χρησιμοποιείται ήδη από κάποιο άλλο χρήστη του Πληροφοριακού Συστήματος. Παρακαλούμε επιλέξτε κάποιο άλλο.";
                return;
            }

            UnitOfWork.Commit();

            MembershipUser mu = Membership.GetUser(Entity.UserName);
            if (oldEmail != newEmail)
            {
                mu.Email = newEmail;
                Membership.UpdateUser(mu);
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
