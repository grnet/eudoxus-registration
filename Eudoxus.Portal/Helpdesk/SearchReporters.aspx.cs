using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using DevExpress.Web.ASPxClasses;
using Eudoxus.Portal.Controls;
using System.Drawing;
using Microsoft.Data.Extensions;
namespace Eudoxus.Portal.Helpdesk
{
    public partial class SearchReporters : BasePortalPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            if (!Page.IsPostBack)
            {
                txtContactName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtContactPhone.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtContactEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));

                txtIdentificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtDescription.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtAcademicIdentifier.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtPublisherAFM.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtPublisherName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtPublisherTradeName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtLibraryName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtDistributionPointName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtCertificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void rblReporterType_Init(object sender, EventArgs e)
        {
            rblReporterType.Items.Clear();

            rblReporterType.Items.Add(enReporterType.Unknown.GetLabel(), ((int)enReporterType.Unknown).ToString());
            rblReporterType.Items.Add(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString());
            rblReporterType.Items.Add(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString());
            rblReporterType.Items.Add(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString());
            rblReporterType.Items.Add(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString());
            rblReporterType.Items.Add(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString());
            rblReporterType.Items.Add(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString());
            rblReporterType.Items.Add(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString());
            rblReporterType.Items.Add(enReporterType.Student.GetLabel(), ((int)enReporterType.Student).ToString());
            rblReporterType.Items.Add(enReporterType.Professor.GetLabel(), ((int)enReporterType.Professor).ToString());
        }

        protected void ddlUnknownReporterType_Init(object sender, EventArgs e)
        {
            ddlUnknownReporterType.Items.Add(new ListItem("-- αδιάφορο --", ""));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString()));
            ddlUnknownReporterType.Items.Add(new ListItem("Άλλο", ((int)enReporterType.Unknown).ToString()));
        }

        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- επιλέξτε ίδρυμα --", ""));
            ddlLibraryInstitution.Items.Add(new ListItem("-- επιλέξτε ίδρυμα --", ""));

            foreach (var item in CacheManager.Institutions.GetItems())
            {
                ddlInstitution.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                ddlLibraryInstitution.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvReporters.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvReporters.PageIndex = 0;
            gvReporters.DataBind();
        }

        protected void odsReporters_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!Page.IsPostBack)
                e.Cancel = true;

            ReporterCriteria criteria = new ReporterCriteria();

            criteria.Includes = new List<string>();

            if (!string.IsNullOrEmpty(txtContactName.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.ContactName, txtContactName.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
            }

            if (!string.IsNullOrEmpty(txtContactPhone.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.ContactPhone, txtContactPhone.Text.ToNull());
            }

            if (!string.IsNullOrEmpty(txtContactEmail.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.ContactEmail, txtContactEmail.Text.ToNull());
            }

            int reporterType = 0;
            if (rblReporterType.Value != null && int.TryParse(rblReporterType.Value.ToString(), out reporterType) && reporterType > 0)
            {
                criteria.ReporterType.FieldValue = (enReporterType)reporterType;
                criteria.Expression = criteria.Expression.Where(x => x.ReporterType, (enReporterType)reporterType);
            }

            int schoolCode;
            int institutionID;

            switch ((enReporterType)reporterType)
            {
                case enReporterType.Unknown:
                    int unknownReporterType;
                    if (int.TryParse(ddlUnknownReporterType.SelectedItem.Value, out unknownReporterType) && unknownReporterType > 0)
                    {
                        criteria.UnknownReporterType.FieldValue = (enReporterType)unknownReporterType;
                    }

                    if (!string.IsNullOrEmpty(txtIdentificationNumber.Text))
                    {
                        criteria.IdentificationNumber.FieldValue = txtIdentificationNumber.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        criteria.Description.FieldValue = txtDescription.Text.ToNull();
                    }

                    break;
                case enReporterType.Publisher:
                    if (!string.IsNullOrEmpty(txtPublisherAFM.Text))
                    {
                        criteria.PublisherAFM.FieldValue = txtPublisherAFM.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtPublisherName.Text))
                    {
                        criteria.PublisherName.FieldValue = txtPublisherName.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtPublisherTradeName.Text))
                    {
                        criteria.PublisherTradeName.FieldValue = txtPublisherTradeName.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }
                    break;
                case enReporterType.DistributionPoint:
                    if (!string.IsNullOrEmpty(txtDistributionPointName.Text))
                    {
                        criteria.DistributionPointName.FieldValue = txtDistributionPointName.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }
                    break;
                case enReporterType.Secretary:
                    if (int.TryParse(hfSchoolCode.Value, out schoolCode))
                    {
                        criteria.AcademicID.FieldValue = schoolCode;
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }

                    break;
                case enReporterType.PublicationsOffice:
                    if (int.TryParse(ddlInstitution.SelectedItem.Value, out institutionID) && institutionID > 0)
                    {
                        criteria.InstitutionID.FieldValue = institutionID;
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }

                    break;
                case enReporterType.DataCenter:
                    if (int.TryParse(ddlInstitution.SelectedItem.Value, out institutionID) && institutionID > 0)
                    {
                        criteria.InstitutionID.FieldValue = institutionID;
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }

                    break;
                case enReporterType.Library:
                    if (int.TryParse(ddlLibraryInstitution.SelectedItem.Value, out institutionID) && institutionID > 0)
                    {
                        criteria.InstitutionID.FieldValue = institutionID;
                    }

                    if (!string.IsNullOrEmpty(txtLibraryName.Text))
                    {
                        criteria.LibraryName.FieldValue = txtLibraryName.Text.ToNull();
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }

                    break;
                case enReporterType.BookSupplier:
                    if (int.TryParse(ddlInstitution.SelectedItem.Value, out institutionID) && institutionID > 0)
                    {
                        criteria.InstitutionID.FieldValue = institutionID;
                    }

                    if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
                    {
                        criteria.CertificationNumber.FieldValue = int.Parse(txtCertificationNumber.Text.ToNull());
                    }

                    break;
                case enReporterType.Student:
                    if (!string.IsNullOrEmpty(txtAcademicIdentifier.Text))
                    {
                        criteria.AcademicIdentifier.FieldValue = txtAcademicIdentifier.Text.ToNull();
                    }

                    if (int.TryParse(hfSchoolCode.Value, out schoolCode))
                    {
                        criteria.AcademicID.FieldValue = schoolCode;
                    }

                    break;
                case enReporterType.Professor:
                    if (int.TryParse(hfSchoolCode.Value, out schoolCode))
                    {
                        criteria.AcademicID.FieldValue = schoolCode;
                    }

                    break;
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvReporters_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            Reporter reporter = (Reporter)gvReporters.GetRow(e.VisibleIndex);

            if (reporter != null)
            {
                if (reporter is Publisher)
                {
                    Publisher publisher = (Publisher)reporter;

                    switch (publisher.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is Secretary)
                {
                    Secretary secretary = (Secretary)reporter;

                    switch (secretary.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is DistributionPoint)
                {
                    DistributionPoint distributionPoint = (DistributionPoint)reporter;

                    switch (distributionPoint.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is PublicationsOffice)
                {
                    PublicationsOffice publicationsOffice = (PublicationsOffice)reporter;

                    switch (publicationsOffice.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is DataCenter)
                {
                    DataCenter dataCenter = (DataCenter)reporter;

                    switch (dataCenter.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is Library)
                {
                    Library library = (Library)reporter;

                    switch (library.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
                else if (reporter is BookSupplier)
                {
                    BookSupplier bookSupplier = (BookSupplier)reporter;

                    switch (bookSupplier.VerificationStatus)
                    {
                        case enVerificationStatus.NotVerified:
                            e.Row.BackColor = Color.DarkGray;
                            break;
                        case enVerificationStatus.Verified:
                            e.Row.BackColor = Color.LightGreen;
                            break;
                        case enVerificationStatus.CannotBeVerified:
                            e.Row.BackColor = Color.Tomato;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        protected string GetReporterDetails(object reporter)
        {
            if (reporter == null)
                return string.Empty;

            string reporterDetails = string.Empty;

            if (reporter is Unknown)
            {
                Unknown unknown = (Unknown)reporter;

                if (!string.IsNullOrEmpty(unknown.IdentificationNumber))
                {
                    reporterDetails = string.Format("Είδος Χρήστη: {0}<br/>Α.Δ.Τ.: {1}<br/>Λοιπά Στοιχεία: {2}", unknown.UnknownReporterType.GetLabel(), unknown.IdentificationNumber, unknown.Description);
                }
                else
                {
                    reporterDetails = string.Format("Είδος Χρήστη: {0}<br/>Λοιπά Στοιχεία: {1}", unknown.UnknownReporterType.GetLabel(), unknown.Description);
                }
            }
            else if (reporter is Student)
            {
                Student student = (Student)reporter;

                var academic = CacheManager.Academics.Get(student.AcademicID.GetValueOrDefault());

                if (academic != null)
                {
                    reporterDetails = string.Format("Αρ. Μητρώου: {0}<br/>Ίδρυμα: {1}<br/>Σχολή: {2}<br/>Τμήμα: {3}", student.AcademicIdentifier, academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));
                }
            }
            else if (reporter is Professor)
            {
                Professor professor = (Professor)reporter;

                var academic = CacheManager.Academics.Get(professor.AcademicID.GetValueOrDefault());

                if (academic != null)
                    reporterDetails = string.Format("Ίδρυμα: {0} <br/>Σχολή: {1}<br/>Τμήμα: {2}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));

            }
            else if (reporter is Publisher)
            {
                Publisher publisher = (Publisher)reporter;

                if (!string.IsNullOrEmpty(publisher.PublisherTradeName))
                {
                    reporterDetails = string.Format("{0}<br/>{1}<br/>{2}", publisher.PublisherName, publisher.PublisherTradeName, publisher.PublisherAFM);
                }
                else
                {
                    reporterDetails = string.Format("{0}<br/>{1}", publisher.PublisherName, publisher.PublisherAFM);
                }
            }
            else if (reporter is Secretary)
            {
                Secretary secretary = (Secretary)reporter;
                var academic = CacheManager.Academics.Get((int)secretary.AcademicReference.GetKey());
                if (academic != null)
                    reporterDetails = string.Format("Ίδρυμα: {0}<br/>Σχολή: {1}<br/>Τμήμα: {2}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));
            }
            else if (reporter is DistributionPoint)
            {
                DistributionPoint distributionPoint = (DistributionPoint)reporter;

                reporterDetails = string.Format("Τίτλος: {0}", distributionPoint.DistributionPointName);
            }
            else if (reporter is PublicationsOffice)
            {
                PublicationsOffice publicationsOffice = (PublicationsOffice)reporter;
                var institution = CacheManager.Institutions.Get(publicationsOffice.InstitutionID);
                if (institution != null)
                    reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }
            else if (reporter is DataCenter)
            {
                DataCenter dataCenter = (DataCenter)reporter;
                var institution = CacheManager.Institutions.Get(dataCenter.InstitutionID);
                if (institution != null)
                    reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }
            else if (reporter is Library)
            {
                Library library = (Library)reporter;
                var institution = CacheManager.Institutions.Get(library.InstitutionID);
                if (institution != null)
                    reporterDetails = string.Format("Ίδρυμα: {0}<br/>Τίτλος: {1}", institution.Name, library.LibraryName);
            }
            else if (reporter is BookSupplier)
            {
                BookSupplier bookSupplier = (BookSupplier)reporter;
                var institution = CacheManager.Institutions.Get(bookSupplier.InstitutionID);
                if (institution != null)
                    reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }

            return reporterDetails;
        }
    }
}