using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;

namespace Eudoxus.Portal.Reports.UserControls
{
    public partial class SecretaryDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvSecretaries.DataSourceID;
            }
            set
            {
                gvSecretaries.DataSourceID = value;
                gvSecretariesExport.DataSourceID = value;
            }
        }

        public bool EnableExport
        {
            get;
            set;
        }

        #endregion

        #region [ Overrides ]

        public override void DataBind()
        {
            gvSecretaries.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            if (secretary.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", secretary.VerificationStatus.GetLabel(), secretary.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", secretary.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetAcademicDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string secretaryDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            var academic = CacheManager.Academics.Get(secretary.SecretaryAcademicID);
            secretaryDetails = string.Format("Ίδρυμα: {0}<br/>Σχολή: {1}<br/>Τμήμα: {2}<br/>Εξάμηνα Σπουδών: {3}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"), secretary.Semesters);

            return secretaryDetails;
        }

        protected string GetSecretaryInfoDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", secretary.SecretaryPhone, secretary.SecretaryEmail, ((enRepresentativeType)secretary.RepresentativeType).GetLabel(), secretary.RepresentativeName);

            return contactDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string secretaryDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            var city = CacheManager.Cities.Get(secretary.SecretaryCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(secretary.SecretaryPrefectureID).Name;

            secretaryDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", secretary.SecretaryAddress, secretary.SecretaryZipCode, city, prefecture);

            return secretaryDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            if (!string.IsNullOrEmpty(secretary.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", secretary.ContactName, secretary.ContactPhone, secretary.ContactMobilePhone, secretary.ContactEmail);
            }

            return contactDetails;
        }

        protected string GetAlternateContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string alternateContactDetails = string.Empty;

            SecretaryDetailsView secretary = (SecretaryDetailsView)o;

            if (!string.IsNullOrEmpty(secretary.AlternateContactName))
            {
                alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", secretary.AlternateContactName, secretary.AlternateContactPhone, secretary.AlternateContactMobilePhone, secretary.AlternateContactEmail);
            }

            return alternateContactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvSecretariesExport.Visible = true;
            gvSecretariesExport.PageIndex = 0;
            gvSecretariesExport.DataBind();

            gveSecretaries.FileName = String.Format("SecretaryDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveSecretaries.WriteXlsToResponse(true);
        }

        protected void gvSecretaries_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            SecretaryDetailsView secretary = gvSecretaries.GetRow(e.VisibleIndex) as SecretaryDetailsView;

            if (secretary == null)
                return;

            switch ((secretary.VerificationStatus))
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

        protected void gveSecretaries_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            SecretaryDetailsView secretary = gvSecretariesExport.GetRow(e.VisibleIndex) as SecretaryDetailsView;

            if (secretary == null)
                return;

            switch (secretary.VerificationStatus)
            {
                case enVerificationStatus.NotVerified:
                    e.BrickStyle.BackColor = Color.DarkGray;
                    break;
                case enVerificationStatus.Verified:
                    e.BrickStyle.BackColor = Color.LightGreen;
                    break;
                case enVerificationStatus.CannotBeVerified:
                    e.BrickStyle.BackColor = Color.Tomato;
                    break;
                default:
                    break;
            }

            if (e.Column.Name == "VerificationStatus")
            {
                e.Text = secretary.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "Institution")
            {
                e.Text = CacheManager.Academics.Get(secretary.SecretaryAcademicID).Institution;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "School")
            {
                e.Text = CacheManager.Academics.Get(secretary.SecretaryAcademicID).School;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "Department")
            {
                e.Text = CacheManager.Academics.Get(secretary.SecretaryAcademicID).Department;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "RepresentativeType")
            {
                e.Text = secretary.RepresentativeType.GetLabel();
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "SecretaryCity")
            {
                e.Text = CacheManager.Cities.Get(secretary.SecretaryCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "SecretaryPrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(secretary.SecretaryPrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}