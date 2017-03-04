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
    public partial class VerifyMinistryPaymentsUsers : BaseEntityPortalPage<object>
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
                int _pageSize = gvMinistryPaymentsUsers.SettingsPager.PageSize;

                gvMinistryPaymentsUsers.SettingsPager.PageSize = int.MaxValue;
                gvMinistryPaymentsUsers.DataBind();

                if (_MinistryPaymentsUsers != null && _MinistryPaymentsUsers.Count > 0)
                {
                    e.EmailRecepients.AddRange(_MinistryPaymentsUsers.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_MinistryPaymentsUsers.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά μέλη της επιτροπής.",
                        e.EmailRecepients.Count(),
                        _MinistryPaymentsUsers.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvMinistryPaymentsUsers.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<MinistryPaymentsUser> _MinistryPaymentsUsers = null;
        protected void odsMinistryPaymentsUsers_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<MinistryPaymentsUser>)
                _MinistryPaymentsUsers = e.ReturnValue as List<MinistryPaymentsUser>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvMinistryPaymentsUsers.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvMinistryPaymentsUsers.PageIndex = 0;
            gvMinistryPaymentsUsers.DataBind();
        }

        protected void odsMinistryPaymentsUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<MinistryPaymentsUser> criteria = new Criteria<MinistryPaymentsUser>();

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

            int MinistryPaymentsUserID;
            if (int.TryParse(txtMinistryPaymentsUserID.Text, out MinistryPaymentsUserID) && MinistryPaymentsUserID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, MinistryPaymentsUserID);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvMinistryPaymentsUsers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            MinistryPaymentsUser ministryPaymentsUser = (MinistryPaymentsUser)gvMinistryPaymentsUsers.GetRow(e.VisibleIndex);

            if (ministryPaymentsUser != null)
            {
                switch (ministryPaymentsUser.VerificationStatus)
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

        protected string GetMinistryPaymentsUserDetails(MinistryPaymentsUser ministryPaymentsUser)
        {
            if (ministryPaymentsUser == null)
                return string.Empty;

            string ministryPaymentsUserDetails = string.Empty;

            ministryPaymentsUserDetails = string.Format("Ον/μο: {0}<br/>Ιδιότητα: {1}<br/>Εξουσιοδότηση: {2}", ministryPaymentsUser.ContactName, ministryPaymentsUser.Description, ministryPaymentsUser.MinistryAuthorization.GetLabel());

            return ministryPaymentsUserDetails;
        }

        public object ministryPaymentsUser { get; set; }
    }
}