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
    public partial class VerifyLibraries : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtUserName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtEmail.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtLibraryName.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                txtCertificationNumber.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
                deCertificationDate.Attributes["onkeypress"] = string.Format("Imis.Lib.EnterHandler(event, function(){{{0};}})",
                    Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty));
            }
        }

        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- επιλέξτε ίδρυμα --", ""));

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
                int _pageSize = gvLibraries.SettingsPager.PageSize;

                gvLibraries.SettingsPager.PageSize = int.MaxValue;
                gvLibraries.DataBind();

                if (_libraries != null && _libraries.Count > 0)
                {
                    e.EmailRecepients.AddRange(_libraries.Select(x => x.Email).Distinct());
                    e.EmailRecepients.AddRange(_libraries.Select(x => x.ContactEmail).Distinct());
                    e.EmailRecepients.AddRange(_libraries.Select(x => x.LibraryDetails.AlternateContactEmail).Where(x => !string.IsNullOrEmpty(x)).Distinct());
                    e.EmailRecepients = e.EmailRecepients.Distinct().ToList();
                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} μοναδικά Email χρηστών {1} συνολικά βιβλιοθήκες.",
                        e.EmailRecepients.Count(),
                        _libraries.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvLibraries.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<Library> _libraries = null;
        protected void odsLibraries_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<Library>)
                _libraries = e.ReturnValue as List<Library>;
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvLibraries.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvLibraries.PageIndex = 0;
            gvLibraries.DataBind();
        }

        protected void odsLibraries_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<Library> criteria = new Criteria<Library>();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("LibraryDetails");

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

            if (!string.IsNullOrEmpty(txtLibraryName.Text))
            {
                criteria.Expression = criteria.Expression.Where(x => x.LibraryName, txtLibraryName.Text.ToNull(), Imis.Domain.EF.Search.enCriteriaOperator.Like);
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

            int institutionType;
            if (int.TryParse(ddlInstitutionType.SelectedItem.Value, out institutionType) && institutionType >= 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Institution.InstitutionTypeInt, institutionType);
            }

            e.InputParameters["criteria"] = criteria;
        }

        protected void gvLibraries_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
                return;

            Library library = (Library)gvLibraries.GetRow(e.VisibleIndex);

            if (library != null)
            {
                switch (library.VerificationStatus)
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

        protected string GetLibraryDetails(Library library)
        {
            if (library == null)
                return string.Empty;

            string libraryDetails = string.Empty;

            var institution = CacheManager.Institutions.Get((int)library.InstitutionID);

            if (institution != null)
            {
                libraryDetails = string.Format("Ίδρυμα: {0}<br/>Τίτλος: {1}", institution.Name, library.LibraryName);
            }

            return libraryDetails;
        }
    }
}