using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.BookSuppliers
{
    public partial class BookSuppliers : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a BookSupplier's user
            if (Roles.IsUserInRole(RoleNames.BookSupplierUser))
            {
                BookSupplier publicationOffice = new BookSupplierRepository().FindByUsername(Page.User.Identity.Name);

                if (publicationOffice != null)
                {
                    if (publicationOffice.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    var institution = CacheManager.Institutions.Get(publicationOffice.InstitutionID);

                    lblBookSupplierName.Text = institution.Name;
                }
            }
            else
            {
                lblBookSupplierName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
