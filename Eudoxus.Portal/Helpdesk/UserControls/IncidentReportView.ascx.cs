using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Helpdesk.UserControls
{
    public partial class IncidentReportView : System.Web.UI.UserControl
    {
        protected IncidentReport CurrentIncidentReport { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetIncidentReport(IncidentReport incidentReport)
        {
            CurrentIncidentReport = incidentReport;

            DataBind();
        }

        public override void DataBind()
        {
            ltIncidentID.Text = CurrentIncidentReport.ID.ToString();

            ltReporterType.Text = CurrentIncidentReport.Reporter.GetLabel();

            if (CurrentIncidentReport.Reporter is Student)
            {
                phStudent.Visible = true;

                //ltUsername.Text = ((Student)CurrentIncidentReport.Reporter).Username;

            }
            else if (CurrentIncidentReport.Reporter is Publisher)
            {
                phPublisher.Visible = true;

                ltPublisherAFM.Text = ((Publisher)CurrentIncidentReport.Reporter).PublisherAFM;
                ltPublisherName.Text = ((Publisher)CurrentIncidentReport.Reporter).PublisherName;

            }
            else if (CurrentIncidentReport.Reporter is Secretary)
            {
                phSecretary.Visible = true;

                Secretary secretary = (Secretary)CurrentIncidentReport.Reporter;

                var academic = CacheManager.Academics.Get(secretary.AcademicID);

                ltInstitution.Text = academic.Institution;
                ltSchool.Text = academic.School;
                ltDepartment.Text = academic.Department;
            }
            else if (CurrentIncidentReport.Reporter is PublicationsOffice)
            {
                phInstitution.Visible = true;

                PublicationsOffice publicationsOffice = (PublicationsOffice)CurrentIncidentReport.Reporter;

                var institution = CacheManager.Institutions.Get(publicationsOffice.InstitutionID);

                ltInstitution2.Text = institution.Name;
            }
            else if (CurrentIncidentReport.Reporter is DataCenter)
            {
                phInstitution.Visible = true;

                DataCenter dataCenter = (DataCenter)CurrentIncidentReport.Reporter;

                var institution = CacheManager.Institutions.Get(dataCenter.InstitutionID);

                ltInstitution2.Text = institution.Name;
            }


            ltIncidentType.Text = CurrentIncidentReport.IncidentType.Name;

            ltCreatedBy.Text = string.Format("{0}, {1:dd/MM/yyyy HH:mm}", CurrentIncidentReport.CreatedBy, CurrentIncidentReport.CreatedAt);

            if (CurrentIncidentReport.UpdatedBy != null)
            {
                trUpdatedBy.Visible = true;
                ltUpdatedBy.Text = string.Format("{0}, {1:dd/MM/yyyy HH:mm}", CurrentIncidentReport.UpdatedBy, CurrentIncidentReport.UpdatedAt);
            }
            else
            {
                trUpdatedBy.Visible = false;
            }

            ltReporterName.Text = CurrentIncidentReport.ReporterName;
            ltReporterPhone.Text = CurrentIncidentReport.ReporterPhone;
            ltReporterEmail.Text = CurrentIncidentReport.ReporterEmail;

            ltReportText.Text = CurrentIncidentReport.ReportText;
            ltReportStatus.Text = CurrentIncidentReport.ReportStatus.GetLabel();
        }
    }
}