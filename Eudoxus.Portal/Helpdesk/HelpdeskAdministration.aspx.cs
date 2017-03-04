using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class HelpdeskAdministration : BaseEntityPortalPage<object>
    {
        protected void ddlAcademicYear_Init(object sender, EventArgs e)
        {
            for (int i = 2010; i < 2020; i++)
            {
                string year = i.ToString() + " - " + (i + 1).ToString();

                ddlAcademicYear.Items.Add(new ListItem(year, i.ToString()));
            }

            ddlAcademicYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnCreateLessonReports_Click(object sender, EventArgs e)
        {
            int academicYear = int.Parse(ddlAcademicYear.SelectedItem.Value);
            string yearList = academicYear.ToString() + " - " + (academicYear + 1).ToString();

            LessonReportCriteria criteria = new LessonReportCriteria();

            criteria.Expression = criteria.Expression.Where(x => x.SubmissionType, enReportSubmissionType.InternalReporter);
            criteria.Expression = criteria.Expression.Where(x => x.AcademicYear, academicYear);

            int recordCount;
            var incidentReports = new LessonReportRepository(UnitOfWork).FindLessonReportsWithCriteria(criteria, out recordCount);

            if (recordCount > 0)
            {
                lblResults.Text = string.Format("Έχετε ήδη δημιουργήσει λίστα συμβάντων για το ακαδημαϊκό έτος {0}", yearList);
                return;
            }

            foreach (var academic in CacheManager.Academics.GetItems())
            {
                LessonReport lr = new LessonReport();

                lr.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                lr.SubmissionType = enReportSubmissionType.InternalReporter;
                lr.CallType = enCallType.Outgoing;
                lr.HandlerType = enHandlerType.Helpdesk;
                lr.HandlerStatus = enHandlerStatus.NotHandledBySupervisor;
                lr.IncidentTypeID = AutomaticIncidentTypes.SecretaryLessonSubmission;

                var reporter = new SecretaryRepository(UnitOfWork).FindByAcademicID(academic.ID);
                lr.Reporter = reporter;

                lr.ReporterName = reporter.ContactName;
                lr.ReporterPhone = reporter.ContactPhone;
                lr.ReporterEmail = reporter.ContactEmail;

                lr.ReportStatus = enReportStatus.Pending;
                lr.ReportText = string.Format(AutomaticIncidentReportMessages.LessonSubmission, "2010-2011");

                lr.AcademicYear = 2010;
                lr.InstitutionID = academic.InstitutionID;

                UnitOfWork.Commit();
            }

            lblResults.Text = string.Format("Η καταχώριση των συμβάντων για το ακαδημαϊκό έτος {0} ολοκληρώθηκε επιτυχώς.", yearList);
        }
    }
}