using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Microsoft.Data.Extensions;
using Eudoxus.eService;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class EditMinistryPaymentsUser : BaseEntityPortalPage<MinistryPaymentsUser>
    {
        protected void ddlMinistryAuthorization_Init(object sender, EventArgs e)
        {
            ddlMinistryAuthorization.Items.Add(new ListItem("-- επιλέξτε --", ""));

            ddlMinistryAuthorization.Items.Add(new ListItem(enMinistryAuthorization.ReadOnly.GetLabel(), ((int)enMinistryAuthorization.ReadOnly).ToString()));
            ddlMinistryAuthorization.Items.Add(new ListItem(enMinistryAuthorization.Admin.GetLabel(), ((int)enMinistryAuthorization.Admin).ToString()));
        }

        protected override void Fill()
        {
            Entity = new MinistryPaymentsUserRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
        }

        protected override void OnLoad(EventArgs e)
        {
            ucMinistryPaymentsUserInput.Entity = Entity;
            if (!Page.IsPostBack)
            {
                ucMinistryPaymentsUserInput.Bind();
                ddlMinistryAuthorization.Items.FindByValue(Entity.MinistryAuthorizationInt.ToString()).Selected = true;
            }

            base.OnLoad(e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucMinistryPaymentsUserInput.Fill();

            int ministryAuthorization;
            if (int.TryParse(ddlMinistryAuthorization.SelectedValue, out ministryAuthorization) && ministryAuthorization > 0)
            {
                if (Entity.MinistryAuthorizationInt != ministryAuthorization)
                    Entity.MinistryAuthorizationInt = ministryAuthorization;
            }

            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
            {
                var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
                EudoxusOsyClient.Update(ministryPaymentsUserDto);
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}