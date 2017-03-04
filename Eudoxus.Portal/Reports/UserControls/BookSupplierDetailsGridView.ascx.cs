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
    public partial class BookSupplierDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvBookSuppliers.DataSourceID;
            }
            set
            {
                gvBookSuppliers.DataSourceID = value;
                gvBookSuppliersExport.DataSourceID = value;
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
            gvBookSuppliers.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            BookSupplierDetailsView bookSupplier = (BookSupplierDetailsView)o;

            if (bookSupplier.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", bookSupplier.VerificationStatus.GetLabel(), bookSupplier.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", bookSupplier.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetInstitutionDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string bookSupplierDetails = string.Empty;

            BookSupplierDetailsView bookSupplier = (BookSupplierDetailsView)o;

            var institution = CacheManager.Institutions.Get(bookSupplier.BookSupplierInstitutionID);
            bookSupplierDetails = string.Format("{0}", institution.Name);

            return bookSupplierDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string bookSupplierDetails = string.Empty;

            BookSupplierDetailsView bookSupplier = (BookSupplierDetailsView)o;

            var city = CacheManager.Cities.Get(bookSupplier.BookSupplierCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(bookSupplier.BookSupplierPrefectureID).Name;

            bookSupplierDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", bookSupplier.BookSupplierAddress, bookSupplier.BookSupplierZipCode, city, prefecture);

            return bookSupplierDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            BookSupplierDetailsView bookSupplier = (BookSupplierDetailsView)o;

            if (!string.IsNullOrEmpty(bookSupplier.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", bookSupplier.ContactName, bookSupplier.ContactPhone, bookSupplier.ContactMobilePhone, bookSupplier.ContactEmail);
            }

            return contactDetails;
        }

        protected string GetAlternateContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string alternateContactDetails = string.Empty;

            BookSupplierDetailsView bookSupplier = (BookSupplierDetailsView)o;

            if (!string.IsNullOrEmpty(bookSupplier.AlternateContactName))
            {
                alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", bookSupplier.AlternateContactName, bookSupplier.AlternateContactPhone, bookSupplier.AlternateContactMobilePhone, bookSupplier.AlternateContactEmail);
            }

            return alternateContactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvBookSuppliersExport.Visible = true;
            gvBookSuppliersExport.PageIndex = 0;
            gvBookSuppliersExport.DataBind();

            gveBookSuppliers.FileName = String.Format("BookSupplierDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveBookSuppliers.WriteXlsToResponse(true);
        }

        protected void gvBookSuppliers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            BookSupplierDetailsView bookSupplier = gvBookSuppliers.GetRow(e.VisibleIndex) as BookSupplierDetailsView;

            if (bookSupplier == null)
                return;

            switch ((bookSupplier.VerificationStatus))
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

        protected void gveBookSuppliers_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            BookSupplierDetailsView bookSupplier = gvBookSuppliersExport.GetRow(e.VisibleIndex) as BookSupplierDetailsView;

            if (bookSupplier == null)
                return;

            switch (bookSupplier.VerificationStatus)
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
                e.Text = bookSupplier.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "Institution")
            {
                e.Text = CacheManager.Institutions.Get(bookSupplier.BookSupplierInstitutionID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "BookSupplierCity")
            {
                e.Text = CacheManager.Cities.Get(bookSupplier.BookSupplierCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "BookSupplierPrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(bookSupplier.BookSupplierPrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}