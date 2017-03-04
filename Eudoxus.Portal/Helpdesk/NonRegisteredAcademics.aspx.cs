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
    public partial class NonRegisteredAcademics : BaseEntityPortalPage<object>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvAcademics.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvAcademics.PageIndex = 0;
            gvAcademics.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gveAcademics.FileName = String.Format("NonRegisteredSecretaries_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveAcademics.WriteXlsToResponse(true);
        }

        protected void OnEmailSending(object sender, EmailFormSendingEventArgs e)
        {
            if (!e.SendToLoggedInUser)
            {
                int _pageSize = gvAcademics.SettingsPager.PageSize;

                gvAcademics.SettingsPager.PageSize = int.MaxValue;
                gvAcademics.DataBind();

                if (_academics != null && _academics.Count > 0)
                {
                    List<string> emails = _academics.Select(x => x.Email).ToList();

                    List<string> recipients = new List<string>();

                    foreach (string email in emails)
                    {
                        if (email != null)
                        {
                            string[] splittedEmails = email.Split(',');

                            foreach (string em in splittedEmails)
                            {
                                recipients.Add(em.Trim());
                            }
                        }
                    }

                    e.EmailRecepients = recipients;

                    e.InfoMessage = string.Format("Η μαζική αποστολή ξεκίνησε για {0} Email Επικοινωνίας από {1} συνολικά Σχολές.",
                        recipients.Count(),
                        _academics.Count);
                }
                else
                {
                    e.Cancel = true;
                    e.InfoMessage = string.Format("Η μαζική αποστολή δεν πραγματοποιήθηκε.");
                }
                gvAcademics.SettingsPager.PageSize = _pageSize;

            }
            else
            {
                e.InfoMessage = string.Format("Η δοκιμαστική αποστολή ολοκληρώθηκε. Παρακαλούμε ελέξτε το Email σας.");
            }
        }

        List<AcademicDetail> _academics = null;
        protected void odsAcademics_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue is List<AcademicDetail>)
                _academics = e.ReturnValue as List<AcademicDetail>;
        }

        protected void odsAcademics_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            AcademicDetailCriteria criteria = new AcademicDetailCriteria();

            criteria.SecretaryExists.FieldValue = false;

            int schoolCode;
            if (int.TryParse(hfSchoolCode.Value, out schoolCode))
            {
                criteria.Expression = criteria.Expression.Where(x => x.ID, schoolCode);
            }

            int notificationStatus;
            if (int.TryParse(ddlNotificationStatus.SelectedItem.Value, out notificationStatus) && notificationStatus >= 0)
            {
                if (notificationStatus == 0)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsNotified, false);
                }
                else if (notificationStatus == 1)
                {
                    criteria.Expression = criteria.Expression.Where(x => x.IsNotified, true);
                }
            }

            e.InputParameters["criteria"] = criteria;
        }
    }
}