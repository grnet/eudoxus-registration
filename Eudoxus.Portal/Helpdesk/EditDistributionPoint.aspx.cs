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
    public partial class EditDistributionPoint : BaseEntityPortalPage<DistributionPoint>
    {
        protected override void Fill()
        {
            Entity = new DistributionPointRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.DistributionPointDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucDistributionPointInput.Entity = Entity;
            if (!Page.IsPostBack)
                ucDistributionPointInput.Bind();

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucDistributionPointInput.Fill();

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendDistributionPointUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}