using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using Eudoxus.BusinessModel;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Data;
using System.Text;

namespace Eudoxus.Portal.Reports.UserControls
{
    public partial class RegisteredPublishersDetails : System.Web.UI.UserControl
    {
        public enum enRegisteredPublishersDetails
        {
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvPublishersExport.Visible = true;
            gvPublishersExport.PageIndex = 0;
            gvPublishersExport.DataBind();

            gvePublishers.FileName = String.Format("RegisteredPublishers_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gvePublishers.WriteXlsToResponse(true);
        }

        protected void sdsPublishers_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM [vRegisteredPublishers] ");

            int rType;
            if (int.TryParse(Request.QueryString["t"], out rType) && rType > 0)
            {
                switch ((enRegisteredPublishersDetails)rType)
                {
                    case enRegisteredPublishersDetails.RegistrationsByDate:
                        DateTime registrationDate = DateTime.Parse(Request.QueryString["registrationDate"]);

                        sb.Append("WHERE RegistrationDate > '" + string.Format("{0:yyyy-MM-dd}", registrationDate) + "' AND RegistrationDate < '" + string.Format("{0:yyyy-MM-dd}", registrationDate.AddDays(1)) + "'");

                        sb.Append(" ORDER BY RegistrationDate");
                        break;
                    case enRegisteredPublishersDetails.VerificationsByDate:
                        DateTime verificationDate = DateTime.Parse(Request.QueryString["verificationDate"]);

                        sb.Append("WHERE VerificationStatus = 1 AND VerificationDate > '" + string.Format("{0:yyyy-MM-dd}", verificationDate) + "' AND VerificationDate < '" + string.Format("{0:yyyy-MM-dd}", verificationDate.AddDays(1)) + "'");

                        sb.Append(" ORDER BY VerificationDate");

                        break;
                    default:
                        break;
                }
            }

            e.Command.CommandText = sb.ToString();
        }

        protected void gvPublishers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            DataRowView row = gvPublishers.GetRow(e.VisibleIndex) as DataRowView;

            if (row != null)
            {
                switch ((enVerificationStatus)row["VerificationStatus"])
                {
                    case enVerificationStatus.NotVerified:
                        e.Row.BackColor = Color.DarkGray;
                        break;
                    case enVerificationStatus.Verified:
                        if ((bool)row["IsActive"])
                        {
                            e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            e.Row.BackColor = Color.Yellow;
                        }
                        break;
                    case enVerificationStatus.CannotBeVerified:
                        e.Row.BackColor = Color.Tomato;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void gvePublishers_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            if (e.Column.FieldName == "VerificationStatus")
            {
                switch ((enVerificationStatus)Convert.ToInt32(e.Value))
                {
                    case enVerificationStatus.NotVerified:
                        e.Text = "ΟΧΙ";
                        e.BrickStyle.BackColor = Color.DarkGray;
                        break;
                    case enVerificationStatus.Verified:
                        e.Text = "ΝΑΙ";
                        e.BrickStyle.BackColor = Color.LightGreen;
                        break;
                    case enVerificationStatus.CannotBeVerified:
                        e.Text = "Δεν μπορεί να πιστοποιηθεί";
                        e.BrickStyle.BackColor = Color.Tomato;
                        break;
                    default:
                        break;
                }

                e.TextValue = e.Text;
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
            else if (e.Column.FieldName == "IsActive")
            {
                if (Convert.ToBoolean(e.Value))
                {
                    e.Text = "ΝΑΙ";
                    e.BrickStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    e.Text = "ΟΧΙ";
                    e.BrickStyle.BackColor = Color.Yellow;
                }

                e.TextValue = e.Text;
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
            else if (e.Column.FieldName == "PublisherType")
            {
                e.Text = ((enPublisherType)Convert.ToInt32(e.Value)).GetLabel();

                e.TextValue = e.Text;
            }
            else if (e.Column.FieldName == "HasLogisticBooks")
            {
                if (string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Text = "Δεν έχει οριστεί";
                }
                else
                {
                    var hasLogisticBooks = Convert.ToBoolean(e.Value);

                    if (hasLogisticBooks)
                    {
                        e.Text = "ΝΑΙ";
                    }
                    else
                    {
                        e.Text = "ΟΧΙ";
                    }
                }

                e.TextValue = e.Text;
            }
        }

        protected string GetPublisherType(object p)
        {
            WebDataRow row = p as WebDataRow;

            if (row != null)
            {
                string publisherType = string.Empty;

                publisherType = string.Format("{0}", ((enPublisherType)row["PublisherType"]).GetLabel());

                if (((enPublisherType)row["PublisherType"]) == enPublisherType.SelfPublisher)
                {
                    publisherType += "<br/>Υπόχρεος Τήρησης Λογιστικών Βιβλίων: ";

                    if (string.IsNullOrEmpty(row["HasLogisticBooks"].ToString()))
                    {
                        publisherType += "Δεν έχει οριστεί";
                    }
                    else
                    {
                        bool hasLogisticBooks;

                        bool.TryParse(row["HasLogisticBooks"].ToString(), out hasLogisticBooks);

                        if (hasLogisticBooks)
                        {
                            publisherType += "ΝΑΙ";
                        }
                        else
                        {
                            publisherType += "ΟΧΙ";
                        }
                    }
                }

                return publisherType;
            }

            return string.Empty;
        }

        protected string GetPublisherDetails(object p)
        {
            WebDataRow row = p as WebDataRow;

            if (row != null)
            {
                string publisherDetails = string.Empty;

                if (!string.IsNullOrEmpty(row["PublisherTradeName"].ToString()))
                {
                    publisherDetails = string.Format("{0}<br/>{1}<br/>{2}", row["PublisherName"], row["PublisherTradeName"], row["PublisherAFM"]);
                }
                else
                {
                    publisherDetails = string.Format("{0}<br/>{1}", row["PublisherName"], row["PublisherAFM"]);
                }

                return publisherDetails;
            }

            return string.Empty;
        }

        protected string GetLegalPersonDetails(object p)
        {
            WebDataRow row = p as WebDataRow;

            if (row != null)
            {
                string legalPersonDetails = string.Empty;

                if (((enPublisherType)row["PublisherType"]) == enPublisherType.LegalPerson)
                {
                    legalPersonDetails = string.Format("{0}<br/>{1}<br/>{2}", row["LegalPersonName"], row["LegalPersonPhone"], row["LegalPersonEmail"]);
                }

                return legalPersonDetails;
            }

            return string.Empty;
        }

        protected string GetAlternateContactDetails(object p)
        {
            WebDataRow row = p as WebDataRow;

            if (row != null)
            {
                string alternateContactDetails = string.Empty;

                if (!string.IsNullOrEmpty(row["AlternateContactName"].ToString()))
                {
                    alternateContactDetails = string.Format("{0}<br/>{1}<br/>{2}<br/>{3}", row["AlternateContactName"], row["AlternateContactPhone"], row["AlternateContactMobilePhone"], row["AlternateContactEmail"]);
                }

                return alternateContactDetails;
            }

            return string.Empty;
        }
    }
}