using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Imis.Domain;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class AddIncidentReportPost : BaseEntityPortalPage<IncidentReport>
    {
        protected override void Fill()
        {
            Entity = new IncidentReportRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["id"]), x => x.Reporter, y => y.IncidentType);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            if (Entity != null)
            {
                ucIncidentReportView.SetIncidentReport(Entity);
                ucIncidentReportView.DataBind();

                ucIncidentReportPostsView.DataSource = odsIncidentReportPosts;
                ucIncidentReportPostsView.DataBind();

                if (Entity.ReportStatus == enReportStatus.Closed)
                {
                    lnkUnlockReport.Visible = true;

                    ucIncidentReportPostInput.Visible = false;
                    tbActions.Visible = false;
                }
                else
                {
                    lnkUnlockReport.Visible = false;

                    ucIncidentReportPostInput.SetIncidentReportPost(Entity);
                    ucIncidentReportPostInput.DataBind();

                    ucIncidentReportPostInput.Visible = true;
                    tbActions.Visible = true;
                }
            }
        }

        protected void btnUnlockReport_Click(object sender, EventArgs e)
        {
            Entity.ReportStatus = enReportStatus.Pending;

            UnitOfWork.Commit();

            DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            IncidentReportRepository iRep = new IncidentReportRepository(UnitOfWork);
            IncidentReportPostRepository ipRep = new IncidentReportPostRepository(UnitOfWork);

            ucIncidentReportPostInput.FillIncidentReportPost(Entity);

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
