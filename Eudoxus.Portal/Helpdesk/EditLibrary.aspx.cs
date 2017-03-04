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
    public partial class EditLibrary : BaseEntityPortalPage<Library>
    {
        protected override void Fill()
        {
            Entity = new LibraryRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.LibraryDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucLibraryInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucLibraryInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            int oldInstitutionID = (int)Entity.InstitutionReference.GetKey();

            ucLibraryInput.Fill();

            int newInstitutionID = (int)Entity.InstitutionReference.GetKey();

            if (oldInstitutionID != newInstitutionID)
            {
                List<Library> notVerifiedLibraries = new LibraryRepository(UnitOfWork).FindLibrariesByVerificationStatus(newInstitutionID, enVerificationStatus.NotVerified);
                List<Library> cannotBeVerifiedLibraries = new LibraryRepository(UnitOfWork).FindLibrariesByVerificationStatus(oldInstitutionID, enVerificationStatus.CannotBeVerified);

                foreach (Library library in notVerifiedLibraries)
                {
                    if (library.ID != Entity.ID)
                    {
                        library.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(library.ID);
                    }
                }

                foreach (Library library in cannotBeVerifiedLibraries)
                {
                    if (library.ID != Entity.ID)
                    {
                        library.VerificationStatus = enVerificationStatus.NotVerified;
                        updatedIDs.Add(library.ID);
                    }
                }
            }

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendLibraryUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}