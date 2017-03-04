using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.Libraries
{
    public partial class Libraries : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Library's user
            if (Roles.IsUserInRole(RoleNames.LibraryUser))
            {
                Library library = new LibraryRepository().FindByUsername(Page.User.Identity.Name);

                if (library != null)
                {
                    if (library.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    var institution = CacheManager.Institutions.Get(library.InstitutionID);

                    lblLibraryName.Text = institution.Name + " - " + library.LibraryName;
                }
            }
            else
            {
                lblLibraryName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
