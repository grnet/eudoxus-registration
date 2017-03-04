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
    public partial class VerifyBookSuppliers : BaseEntityPortalPage<object>
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
                int _pageSize = gvBookSuppliers.SettingsPager.PageSize;

                gvBookSuppliers.SettingsPager.PageSize = int.MaxValue;
                gvBookSuppliers.DataBind();

                if (_bookSuppliers != null && _bookSuppliers.Count > 0)
                {
                    e.EmailRecepients.AddRange(_bookSuppliers.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_bookSuppliers.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_bookSuppliers.Select(x => x.BookSupplierDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά υπευθύνους παραγγελίας βιβλίων.",
                        e.EmailRecepients.Count(),
                        _bookSuppliers.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvBookSuppliers.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<BookSupplier> _bookSuppliers = null;
        protected void odsBookSuppliers_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<BookSupplier>)
                _bookSuppliers = e.ReturnValue as List<BookSupplier>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvBookSuppliers.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvBookSuppliers.PageIndex = 0;
            gvBookSuppliers.DataBind();
        }

        protected void odsBookSuppliers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<BookSupplier> criteria = new Criteria<BookSupplier>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("BookSupplierDetails");

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

        protected void gvBookSuppliers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            BookSupplier bookSupplier = (BookSupplier)gvBookSuppliers.GetRow(e.VisibleIndex);

            if (bookSupplier != null)
            {
                switch (bookSupplier.VerificationStatus)
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

        protected string GetBookSupplierDetails(BookSupplier bookSupplier)
        {
            if (bookSupplier == null)
                return string.Empty;

            string bookSupplierDetails = string.Empty;

            var institution = CacheManager.Institutions.Get((int)bookSupplier.InstitutionID);

            if (institution != null)
            {
                bookSupplierDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }

            return bookSupplierDetails;
        }
    }
}