using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class EditBookSupplier : BaseEntityPortalPage<BookSupplier>
    {
        protected override void Fill()
        {
            Entity = new BookSupplierRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.BookSupplierDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucBookSupplierInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucBookSupplierInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            int oldInstitutionID = (int)Entity.InstitutionReference.GetKey();

            ucBookSupplierInput.Fill();

            int newInstitutionID = (int)Entity.InstitutionReference.GetKey();

            if (oldInstitutionID != newInstitutionID)
            {
                List<BookSupplier> notVerifiedBookSuppliers = new BookSupplierRepository(UnitOfWork).FindBookSuppliersByVerificationStatus(newInstitutionID, enVerificationStatus.NotVerified);
                List<BookSupplier> cannotBeVerifiedBookSuppliers = new BookSupplierRepository(UnitOfWork).FindBookSuppliersByVerificationStatus(oldInstitutionID, enVerificationStatus.CannotBeVerified);

                foreach (BookSupplier bookSupplier in notVerifiedBookSuppliers)
                {
                    if (bookSupplier.ID != Entity.ID)
                    {
                        bookSupplier.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(bookSupplier.ID);
                    }
                }

                foreach (BookSupplier bookSupplier in cannotBeVerifiedBookSuppliers)
                {
                    if (bookSupplier.ID != Entity.ID)
                    {
                        bookSupplier.VerificationStatus = enVerificationStatus.NotVerified;
                        updatedIDs.Add(bookSupplier.ID);
                    }
                }
            }

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendBookSupplierUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}