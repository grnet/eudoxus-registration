using System;

using System.Web.Security;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Secure.Publishers
{
    public partial class Publishers : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user if a Publisher's user
            if (Roles.IsUserInRole(RoleNames.PublisherUser))
            {
                Publisher publisher = new PublisherRepository().FindByUsername(Page.User.Identity.Name);

                if (publisher != null)
                {
                    if (publisher.MustChangePassword)
                    {
                        Response.Redirect("~/Common/ChangeDefaultPassword.aspx");
                    }

                    lblPublisherName.Text = publisher.PublisherName;
                }
            }
            else
            {
                lblPublisherName.Visible = false;
            }
        }

        protected string Separator(int ItemIndex)
        {
            return "";
        }
    }
}
