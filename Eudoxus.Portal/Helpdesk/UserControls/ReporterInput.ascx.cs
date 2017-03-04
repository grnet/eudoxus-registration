using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Helpdesk.UserControls
{
    public partial class ReporterInput : BaseUserControl<BaseEntityPortalPage<Reporter>>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
        }

        protected void ddlUnknownReporterType_Init(object sender, EventArgs e)
        {
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem("Άλλο", ((int)enReporterType.Unknown).ToString()));
        }

        public void FillReporter(Reporter reporter)
        {
            if (reporter is Unknown)
            {
                Unknown unknown = (Unknown)reporter;

                unknown.UnknownReporterType = (enReporterType)Convert.ToInt32(ddlUnknownReporterType.SelectedValue);
                unknown.IdentificationNumber = txtIdentificationNumber.Text.ToNull();
                unknown.Description = txtDescription.Text.ToNull();

                reporter = unknown;
            }
            else if (reporter is Student)
            {
                Student student = (Student)reporter;

                student.AcademicIdentifier = txtAcademicIdentifier.Text.ToNull();

                if (!string.IsNullOrEmpty(hfSchoolCode.Value))
                {
                    student.AcademicID = Convert.ToInt32(hfSchoolCode.Value);
                }
                else
                {
                    student.Academic = null;
                }

                reporter = student;
            }
            else if (reporter is Professor)
            {
                Professor professor = (Professor)reporter;

                if (!string.IsNullOrEmpty(hfSchoolCode.Value))
                {
                    professor.AcademicID = Convert.ToInt32(hfSchoolCode.Value);
                }
                else
                {
                    professor.Academic = null;
                }

                reporter = professor;
            }

            reporter.ContactName = txtContactName.Text.ToNull();
            reporter.ContactPhone = txtContactPhone.Text.ToNull();
            reporter.ContactEmail = txtContactEmail.Text.ToNull();
        }

        public void SetReporter(Reporter reporter)
        {
            if (reporter is Unknown)
            {
                Unknown unknown = (Unknown)reporter;

                lblReporterType.Text = enReporterType.Unknown.GetLabel();
                tbUnknownDetails.Visible = true;

                ddlUnknownReporterType.SelectedValue = ((int)unknown.UnknownReporterType).ToString();
                txtIdentificationNumber.Text = unknown.IdentificationNumber;
                txtDescription.Text = unknown.Description;
            }
            else if (reporter is Student)
            {
                Student student = (Student)reporter;

                lblReporterType.Text = enReporterType.Student.GetLabel();
                tbStudentDetails.Visible = true;
                tbAcademicDetails.Visible = true;

                student.AcademicReference.Load();
                Academic academic = student.Academic;

                if (academic != null)
                {
                    txtAcademicIdentifier.Text = student.AcademicIdentifier;
                    txtInstitutionName.Text = academic.Institution;
                    txtSchoolName.Text = academic.School != null ? academic.School : "-";
                    txtDepartmentName.Text = academic.Department != null ? academic.Department : "-";
                    hfSchoolCode.Value = academic.ID.ToString();
                }
            }
            else if (reporter is Professor)
            {
                Professor professor = (Professor)reporter;

                lblReporterType.Text = enReporterType.Professor.GetLabel();
                tbProfessorDetails.Visible = true;
                tbAcademicDetails.Visible = true;

                professor.AcademicReference.Load();
                Academic academic = professor.Academic;

                if (academic != null)
                {
                    txtInstitutionName.Text = academic.Institution;
                    txtSchoolName.Text = academic.School != null ? academic.School : "-";
                    txtDepartmentName.Text = academic.Department != null ? academic.Department : "-";
                    hfSchoolCode.Value = academic.ID.ToString();
                }
            }

            txtContactName.Text = reporter.ContactName;
            txtContactPhone.Text = reporter.ContactPhone;
            txtContactEmail.Text = reporter.ContactEmail;
        }
    }
}