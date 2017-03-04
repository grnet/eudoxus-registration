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
    public partial class VerifyPricingCommittees : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvPricingCommittees.SettingsPager.PageSize;

                gvPricingCommittees.SettingsPager.PageSize = int.MaxValue;
                gvPricingCommittees.DataBind();

                if (_pricingCommittees != null && _pricingCommittees.Count > 0)
                {
                    e.EmailRecepients.AddRange(_pricingCommittees.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_pricingCommittees.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά μέλη της επιτροπής.",
                        e.EmailRecepients.Count(),
                        _pricingCommittees.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvPricingCommittees.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<PricingCommittee> _pricingCommittees = null;
        protected void odsPricingCommittees_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<PricingCommittee>)
                _pricingCommittees = e.ReturnValue as List<PricingCommittee>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvPricingCommittees.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvPricingCommittees.PageIndex = 0;
            gvPricingCommittees.DataBind();
        }

        protected void odsPricingCommittees_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<PricingCommittee> criteria = new Criteria<PricingCommittee>();

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

            int ministryAuthorization;
            if (int.TryParse(ddlMinistryAuthorization.SelectedItem.Value, out ministryAuthorization) && ministryAuthorization >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.MinistryAuthorizationInt, ministryAuthorization);
            }

            int pricingCommitteeID;
            if (int.TryParse(txtPricingCommitteeID.Text, out pricingCommitteeID) && pricingCommitteeID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, pricingCommitteeID);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvPricingCommittees_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            PricingCommittee pricingCommittee = (PricingCommittee)gvPricingCommittees.GetRow(e.VisibleIndex);

            if (pricingCommittee != null)
            {
                switch (pricingCommittee.VerificationStatus)
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

        protected string GetPricingCommitteeDetails(PricingCommittee pricingCommittee)
        {
            if (pricingCommittee == null)
                return string.Empty;

            string pricingCommitteeDetails = string.Empty;

            pricingCommitteeDetails = string.Format("Ον/μο: {0}<br/>Ιδιότητα: {1}<br/>Εξουσιοδότηση: {2}", pricingCommittee.ContactName, pricingCommittee.PricingCommitteeType, pricingCommittee.MinistryAuthorization.GetLabel());

            return pricingCommitteeDetails;
        }
    }
}