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
    public partial class VerifyDistributionPoints : BaseEntityPortalPage<object>
    {
        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (var item in CacheManager.Institutions.GetInstitutions())
            {
                ddlInstitution.Items.Add(new ListItem(item.Name, ((int)item.ID).ToString()));
            }
        }

        protected void ddlDistributionPointType_Init(object sender, EventArgs e)
        {
            ddlDistributionPointType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enDistributionPointType item in Enum.GetValues(typeof(enDistributionPointType)))
            {
                ddlDistributionPointType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtDistributionPointName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtDistributionPointID.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtCertificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                deCertificationDate.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvDistributionPoints.SettingsPager.PageSize;

                gvDistributionPoints.SettingsPager.PageSize = int.MaxValue;
                gvDistributionPoints.DataBind();

                if (_distributionPoints != null && _distributionPoints.Count > 0)
                {
                    e.EmailRecepients.AddRange(_distributionPoints.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_distributionPoints.Select(x => x.ContactEmail).Distinct());                    
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά σημεία διανομής.",
                        e.EmailRecepients.Count(),
                        _distributionPoints.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvDistributionPoints.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<DistributionPoint> _distributionPoints = null;
        protected void odsDistributionPoints_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<DistributionPoint>)
                _distributionPoints = e.ReturnValue as List<DistributionPoint>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvDistributionPoints.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvDistributionPoints.PageIndex = 0;
            gvDistributionPoints.DataBind();
        }

        protected void odsDistributionPoints_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<DistributionPoint> criteria = new Criteria<DistributionPoint>();

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

            if (!string.IsNullOrEmpty(txtDistributionPointName.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.DistributionPointName, txtDistributionPointName.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
            }

            int distributionPointID;
            if (int.TryParse(txtDistributionPointID.Text, out distributionPointID) && distributionPointID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, distributionPointID);
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

            int institutionID;
            if (int.TryParse(ddlInstitution.SelectedItem.Value, out institutionID) && institutionID >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.InstitutionID, institutionID);
            }

            int distributionPointType;
            if (int.TryParse(ddlDistributionPointType.SelectedItem.Value, out distributionPointType) && distributionPointType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.DistributionPointTypeInt, distributionPointType);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvDistributionPoints_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            DistributionPoint distributionPoint = (DistributionPoint)gvDistributionPoints.GetRow(e.VisibleIndex);

            if (distributionPoint != null)
            {
                switch (distributionPoint.VerificationStatus)
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

        protected string GetDistributionPointDetails(DistributionPoint distributionPoint)
        {
            if (distributionPoint == null)
                return string.Empty;

            string distributionPointDetails = string.Empty;

            if (distributionPoint.DistributionPointType == enDistributionPointType.Institution)
            {
                distributionPointDetails = string.Format("{0}<br/>{1}", distributionPoint.DistributionPointName, CacheManager.Institutions.Get((int)distributionPoint.InstitutionID).Name);
            }
            else
            {
                distributionPointDetails = string.Format("{0}", distributionPoint.DistributionPointName);
            }

            return distributionPointDetails;
        }
    }
}