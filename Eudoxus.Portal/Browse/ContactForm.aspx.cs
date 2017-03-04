using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;

using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Browse
{
    public partial class ContactForm : BaseEntityPortalPage<Reporter>
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

        protected void cvValidateSchool_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlReporterType.SelectedItem.Value == enReporterType.Student.ToString("D"))
                args.IsValid = !string.IsNullOrWhiteSpace(txtInstitutionName.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtSchoolName.Attributes.Add("onclick", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtSchoolName.Attributes.Add("onfocus", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtDepartmentName.Attributes.Add("onclick", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtDepartmentName.Attributes.Add("onfocus", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            if (!Page.IsPostBack)
            {
                txtReporterName.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";

                int reporterID = Convert.ToInt32(!String.IsNullOrEmpty(Request.QueryString["rID"]) ? Convert.ToInt32(Request.QueryString["rID"]) : 0);
                int reporterType = Convert.ToInt32(!String.IsNullOrEmpty(Request.QueryString["source"]) ? Convert.ToInt32(Request.QueryString["source"]) : 0);
                int incidentType = Convert.ToInt32(!String.IsNullOrEmpty(Request.QueryString["type"]) ? Convert.ToInt32(Request.QueryString["type"]) : 0);

                if (reporterID > 0)
                {
                    Reporter reporter = new ReporterRepository(UnitOfWork).Load(reporterID);

                    txtReporterEmail.Text = reporter.ContactEmail;
                }

                if (reporterType > 0)
                {
                    ddlReporterType.SelectedValue = reporterType.ToString();
                    ddlReporterType.Enabled = false;
                }

                if (incidentType > 0)
                {
                    ddlIncidentType.SelectedValue = incidentType.ToString();
                    ddlIncidentType.Enabled = false;
                }
            }
        }

        protected void ddlReporterType_Init(object sender, EventArgs e)
        {
            ddlReporterType.Items.Add(new ListItem("-- επιλέξτε σε ποιά κατηγορία χρηστών ανήκετε --", ""));

            ddlReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));            
            ddlReporterType.Items.Add(new ListItem(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString()));            
            ddlReporterType.Items.Add(new ListItem(enReporterType.Student.GetLabel(), ((int)enReporterType.Student).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Professor.GetLabel(), ((int)enReporterType.Professor).ToString()));
            ddlReporterType.Items.Add(new ListItem("Άλλο", ((int)enReporterType.Unknown).ToString()));
        }

        protected void lnkSend_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                IncidentReport ir = new IncidentReport();

                int reporterID = Convert.ToInt32(!String.IsNullOrEmpty(Request.QueryString["rID"]) ? Convert.ToInt32(Request.QueryString["rID"]) : 0);

                if (reporterID > 0)
                {
                    Reporter reporter = new ReporterRepository(UnitOfWork).Load(reporterID);

                    ir.Reporter = reporter;
                }
                else
                {
                    Online o = new Online();

                    o.OnlineReporterType = (enReporterType)Convert.ToInt32(ddlReporterType.SelectedValue);
                    o.ContactName = txtReporterName.Text.ToNull();
                    o.ContactPhone = txtReporterPhone.Text.ToNull();
                    o.ContactEmail = txtReporterEmail.Text.ToNull();
                    if ((o.OnlineReporterType == enReporterType.Student) || (o.OnlineReporterType == enReporterType.Secretary))
                        o.AcademicID = int.Parse(hfSchoolCode.Value);

                    o.CreatedBy = "online";

                    ir.Reporter = o;
                    ir.SubmissionType = enReportSubmissionType.Portal;
                }

                ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                ir.IncidentTypeID = Convert.ToInt32(ddlIncidentType.SelectedValue);

                ir.CreatedBy = "online";

                ir.ReporterName = txtReporterName.Text.ToNull();
                ir.ReporterPhone = txtReporterPhone.Text.ToNull();
                ir.ReporterEmail = txtReporterEmail.Text.ToNull();

                ir.ReportText = txtReportText.Text;
                ir.CallType = enCallType.Incoming;
                ir.ReportStatus = enReportStatus.Pending;

                UnitOfWork.MarkAsNew(ir);
                UnitOfWork.Commit();

                mvContact.SetActiveView(vComplete);
            }
        }
    }
}
