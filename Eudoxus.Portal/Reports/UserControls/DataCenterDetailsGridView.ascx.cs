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
    public partial class DataCenterDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvDataCenters.DataSourceID;
            }
            set
            {
                gvDataCenters.DataSourceID = value;
                gvDataCentersExport.DataSourceID = value;
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
            gvDataCenters.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            if (dataCenter.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", dataCenter.VerificationStatus.GetLabel(), dataCenter.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", dataCenter.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetInstitutionDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string dataCenterDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            var institution = CacheManager.Institutions.Get(dataCenter.DataCenterInstitutionID);
            dataCenterDetails = string.Format("{0}", institution.Name);

            return dataCenterDetails;
        }

        protected string GetDataCenterInfoDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            contactDetails = string.Format("{0}<br/>{1}<br/>{2}", dataCenter.DataCenterPhone, dataCenter.DataCenterEmail, dataCenter.DirectorName);

            return contactDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string dataCenterDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            var city = CacheManager.Cities.Get(dataCenter.DataCenterCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(dataCenter.DataCenterPrefectureID).Name;

            dataCenterDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", dataCenter.DataCenterAddress, dataCenter.DataCenterZipCode, city, prefecture);

            return dataCenterDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            if (!string.IsNullOrEmpty(dataCenter.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", dataCenter.ContactName, dataCenter.ContactPhone, dataCenter.ContactMobilePhone, dataCenter.ContactEmail);
            }

            return contactDetails;
        }

        protected string GetAlternateContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string alternateContactDetails = string.Empty;

            DataCenterDetailsView dataCenter = (DataCenterDetailsView)o;

            if (!string.IsNullOrEmpty(dataCenter.AlternateContactName))
            {
                alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", dataCenter.AlternateContactName, dataCenter.AlternateContactPhone, dataCenter.AlternateContactMobilePhone, dataCenter.AlternateContactEmail);
            }

            return alternateContactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvDataCentersExport.Visible = true;
            gvDataCentersExport.PageIndex = 0;
            gvDataCentersExport.DataBind();

            gveDataCenters.FileName = String.Format("DataCenterDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveDataCenters.WriteXlsToResponse(true);
        }

        protected void gvDataCenters_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            DataCenterDetailsView dataCenter = gvDataCenters.GetRow(e.VisibleIndex) as DataCenterDetailsView;

            if (dataCenter == null)
                return;

            switch ((dataCenter.VerificationStatus))
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

        protected void gveDataCenters_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            DataCenterDetailsView dataCenter = gvDataCentersExport.GetRow(e.VisibleIndex) as DataCenterDetailsView;

            if (dataCenter == null)
                return;

            switch (dataCenter.VerificationStatus)
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
                e.Text = dataCenter.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "Institution")
            {
                e.Text = CacheManager.Institutions.Get(dataCenter.DataCenterInstitutionID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "DataCenterCity")
            {
                e.Text = CacheManager.Cities.Get(dataCenter.DataCenterCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "DataCenterPrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(dataCenter.DataCenterPrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}