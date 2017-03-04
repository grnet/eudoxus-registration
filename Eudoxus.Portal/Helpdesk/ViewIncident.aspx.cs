using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class ViewIncident : BaseEntityPortalPage<IncidentReport>
    {
        protected override void Fill()
        {
            Entity = new IncidentReportRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["id"]), x => x.Reporter, y => y.IncidentType, z => z.LastPost);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity != null)
            {
                if (Entity.LastPost == null)
                {
                    btnSendEmail.Visible = false;
                }

                incidentReportView.SetIncidentReport(Entity);
                incidentReportView.DataBind();

                incidentReportPostsView.DataSource = odsIncidentReportPosts;
                incidentReportPostsView.DataBind();

                phFromSuccess.DataBind();
            }
            else
            {
                //ErrorHelper.ShowBackofficeError(Response, "Δε βρέθηκε η αναφορά με κωδικό " + Request.QueryString["id"]);
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            MailSender.SendIncidentReportAnswer(Entity.ReporterEmail, Entity.ID.ToString(), Entity.ReportText, Entity.LastPost.PostText);

            Dispatch d = new Dispatch();

            d.IncidentReportPost = Entity.LastPost;
            d.DispatchType = enDispatchType.Email;
            d.DispatchText = Entity.LastPost.PostText;
            d.DispatchSentAt = DateTime.Now;
            d.DispatchSentBy = Page.User.Identity.Name;

            UnitOfWork.MarkAsNew(d);

            Entity.ReportStatus = enReportStatus.Closed;
            Entity.LastPost.LastDispatch = d;

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
