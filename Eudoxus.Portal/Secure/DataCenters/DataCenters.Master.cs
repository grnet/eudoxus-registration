using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.DataCenters
{
    public partial class DataCenters : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a DataCenter's user
            if (Roles.IsUserInRole(RoleNames.DataCenterUser))
            {
                DataCenter publicationOffice = new DataCenterRepository().FindByUsername(Page.User.Identity.Name);

                if (publicationOffice != null)
                {
                    if (publicationOffice.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    var institution = CacheManager.Institutions.Get(publicationOffice.InstitutionID);

                    lblDataCenterName.Text = institution.Name;
                }
            }
            else
            {
                lblDataCenterName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
