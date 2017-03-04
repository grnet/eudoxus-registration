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
    public partial class EditDataCenter : BaseEntityPortalPage<DataCenter>
    {
        protected override void Fill()
        {
            Entity = new DataCenterRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.DataCenterDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucDataCenterInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucDataCenterInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            int oldInstitutionID = (int)Entity.InstitutionReference.GetKey();

            ucDataCenterInput.Fill();

            int newInstitutionID = (int)Entity.InstitutionReference.GetKey();

            if (oldInstitutionID != newInstitutionID)
            {
                List<DataCenter> notVerifiedDataCenters = new DataCenterRepository(UnitOfWork).FindDataCentersByVerificationStatus(newInstitutionID, enVerificationStatus.NotVerified);
                List<DataCenter> cannotBeVerifiedDataCenters = new DataCenterRepository(UnitOfWork).FindDataCentersByVerificationStatus(oldInstitutionID, enVerificationStatus.CannotBeVerified);

                foreach (DataCenter dataCenter in notVerifiedDataCenters)
                {
                    if (dataCenter.ID != Entity.ID)
                    {
                        dataCenter.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(dataCenter.ID);
                    }
                }

                foreach (DataCenter dataCenter in cannotBeVerifiedDataCenters)
                {
                    if (dataCenter.ID != Entity.ID)
                    {
                        dataCenter.VerificationStatus = enVerificationStatus.NotVerified;
                        updatedIDs.Add(dataCenter.ID);
                    }
                }
            }

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendDataCenterUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}