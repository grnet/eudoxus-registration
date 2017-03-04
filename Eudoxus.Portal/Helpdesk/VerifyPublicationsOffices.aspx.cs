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
    public partial class VerifyPublicationsOffices : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (var item in CacheManager.Institutions.GetItems())
            {
                ddlInstitution.Items.Add(new ListItem(item.Name, item.ID.ToString()));
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

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvPublicationsOffices.SettingsPager.PageSize;

                gvPublicationsOffices.SettingsPager.PageSize = int.MaxValue;
                gvPublicationsOffices.DataBind();

                if (_publicationsOffices != null && _publicationsOffices.Count > 0)
                {
                    e.EmailRecepients.AddRange(_publicationsOffices.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_publicationsOffices.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_publicationsOffices.Select(x => x.PublicationsOfficeDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά γραφεία.",
                        e.EmailRecepients.Count(),
                        _publicationsOffices.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvPublicationsOffices.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<PublicationsOffice> _publicationsOffices = null;
        protected void odsPublicationsOffices_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<PublicationsOffice>)
                _publicationsOffices = e.ReturnValue as List<PublicationsOffice>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvPublicationsOffices.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvPublicationsOffices.PageIndex = 0;
            gvPublicationsOffices.DataBind();
        }

        protected void odsPublicationsOffices_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<PublicationsOffice> criteria = new Criteria<PublicationsOffice>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("PublicationsOfficeDetails");

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

            int institution;
            if (int.TryParse(ddlInstitution.SelectedItem.Value, out institution) && institution > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.InstitutionID, institution);
            }

            int institutionType;
            if (int.TryParse(ddlInstitutionType.SelectedItem.Value, out institutionType) && institutionType >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Institution.InstitutionTypeInt, institutionType);
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

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvPublicationsOffices_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            PublicationsOffice publicationsOffice = (PublicationsOffice)gvPublicationsOffices.GetRow(e.VisibleIndex);

            if (publicationsOffice != null)
            {
                switch (publicationsOffice.VerificationStatus)
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

        protected string GetPublicationsOfficeDetails(PublicationsOffice publicationsOffice)
        {
            if (publicationsOffice == null)
                return string.Empty;

            string publicationsOfficeDetails = string.Empty;

            var institution = CacheManager.Institutions.Get((int)publicationsOffice.InstitutionID);

            if (institution != null)
            {
                publicationsOfficeDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }

            return publicationsOfficeDetails;
        }
    }
}