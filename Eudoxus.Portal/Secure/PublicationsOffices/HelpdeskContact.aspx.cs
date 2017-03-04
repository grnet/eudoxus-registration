using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using System.Xml.Linq;
using Eudoxus.Portal.DataSources;
using System.Web.Security;
using System.IO;
using System.Reflection;
using System.Xml;
using Imis.Domain;
using Eudoxus.Portal.Controls;
using Eudoxus.Mails;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.PublicationsOffices
{
    public partial class HelpdeskContact : BaseEntityPortalPage<PublicationsOffice>
    {
        protected override void Fill()
        {
            Entity = new PublicationsOfficeRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string link = this.ResolveUrl("/Secure/PublicationsOffices/ContactForm.aspx");

            lnkSubmitQuestion.NavigateUrl = link;
            lnkSubmitQuestion.Attributes["onclick"] = "popUp.show('" + link + "','Ερώτηση προς Γραφείο Αρωγής', cmdRefresh);";
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvIncidentReports.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvIncidentReports.PageIndex = 0;
            gvIncidentReports.DataBind();
        }

        protected void odsIncidentReports_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            IncidentReportCriteria criteria = new IncidentReportCriteria();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("Reporter");
            criteria.Includes.Add("LastPost.LastDispatch");

            criteria.Expression = criteria.Expression.Where(x => x.SubSystem.ID, HelpDeskConstants.DEFAULT_SUBSYSTEM_ID);
            criteria.Expression = criteria.Expression.Where(x => x.SubmissionType, enReportSubmissionType.LoggedInUser);
            criteria.Expression = criteria.Expression.Where(x => x.Reporter.ID, Entity.ID);

            e.InputParameters["criteria"] = criteria;
        }

        protected string GetIncidentTypeDetails(IncidentReport ir)
        {
            string incidentTypeDetails = string.Empty;

            IncidentType it = CacheManager.IncidentTypes.Get((int)ir.IncidentTypeReference.GetKey());

            incidentTypeDetails = string.Format("{0}", it.Name);

            return incidentTypeDetails;
        }

        protected string GetLastAnswer(IncidentReport ir)
        {
            string lastAnswer = string.Empty;

            if (ir.LastPost != null && ir.LastPost.LastDispatch != null)
            {
                Dispatch lastDispatch = ir.LastPost.LastDispatch;

                lastAnswer = string.Format("<span style=\"font-size: 11px; font-weight: bold\">Ημ/νία Απάντησης</span><br />{0}<br/><br/>{1}", lastDispatch.DispatchSentAt, lastDispatch.DispatchText);
            }

            return lastAnswer;
        }
    }
}
