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
    public partial class DistributionPointDetailsGridView : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs> RenderBrick;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvDistributionPoints.DataSourceID;
            }
            set
            {
                gvDistributionPoints.DataSourceID = value;
                gvDistributionPointsExport.DataSourceID = value;
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
            gvDistributionPoints.DataBind();
        }

        #endregion

        #region [ Gridview Methods ]

        protected string GetVerificationDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string verificationDetails = string.Empty;

            DistributionPointDetailsView distributionPoint = (DistributionPointDetailsView)o;

            if (distributionPoint.VerificationStatus == enVerificationStatus.Verified)
            {
                verificationDetails = string.Format("{0}<br/>{1:dd/MM/yyyy}", distributionPoint.VerificationStatus.GetLabel(), distributionPoint.VerificationDate);
            }
            else
            {
                verificationDetails = string.Format("{0}", distributionPoint.VerificationStatus.GetLabel());
            }

            return verificationDetails;
        }

        protected string GetDistributionPointTypeDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string distributionPointTypeDetails = string.Empty;

            DistributionPointDetailsView distributionPoint = (DistributionPointDetailsView)o;

            if (distributionPoint.DistributionPointType == enDistributionPointType.Institution)
            {
                distributionPointTypeDetails = string.Format("{0}<br/>{1}", distributionPoint.DistributionPointType.GetLabel(), CacheManager.Institutions.Get((int)distributionPoint.DistributionPointInstitutionID).Name);
            }
            else
            {
                distributionPointTypeDetails = string.Format("{0}", distributionPoint.DistributionPointType.GetLabel());
            }

            return distributionPointTypeDetails;
        }

        protected string GetDistributionPointDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string distributionPointDetails = string.Empty;

            DistributionPointDetailsView distributionPoint = (DistributionPointDetailsView)o;

            distributionPointDetails = string.Format("{0}<br/>{1}<br/>{2}", distributionPoint.DistributionPointName, distributionPoint.DistributionPointPhone, distributionPoint.DistributionPointEmail);

            return distributionPointDetails;
        }

        protected string GetAddressDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string distributionPointDetails = string.Empty;

            DistributionPointDetailsView distributionPoint = (DistributionPointDetailsView)o;

            var city = CacheManager.Cities.Get(distributionPoint.DistributionPointCityID).Name;
            var prefecture = CacheManager.Prefectures.Get(distributionPoint.DistributionPointPrefectureID).Name;

            distributionPointDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", distributionPoint.DistributionPointAddress, distributionPoint.DistributionPointZipCode, city, prefecture);

            return distributionPointDetails;
        }

        protected string GetContactDetails(object o)
        {
            if (o == null)
                return string.Empty;

            string contactDetails = string.Empty;

            DistributionPointDetailsView distributionPoint = (DistributionPointDetailsView)o;

            if (!string.IsNullOrEmpty(distributionPoint.ContactName))
            {
                contactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", distributionPoint.ContactName, distributionPoint.ContactPhone, distributionPoint.ContactMobilePhone, distributionPoint.ContactEmail);
            }

            return contactDetails;
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvDistributionPointsExport.Visible = true;
            gvDistributionPointsExport.PageIndex = 0;
            gvDistributionPointsExport.DataBind();

            gveDistributionPoints.FileName = String.Format("DistributionPointDetails_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveDistributionPoints.WriteXlsToResponse(true);
        }

        protected void gvDistributionPoints_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            DistributionPointDetailsView distributionPoint = gvDistributionPoints.GetRow(e.VisibleIndex) as DistributionPointDetailsView;

            if (distributionPoint == null)
                return;

            switch ((distributionPoint.VerificationStatus))
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

        protected void gveDistributionPoints_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            DistributionPointDetailsView distributionPoint = gvDistributionPointsExport.GetRow(e.VisibleIndex) as DistributionPointDetailsView;

            if (distributionPoint == null)
                return;

            switch (distributionPoint.VerificationStatus)
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
                e.Text = distributionPoint.VerificationStatus.GetLabel();
            }
            else if (e.Column.Name == "DistributionPointType")
            {
                e.Text = distributionPoint.DistributionPointType.GetLabel();
            }
            else if (e.Column.Name == "DistributionPointInstitution")
            {
                if (distributionPoint.DistributionPointType == enDistributionPointType.Institution)
                {
                    e.Text = CacheManager.Institutions.Get((int)distributionPoint.DistributionPointInstitutionID).Name;
                    e.TextValue = e.Text;
                }
            }
            else if (e.Column.Name == "DistributionPointCity")
            {
                e.Text = CacheManager.Cities.Get(distributionPoint.DistributionPointCityID).Name;
                e.TextValue = e.Text;
            }
            else if (e.Column.Name == "DistributionPointPrefecture")
            {
                e.Text = CacheManager.Prefectures.Get(distributionPoint.DistributionPointPrefectureID).Name;
                e.TextValue = e.Text;
            }

            if (RenderBrick != null)
                RenderBrick(this, e);
        }
    }
}