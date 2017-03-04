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
    public partial class ViewReporterReports : BaseEntityPortalPage<object>
    {
        private Reporter _currentReporter = null;
        protected Reporter CurrentReporter
        {
            get
            {
                if (_currentReporter != null)
                    return _currentReporter;
                try
                {
                    int reporterID = Convert.ToInt32(Request.QueryString["rID"]);
                    if (reporterID > 0)
                        _currentReporter = new ReporterRepository(UnitOfWork).Load(reporterID);
                }
                catch (FormatException)
                {
                }

                return _currentReporter;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).EnablePageMethods = true;

            if (CurrentReporter != null && CurrentReporter.ReporterType != enReporterType.Online)
            {
                lnkAddIncidentReportForReporter.Attributes.Add("onclick", string.Format("popUp.show('ReportIncident.aspx?rID={0}','Αναφορά Συμβάντος', cmdRefresh);", CurrentReporter.ID));
                lnkAddIncidentReportForReporter.Visible = true;
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

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvIncidentReports.DataBind();
        }

        protected void odsIncidentReports_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            IncidentReportCriteria criteria = new IncidentReportCriteria();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("Reporter");
            criteria.Includes.Add("LastPost.LastDispatch");

            if (CurrentReporter != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Reporter.ID, CurrentReporter.ID);
            }

            e.InputParameters["criteria"] = criteria;
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
