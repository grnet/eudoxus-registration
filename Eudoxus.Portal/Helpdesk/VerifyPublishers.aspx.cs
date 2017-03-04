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
using Eudoxus.Portal.UserControls;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class VerifyPublishers : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPublisherAFM.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtPublisherName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtPublisherID.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtCertificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                deCertificationDate.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void ddlPublisherType_Init(object sender, EventArgs e)
        {
            ddlPublisherType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enPublisherType item in Enum.GetValues(typeof(enPublisherType)))
            {
                ddlPublisherType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvPublishers.SettingsPager.PageSize;

                gvPublishers.SettingsPager.PageSize = int.MaxValue;
                gvPublishers.DataBind();

                if (_publishers != null && _publishers.Count > 0)
                {
                    e.EmailRecepients.AddRange(_publishers.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_publishers.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_publishers.Select(x => x.PublisherDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά εκδότες.",
                        e.EmailRecepients.Count(),
                        _publishers.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvPublishers.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<Publisher> _publishers = null;
        protected void odsPublishers_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<Publisher>)
                _publishers = e.ReturnValue as List<Publisher>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvPublishers.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvPublishers.PageIndex = 0;
            gvPublishers.DataBind();
        }

        protected void odsPublishers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<Publisher> criteria = new Criteria<Publisher>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("PublisherDetails");

            int verificationStatus;
            if (int.TryParse(ddlVerificationStatus.SelectedItem.Value, out verificationStatus) && verificationStatus >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, (enVerificationStatus)verificationStatus);
            }

            int activationStatus;
            if (int.TryParse(ddlActivationStatus.SelectedItem.Value, out activationStatus) && activationStatus >= 0)
            {
                if (activationStatus == 0)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsActivated, false);
                }
                else if (activationStatus == 1)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsActivated, true);
                }
            }

            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.UserName, txtUserName.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
            }

            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.Email, txtEmail.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
            }

            int publisherType;
            if (int.TryParse(ddlPublisherType.SelectedItem.Value, out publisherType) && publisherType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.PublisherType, (enPublisherType)publisherType);
            }

            int publisherID;
            if (int.TryParse(txtPublisherID.Text, out publisherID) && publisherID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, publisherID);
            }

            if (!string.IsNullOrEmpty(txtPublisherAFM.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.PublisherAFM, txtPublisherAFM.Text.ToNull());
            }

            if (!string.IsNullOrEmpty(txtPublisherName.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.PublisherName, txtPublisherName.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
            }

            if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.CertificationNumber, Convert.ToInt32(txtCertificationNumber.Text.ToNull()));
            }

            if (deCertificationDate.Value != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.CertificationDate, deCertificationDate.Date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                criteria.Expression = criteria.Expression.Where(x => x.CertificationDate, deCertificationDate.Date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
            }

            int isActive;
            if (int.TryParse(ddlActive.SelectedItem.Value, out isActive) && isActive >= 0)
            {
                if (isActive == 0)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsActive, false);
                }
                else if (isActive == 1)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsActive, true);
                }
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvPublishers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            Publisher publisher = (Publisher)gvPublishers.GetRow(e.VisibleIndex);

            if (publisher != null)
            {
                switch (publisher.VerificationStatus)
                {
                    case enVerificationStatus.NotVerified:
                        e.Row.BackColor = Color.DarkGray;
                        break;
                    case enVerificationStatus.Verified:
                        if (publisher.IsActive)
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

        protected string GetPublisherDetails(Publisher publisher)
        {
            if (publisher == null)
                return string.Empty;

            string publisherDetails = string.Empty;

            if (!string.IsNullOrEmpty(publisher.PublisherTradeName))
            {
                publisherDetails = string.Format("{0} <br/>{1} <br/>{2}", publisher.PublisherName, publisher.PublisherTradeName, publisher.PublisherAFM);
            }
            else
            {
                publisherDetails = string.Format("{0} <br/>{1}", publisher.PublisherName, publisher.PublisherAFM);
            }

            return publisherDetails;
        }
    }
}