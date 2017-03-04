using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Secure.BookSuppliers
{
    public partial class ContactForm : BaseEntityPortalPage<Reporter>
    {
        protected override void Fill()
        {
            Entity = (Reporter)new BookSupplierRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack)
            {
                ucContactFormInput.SetContactForm(Entity);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            IncidentReport ir = new IncidentReport();

            ucContactFormInput.FillContactForm(ir);

            ir.Reporter = Entity;

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
