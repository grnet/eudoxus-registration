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
using Microsoft.Data.Extensions;
using Eudoxus.Portal.Helpdesk.UserControls;
using System.Web.Services;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class SearchIncidentReports : BaseEntityPortalPage<object>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<IncidentType> incidentTypes = CacheManager.IncidentTypes.GetItems();
            Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, string.Empty);
            foreach (IncidentType incidentType in incidentTypes)
            {
                Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, incidentType.ID.ToString());
            }

            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).EnablePageMethods = true;

            if (!Page.IsPostBack)
            {
                deDateExportFrom.Date = deDateExportTo.Date = DateTime.Now;

                deIncidentReportDateFrom.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                deIncidentReportDateTo.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }

            gvIncidentReports.DefaultColumns = new List<UserControls.enIncidentReportsGridviewColumns>()
                {
                     enIncidentReportsGridviewColumns.CreatedAt,
                     enIncidentReportsGridviewColumns.LastPost_PostText,
                     enIncidentReportsGridviewColumns.Reporter_ReporterType,
                     enIncidentReportsGridviewColumns.ReporterName,
                     enIncidentReportsGridviewColumns.ReportStatus,
                     enIncidentReportsGridviewColumns.SpecialDetailsOfReporter,
                     enIncidentReportsGridviewColumns.ReportText,
                     enIncidentReportsGridviewColumns.Commands,
                      enIncidentReportsGridviewColumns.CallType,
                      enIncidentReportsGridviewColumns.HandlerType
                };
        }

        protected void ddlReportedBy_Init(object sender, EventArgs e)
        {
            ddlReportedBy.Items.Add(new ListItem("-- αδιάφορο --", ""));

            Users users = new Users();

            IList<MembershipUser> helpdeskUsers = users.FindUsersInRoles("%", new string[] { RoleNames.Helpdesk });

            foreach (MembershipUser user in helpdeskUsers)
            {
                ddlReportedBy.Items.Add(new ListItem(user.UserName, user.UserName));
            }
        }

        protected void ddlReportStatus_Init(object sender, EventArgs e)
        {
            ddlReportStatus.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enReportStatus item in Enum.GetValues(typeof(enReportStatus)))
            {
                ddlReportStatus.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlCallType_Init(object sender, EventArgs e)
        {
            ddlCallType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enCallType item in Enum.GetValues(typeof(enCallType)))
            {
                ddlCallType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlReporterType_Init(object sender, EventArgs e)
        {
            ddlReporterType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            ddlReporterType.Items.Add(new ListItem(enReporterType.Online.GetLabel(), ((int)enReporterType.Online).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Unknown.GetLabel(), ((int)enReporterType.Unknown).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.PricingCommittee.GetLabel(), ((int)enReporterType.PricingCommittee).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.MinistryPayments.GetLabel(), ((int)enReporterType.MinistryPayments).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Student.GetLabel(), ((int)enReporterType.Student).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Professor.GetLabel(), ((int)enReporterType.Professor).ToString()));
        }

        protected void ddlIncidentType_Init(object sender, EventArgs e)
        {
            ddlIncidentType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            var incidentTypes = CacheManager.IncidentTypes.GetItems().Where(x => x.SubSystemID == HelpDeskConstants.DEFAULT_SUBSYSTEM_ID).ToList();

            foreach (var item in incidentTypes)
            {
                ddlIncidentType.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        protected void ddlHandlerType_Init(object sender, EventArgs e)
        {
            ddlHandlerType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enHandlerType item in Enum.GetValues(typeof(enHandlerType)))
            {
                ddlHandlerType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlHandlerStatus_Init(object sender, EventArgs e)
        {
            ddlHandlerStatus.Items.Add(new ListItem("-- αδιάφορο --", ""));

            ddlHandlerStatus.Items.Add(new ListItem(enHandlerStatus.Pending.GetLabel(), ((int)enHandlerStatus.Pending).ToString()));
            ddlHandlerStatus.Items.Add(new ListItem(enHandlerStatus.Closed.GetLabel(), ((int)enHandlerStatus.Closed).ToString()));
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvIncidentReportsExport.Visible = true;
            gvIncidentReportsExport.PageIndex = 0;
            gvIncidentReportsExport.DataBind();

            gveIncidentReports.FileName = String.Format("IncidentReportDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveIncidentReports.WriteXlsToResponse(true);
        }

        protected void odsIncidentReports_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            IncidentReportCriteria criteria = new IncidentReportCriteria();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("Reporter");
            criteria.Includes.Add("LastPost.LastDispatch");

            //criteria.Expression = criteria.Expression.Where(x => x.SubSystem.ID, HelpDeskConstants.DEFAULT_SUBSYSTEM_ID);
            criteria.Expression = criteria.Expression.Where(x => x.SubmissionType, enReportSubmissionType.Helpdesk);

            int reportStatus;
            if (int.TryParse(ddlReportStatus.SelectedItem.Value, out reportStatus) && reportStatus > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ReportStatus, (enReportStatus)reportStatus);
            }

            if (!string.IsNullOrEmpty(ddlReportedBy.SelectedValue))
            {
                criteria.Expression = criteria.Expression.Where(x => x.CreatedBy, ddlReportedBy.SelectedValue);
            }

            int reporterType;
            if (int.TryParse(ddlReporterType.SelectedItem.Value, out reporterType) && reporterType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Reporter.ReporterType, (enReporterType)reporterType);
            }

            int incidentType;
            if (int.TryParse(ddlIncidentType.SelectedItem.Value, out incidentType) && incidentType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.IncidentType.ID, incidentType);
            }

            int handlerType;
            if (int.TryParse(ddlHandlerType.SelectedItem.Value, out handlerType) && handlerType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.HandlerType, (enHandlerType)handlerType);
            }

            int handlerStatus;
            if (int.TryParse(ddlHandlerStatus.SelectedItem.Value, out handlerStatus) && handlerStatus > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.HandlerStatus, (enHandlerStatus)handlerStatus);
            }

            int callType;
            if (int.TryParse(ddlCallType.SelectedItem.Value, out callType) && callType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.CallType, (enCallType)callType);
            }

            if (deIncidentReportDateFrom.Value != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.CreatedAt, deIncidentReportDateFrom.Date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThan);
            }

            if (deIncidentReportDateTo.Value != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.CreatedAt, deIncidentReportDateTo.Date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThan);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void odsIncidentReportsExport_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<IncidentReportDetailsView> criteria = new Criteria<IncidentReportDetailsView>();

            e.InputParameters["criteria"] = criteria;
        }

        protected void sdsIncidentReportsExport_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            sdsIncidentReportsExport.SelectParameters["FromDate"].DefaultValue = deDateExportFrom.Date.Date.ToString();
            sdsIncidentReportsExport.SelectParameters["ToDate"].DefaultValue = deDateExportTo.Date.Date.AddDays(1).ToString();
        }

        [WebMethod]
        public static string ChangeStatus(int incidentReportID, enReportStatus newStatus)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var ic = new IncidentReportRepository(uow).Load(incidentReportID);
                if (ic == null)
                    return null;
                ic.ReportStatus = newStatus;
                uow.Commit();
                return newStatus.GetIcon();
            }
        }
    }
}
