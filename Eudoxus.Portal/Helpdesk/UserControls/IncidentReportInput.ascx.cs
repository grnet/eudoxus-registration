using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

using System.Xml.Linq;
using DevExpress.Web.ASPxClasses;
using Eudoxus.Portal.Controls;
using Imis.Domain;

namespace Eudoxus.Portal.Helpdesk.UserControls
{
    public partial class IncidentReportInput : BaseUserControl<BaseEntityPortalPage<IncidentReport>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer) {
            IList<IncidentType> incidentTypes = CacheManager.IncidentTypes.GetItems();

            Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, string.Empty);

            foreach (IncidentType incidentType in incidentTypes) {
                Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, incidentType.ID.ToString());
            }

            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e) {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtReporterName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
        }

        protected void ddlReporterType_Init(object sender, EventArgs e) {
            ddlReporterType.Items.Add(new ListItem("-- αδιάφορο --", ""));

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

        protected void ddlCallType_Init(object sender, EventArgs e) {
            ddlCallType.Items.Add(new ListItem("-- επιλέξτε τύπο κλήσης --", ""));

            foreach (enCallType item in Enum.GetValues(typeof(enCallType))) {
                ddlCallType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlUnknownReporterType_Init(object sender, EventArgs e) {
            ddlUnknownReporterType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.PricingCommittee.GetLabel(), ((int)enReporterType.PricingCommittee).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.MinistryPayments.GetLabel(), ((int)enReporterType.MinistryPayments).ToString()));
            //ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem("Άλλο", ((int)enReporterType.Unknown).ToString()));
        }

        protected void ddlReportStatus_Init(object sender, EventArgs e) {
            foreach (enReportStatus item in Enum.GetValues(typeof(enReportStatus))) {
                ddlReportStatus.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }

            ddlReportStatus.SelectedValue = ((int)enReportStatus.Pending).ToString();
        }

        public void FillIncidentReport(IncidentReport ir) {
            if (ir.IsNew) {
                ir.CreatedBy = Page.User.Identity.Name.ToLower().Trim();
                ir.ReportStatus = enReportStatus.Pending;
            }
            else {
                ir.UpdatedBy = Page.User.Identity.Name.ToLower().Trim();
                ir.UpdatedAt = DateTime.Now;
            }

            if (ir.Reporter == null) {
                switch ((enReporterType)Convert.ToInt32(ddlReporterType.SelectedValue)) {
                    case enReporterType.Unknown:
                        Unknown unknown = new Unknown();

                        unknown.UnknownReporterType = (enReporterType)Convert.ToInt32(ddlUnknownReporterType.SelectedValue);
                        unknown.IdentificationNumber = txtIdentificationNumber.Text.ToNull();
                        unknown.Description = txtDescription.Text.ToNull();

                        ir.Reporter = unknown;

                        break;
                    case enReporterType.Student:
                        Student student = new Student();

                        student.AcademicIdentifier = txtAcademicIdentifier.Text.ToNull();

                        if (!string.IsNullOrEmpty(hfSchoolCode.Value)) {
                            student.AcademicID = Convert.ToInt32(hfSchoolCode.Value);
                        }
                        else {
                            student.Academic = null;
                        }

                        ir.Reporter = student;

                        break;
                    case enReporterType.Professor:
                        Professor professor = new Professor();

                        if (!string.IsNullOrEmpty(hfSchoolCode.Value)) {
                            professor.AcademicID = Convert.ToInt32(hfSchoolCode.Value);
                        }
                        else {
                            professor.Academic = null;
                        }

                        ir.Reporter = professor;

                        break;
                    default:
                        break;
                }
            }

            enReporterType reporterType = (enReporterType)int.Parse(ddlReporterType.SelectedItem.Value);

            if (reporterType == enReporterType.Unknown || reporterType == enReporterType.Student || reporterType == enReporterType.Professor) {
                ir.Reporter.ContactName = txtReporterName.Text.ToNull();
                ir.Reporter.ContactPhone = txtReporterPhone.Text.ToNull();
                ir.Reporter.ContactEmail = txtReporterEmail.Text.ToNull();
            }

            ir.IncidentTypeID = Convert.ToInt32(ddlIncidentType.SelectedValue);

            ir.ReporterName = txtReporterName.Text.ToNull();
            ir.ReporterPhone = txtReporterPhone.Text.ToNull();
            ir.ReporterEmail = txtReporterEmail.Text.ToNull();

            ir.CallType = (enCallType)Convert.ToInt32(ddlCallType.SelectedValue);
            ir.ReportStatus = (enReportStatus)Convert.ToInt32(ddlReportStatus.SelectedValue);
            ir.ReportText = txtReportText.Text.ToNull();
        }

        public void SetIncidentReport(IncidentReport ir, bool isNew) {
            if (ir.Reporter != null) {
                if (ir.Reporter is Unknown) {
                    Unknown unknown = (Unknown)ir.Reporter;

                    ddlUnknownReporterType.SelectedValue = ((int)unknown.UnknownReporterType).ToString();
                    txtIdentificationNumber.Text = unknown.IdentificationNumber;
                    txtDescription.Text = unknown.Description;

                    ddlReporterType.SelectedValue = ((int)enReporterType.Unknown).ToString();
                }
                else if (ir.Reporter is Student) {
                    Student student = (Student)ir.Reporter;

                    student.AcademicReference.Load();
                    Academic academic = student.Academic;

                    if (academic != null) {
                        txtAcademicIdentifier.Text = student.AcademicIdentifier;
                        txtInstitutionName.Text = academic.Institution;
                        txtSchoolName.Text = academic.School != null ? academic.School : "-";
                        txtDepartmentName.Text = academic.Department != null ? academic.Department : "-";
                        hfSchoolCode.Value = academic.ID.ToString();
                    }

                    ddlReporterType.SelectedValue = ((int)enReporterType.Student).ToString();
                }
                else if (ir.Reporter is Professor) {
                    Professor professor = (Professor)ir.Reporter;

                    professor.AcademicReference.Load();
                    Academic academic = professor.Academic;

                    if (academic != null) {
                        txtInstitutionName.Text = academic.Institution;
                        txtSchoolName.Text = academic.School != null ? academic.School : "-";
                        txtDepartmentName.Text = academic.Department != null ? academic.Department : "-";
                        hfSchoolCode.Value = academic.ID.ToString();
                    }

                    ddlReporterType.SelectedValue = ((int)enReporterType.Professor).ToString();
                }
                else if (ir.Reporter is Publisher) {
                    Publisher publisher = (Publisher)ir.Reporter;

                    lblPublisherAFM.Text = publisher.PublisherAFM;

                    lblPublisherName.Text = publisher.PublisherName;

                    ddlReporterType.SelectedValue = ((int)enReporterType.Publisher).ToString();
                }
                else if (ir.Reporter is Secretary) {
                    Secretary secretary = (Secretary)ir.Reporter;

                    secretary.AcademicReference.Load();

                    Academic academic = secretary.Academic;

                    lblInstitution.Text = academic.Institution;
                    lblSchool.Text = academic.School != null ? academic.School : "-";
                    lblDepartment.Text = academic.Department != null ? academic.Department : "-";

                    ddlReporterType.SelectedValue = ((int)enReporterType.Secretary).ToString();
                }
                else if (ir.Reporter is DistributionPoint) {
                    DistributionPoint distributionPoint = (DistributionPoint)ir.Reporter;

                    lblDistributionPointName.Text = distributionPoint.DistributionPointName;

                    ddlReporterType.SelectedValue = ((int)enReporterType.DistributionPoint).ToString();
                }
                else if (ir.Reporter is PublicationsOffice) {
                    PublicationsOffice publicationsOffice = (PublicationsOffice)ir.Reporter;

                    publicationsOffice.InstitutionReference.Load();

                    Institution institution = publicationsOffice.Institution;

                    lblPubInstitution.Text = institution.Name;

                    ddlReporterType.SelectedValue = ((int)enReporterType.PublicationsOffice).ToString();
                }
                else if (ir.Reporter is DataCenter) {
                    DataCenter dataCenter = (DataCenter)ir.Reporter;

                    dataCenter.InstitutionReference.Load();

                    Institution institution = dataCenter.Institution;

                    lblPubInstitution.Text = institution.Name;

                    ddlReporterType.SelectedValue = ((int)enReporterType.DataCenter).ToString();
                }
                else if (ir.Reporter is Library) {
                    Library library = (Library)ir.Reporter;

                    library.InstitutionReference.Load();

                    Institution institution = library.Institution;

                    lblLibraryInstitution.Text = institution.Name;
                    lblLibraryName.Text = library.LibraryName;

                    ddlReporterType.SelectedValue = ((int)enReporterType.Library).ToString();
                }
                else if (ir.Reporter is BookSupplier) {
                    BookSupplier bookSupplier = (BookSupplier)ir.Reporter;

                    bookSupplier.InstitutionReference.Load();

                    Institution institution = bookSupplier.Institution;

                    lblPubInstitution.Text = institution.Name;

                    ddlReporterType.SelectedValue = ((int)enReporterType.BookSupplier).ToString();
                }
                else if (ir.Reporter is PricingCommittee)
                {
                    PricingCommittee pricingCommittee = (PricingCommittee)ir.Reporter;

                    ddlReporterType.SelectedValue = ((int)enReporterType.PricingCommittee).ToString();
                }
                else if (ir.Reporter is MinistryPaymentsUser)
                {
                    MinistryPaymentsUser ministryPaymentsUser = (MinistryPaymentsUser)ir.Reporter;

                    ddlReporterType.SelectedValue = ((int)enReporterType.MinistryPayments).ToString();
                }

                ddlReporterType.Enabled = false;

                txtReporterName.Text = ir.Reporter.ContactName;
                txtReporterPhone.Text = ir.Reporter.ContactPhone;
                txtReporterEmail.Text = ir.Reporter.ContactEmail;

                ddlCallType.SelectedValue = ((int)ir.CallType).ToString();

                if (!isNew) {
                    ddlIncidentType.SelectedValue = ir.IncidentType.ID.ToString();
                    cddIncidentType.SelectedValue = ir.IncidentType.ID.ToString();

                    ddlReportStatus.SelectedValue = ((int)ir.ReportStatus).ToString();
                    txtReportText.Text = ir.ReportText;
                }
                else {
                    ddlReportStatus.SelectedValue = ((int)enReportStatus.Pending).ToString();
                }
            }
            else {
                ddlReporterType.Items.Clear();
                ddlReporterType.Items.Add(new ListItem("-- αδιάφορο --", ""));

                ddlReporterType.Items.Add(new ListItem(enReporterType.Unknown.GetLabel(), ((int)enReporterType.Unknown).ToString()));
                ddlReporterType.Items.Add(new ListItem(enReporterType.Student.GetLabel(), ((int)enReporterType.Student).ToString()));
                ddlReporterType.Items.Add(new ListItem(enReporterType.Professor.GetLabel(), ((int)enReporterType.Professor).ToString()));
            }
        }
    }
}