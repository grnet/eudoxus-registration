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
using Microsoft.Data.Extensions;
using Eudoxus.Portal.UserControls;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class VerifySecretaries : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            if (!Page.IsPostBack)
            {
                txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtCertificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                deCertificationDate.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtSecretaryID.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void ddlInstitutionType_Init(object sender, EventArgs e)
        {
            ddlInstitutionType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enInstitutionType item in Enum.GetValues(typeof(enInstitutionType)))
            {
                ddlInstitutionType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvSecretaries.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvSecretaries.PageIndex = 0;
            gvSecretaries.DataBind();
        }

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvSecretaries.SettingsPager.PageSize;

                gvSecretaries.SettingsPager.PageSize = int.MaxValue;
                gvSecretaries.DataBind();

                if (_secretaries != null && _secretaries.Count > 0)
                {
                    e.EmailRecepients.AddRange(_secretaries.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_secretaries.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_secretaries.Select(x => x.SecretaryDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά γραμματείες.",
                        e.EmailRecepients.Count(),
                        _secretaries.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvSecretaries.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<Secretary> _secretaries = null;
        protected void odsSecretaries_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<Secretary>)
                _secretaries = e.ReturnValue as List<Secretary>;
        }

        protected void odsSecretaries_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<Secretary> criteria = new Criteria<Secretary>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("SecretaryDetails");

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

            if (!string.IsNullOrEmpty(txtCertificationNumber.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.CertificationNumber, Convert.ToInt32(txtCertificationNumber.Text.ToNull()));
            }

            if (deCertificationDate.Value != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.CertificationDate, deCertificationDate.Date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                criteria.Expression = criteria.Expression.Where(x => x.CertificationDate, deCertificationDate.Date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
            }

            int secretaryID;
            if (int.TryParse(txtSecretaryID.Text, out secretaryID) && secretaryID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, secretaryID);
            }

            int schoolCode;
            if (int.TryParse(hfSchoolCode.Value, out schoolCode))
            {
                criteria.Expression = criteria.Expression.Where(x => x.AcademicID, schoolCode);
            }

            int institutionType;
            if (int.TryParse(ddlInstitutionType.SelectedItem.Value, out institutionType) && institutionType >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Academic.InstitutionRef.InstitutionTypeInt, institutionType);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvSecretaries_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            Secretary secretary = (Secretary)gvSecretaries.GetRow(e.VisibleIndex);

            if (secretary != null)
            {
                switch (secretary.VerificationStatus)
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

        protected string GetSecretaryDetails(Secretary secretary)
        {
            if (secretary == null)
                return string.Empty;

            string secretaryDetails = string.Empty;

            var academic = CacheManager.Academics.Get((int)secretary.AcademicReference.GetKey());

            if (academic != null)
            {
                secretaryDetails = string.Format("Ίδρυμα: {0}<br/>Σχολή: {1}<br/>Τμήμα: {2}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));
            }

            return secretaryDetails;
        }
    }
}