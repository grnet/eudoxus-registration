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
    public partial class EditSecretary : BaseEntityPortalPage<Secretary>
    {
        protected override void Fill()
        {
            Entity = new SecretaryRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["sID"]));
            if (Entity != null)
                Entity.SecretaryDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucSecretaryInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucSecretaryInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            int oldAcademicID = (int)Entity.AcademicReference.GetKey();

            ucSecretaryInput.Fill();

            int newAcademicID = (int)Entity.AcademicReference.GetKey();

            if (oldAcademicID != newAcademicID)
            {
                List<Secretary> notVerifiedSecretaries = new SecretaryRepository(UnitOfWork).FindSecretariesByVerificationStatus(newAcademicID, enVerificationStatus.NotVerified);
                List<Secretary> cannotBeVerifiedSecretaries = new SecretaryRepository(UnitOfWork).FindSecretariesByVerificationStatus(oldAcademicID, enVerificationStatus.CannotBeVerified);

                foreach (Secretary secretary in notVerifiedSecretaries)
                {
                    if (secretary.ID != Entity.ID)
                    {
                        secretary.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(secretary.ID);
                    }
                }

                foreach (Secretary secretary in cannotBeVerifiedSecretaries)
                {
                    if (secretary.ID != Entity.ID)
                    {
                        secretary.VerificationStatus = enVerificationStatus.NotVerified;
                        updatedIDs.Add(secretary.ID);
                    }
                }
            }

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendSecretaryUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}