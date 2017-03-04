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
    public partial class VerifyDataCenters : BaseEntityPortalPage<object>
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
                int _pageSize = gvDataCenters.SettingsPager.PageSize;

                gvDataCenters.SettingsPager.PageSize = int.MaxValue;
                gvDataCenters.DataBind();

                if (_dataCenters != null && _dataCenters.Count > 0)
                {
                    e.EmailRecepients.AddRange(_dataCenters.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_dataCenters.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_dataCenters.Select(x => x.DataCenterDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά γραφεία.",
                        e.EmailRecepients.Count(),
                        _dataCenters.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvDataCenters.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<DataCenter> _dataCenters = null;
        protected void odsDataCenters_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<DataCenter>)
                _dataCenters = e.ReturnValue as List<DataCenter>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvDataCenters.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvDataCenters.PageIndex = 0;
            gvDataCenters.DataBind();
        }

        protected void odsDataCenters_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<DataCenter> criteria = new Criteria<DataCenter>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("DataCenterDetails");

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

        protected void gvDataCenters_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            DataCenter dataCenter = (DataCenter)gvDataCenters.GetRow(e.VisibleIndex);

            if (dataCenter != null)
            {
                switch (dataCenter.VerificationStatus)
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

        protected string GetDataCenterDetails(DataCenter dataCenter)
        {
            if (dataCenter == null)
                return string.Empty;

            string dataCenterDetails = string.Empty;

            var institution = CacheManager.Institutions.Get((int)dataCenter.InstitutionID);

            if (institution != null)
            {
                dataCenterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }

            return dataCenterDetails;
        }
    }
}