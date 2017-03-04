using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.PublicationsOffices
{
    public partial class PublicationsOffices : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a PublicationsOffice's user
            if (Roles.IsUserInRole(RoleNames.PublicationsOfficeUser))
            {
                PublicationsOffice publicationOffice = new PublicationsOfficeRepository().FindByUsername(Page.User.Identity.Name);

                if (publicationOffice != null)
                {
                    if (publicationOffice.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    var institution = CacheManager.Institutions.Get(publicationOffice.InstitutionID);

                    lblPublicationsOfficeName.Text = institution.Name;
                }
            }
            else
            {
                lblPublicationsOfficeName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
