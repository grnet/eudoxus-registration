using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.Secretaries
{
    public partial class Secretaries : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Publisher's user
            if (Roles.IsUserInRole(RoleNames.SecretaryUser))
            {
                Secretary secretary = new SecretaryRepository().FindByUsername(Page.User.Identity.Name);

                if (secretary != null)
                {
                    if (secretary.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    var academic = CacheManager.Academics.Get(secretary.AcademicID);

                    if (string.IsNullOrEmpty(academic.School))
                    {
                        lblAcademicName.Text = academic.Institution + "<br/>" + academic.Department;
                    }
                    else if (string.IsNullOrEmpty(academic.Department))
                    {
                        lblAcademicName.Text = academic.Institution + "<br/>" + academic.School;
                    }
                    else
                    {
                        lblAcademicName.Text = academic.Institution + "<br/>" + academic.School + "<br/>" + academic.Department;
                    }
                }
            }
            else
            {
                lblAcademicName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
