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
    public partial class PublicationsOfficeDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvPublicationsOffices.DataSourceID;
            }
            set
            {
                gvPublicationsOffices.DataSourceID = value;
                gvPublicationsOfficesExport.DataSourceID = value;
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
            gvPublicationsOffices.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            if (publicationsOffice.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", publicationsOffice.VerificationStatus.GetLabel(), publicationsOffice.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", publicationsOffice.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetInstitutionDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string publicationsOfficeDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            var institution = CacheManager.Institutions.Get(publicationsOffice.PublicationsOfficeInstitutionID);
            publicationsOfficeDetails = string.Format("{0}", institution.Name);

            return publicationsOfficeDetails;
        }

        protected string GetPublicationsOfficeInfoDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            contactDetails = string.Format("{0}<br/>{1}<br/>{2}", publicationsOffice.PublicationsOfficePhone, publicationsOffice.PublicationsOfficeEmail, publicationsOffice.DirectorName);

            return contactDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string publicationsOfficeDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            var city = CacheManager.Cities.Get(publicationsOffice.PublicationsOfficeCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(publicationsOffice.PublicationsOfficePrefectureID).Name;

            publicationsOfficeDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", publicationsOffice.PublicationsOfficeAddress, publicationsOffice.PublicationsOfficeZipCode, city, prefecture);

            return publicationsOfficeDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            if (!string.IsNullOrEmpty(publicationsOffice.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", publicationsOffice.ContactName, publicationsOffice.ContactPhone, publicationsOffice.ContactMobilePhone, publicationsOffice.ContactEmail);
            }

            return contactDetails;
        }

        protected string GetAlternateContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string alternateContactDetails = string.Empty;

            PublicationsOfficeDetailsView publicationsOffice = (PublicationsOfficeDetailsView)o;

            if (!string.IsNullOrEmpty(publicationsOffice.AlternateContactName))
            {
                alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", publicationsOffice.AlternateContactName, publicationsOffice.AlternateContactPhone, publicationsOffice.AlternateContactMobilePhone, publicationsOffice.AlternateContactEmail);
            }

            return alternateContactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvPublicationsOfficesExport.Visible = true;
            gvPublicationsOfficesExport.PageIndex = 0;
            gvPublicationsOfficesExport.DataBind();

            gvePublicationsOffices.FileName = String.Format("PublicationsOfficeDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gvePublicationsOffices.WriteXlsToResponse(true);
        }

        protected void gvPublicationsOffices_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            PublicationsOfficeDetailsView publicationsOffice = gvPublicationsOffices.GetRow(e.VisibleIndex) as PublicationsOfficeDetailsView;

            if (publicationsOffice == null)
                return;

            switch ((publicationsOffice.VerificationStatus))
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

        protected void gvePublicationsOffices_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            PublicationsOfficeDetailsView publicationsOffice = gvPublicationsOfficesExport.GetRow(e.VisibleIndex) as PublicationsOfficeDetailsView;

            if (publicationsOffice == null)
                return;

            switch (publicationsOffice.VerificationStatus)
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
                e.Text = publicationsOffice.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "Institution")
            {
                e.Text = CacheManager.Institutions.Get(publicationsOffice.PublicationsOfficeInstitutionID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "PublicationsOfficeCity")
            {
                e.Text = CacheManager.Cities.Get(publicationsOffice.PublicationsOfficeCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "PublicationsOfficePrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(publicationsOffice.PublicationsOfficePrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}