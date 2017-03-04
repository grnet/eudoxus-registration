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
    public partial class LibraryDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvLibraries.DataSourceID;
            }
            set
            {
                gvLibraries.DataSourceID = value;
                gvLibrariesExport.DataSourceID = value;
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
            gvLibraries.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            if (library.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", library.VerificationStatus.GetLabel(), library.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", library.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetInstitutionDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string libraryDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            var institution = CacheManager.Institutions.Get(library.LibraryInstitutionID);
            libraryDetails = string.Format("{0}<br/>{1}", institution.Name, library.LibraryName);

            return libraryDetails;
        }

        protected string GetLibraryInfoDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            contactDetails = string.Format("{0}<br/>{1}<br/>{2}", library.LibraryPhone, library.LibraryEmail, library.DirectorName);

            return contactDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string libraryDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            var city = CacheManager.Cities.Get(library.LibraryCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(library.LibraryPrefectureID).Name;

            libraryDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", library.LibraryAddress, library.LibraryZipCode, city, prefecture);

            return libraryDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            if (!string.IsNullOrEmpty(library.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", library.ContactName, library.ContactPhone, library.ContactMobilePhone, library.ContactEmail);
            }

            return contactDetails;
        }

        protected string GetAlternateContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string alternateContactDetails = string.Empty;

            LibraryDetailsView library = (LibraryDetailsView)o;

            if (!string.IsNullOrEmpty(library.AlternateContactName))
            {
                alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", library.AlternateContactName, library.AlternateContactPhone, library.AlternateContactMobilePhone, library.AlternateContactEmail);
            }

            return alternateContactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvLibrariesExport.Visible = true;
            gvLibrariesExport.PageIndex = 0;
            gvLibrariesExport.DataBind();

            gveLibraries.FileName = String.Format("LibraryDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveLibraries.WriteXlsToResponse(true);
        }

        protected void gvLibraries_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            LibraryDetailsView library = gvLibraries.GetRow(e.VisibleIndex) as LibraryDetailsView;

            if (library == null)
                return;

            switch ((library.VerificationStatus))
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

        protected void gveLibraries_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            LibraryDetailsView library = gvLibrariesExport.GetRow(e.VisibleIndex) as LibraryDetailsView;

            if (library == null)
                return;

            switch (library.VerificationStatus)
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
                e.Text = library.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "Institution")
            {
                e.Text = CacheManager.Institutions.Get(library.LibraryInstitutionID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "LibraryCity")
            {
                e.Text = CacheManager.Cities.Get(library.LibraryCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "LibraryPrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(library.LibraryPrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}