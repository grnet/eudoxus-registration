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
    public partial class EditPublicationsOffice : BaseEntityPortalPage<PublicationsOffice>
    {
        protected override void Fill()
        {
            Entity = new PublicationsOfficeRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.PublicationsOfficeDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucPublicationsOfficeInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucPublicationsOfficeInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            int oldInstitutionID = (int)Entity.InstitutionReference.GetKey();

            ucPublicationsOfficeInput.Fill();

            int newInstitutionID = (int)Entity.InstitutionReference.GetKey();

            if (oldInstitutionID != newInstitutionID)
            {
                List<PublicationsOffice> notVerifiedPublicationsOffices = new PublicationsOfficeRepository(UnitOfWork).FindPublicationsOfficesByVerificationStatus(newInstitutionID, enVerificationStatus.NotVerified);
                List<PublicationsOffice> cannotBeVerifiedPublicationsOffices = new PublicationsOfficeRepository(UnitOfWork).FindPublicationsOfficesByVerificationStatus(oldInstitutionID, enVerificationStatus.CannotBeVerified);

                foreach (PublicationsOffice publicationsOffice in notVerifiedPublicationsOffices)
                {
                    if (publicationsOffice.ID != Entity.ID)
                    {
                        publicationsOffice.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(publicationsOffice.ID);
                    }
                }

                foreach (PublicationsOffice publicationsOffice in cannotBeVerifiedPublicationsOffices)
                {
                    if (publicationsOffice.ID != Entity.ID)
                    {
                        publicationsOffice.VerificationStatus = enVerificationStatus.NotVerified;
                        updatedIDs.Add(publicationsOffice.ID);
                    }
                }
            }

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendPublicationsOfficeUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}