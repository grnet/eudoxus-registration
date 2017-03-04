using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using AjaxControlToolkit;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Eudoxus.eService;

namespace Eudoxus.Portal.UserControls.Generic
{
    public partial class LoginBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Controls in templates
        private ModalPopupExtender mpeChangePassword
        {
            get { return (ModalPopupExtender)loginView.FindControl("mpeChangePassword"); }
        }

        private ChangePassword cp
        {
            get { return (ChangePassword)loginView.FindControl("cp"); }
        }

        private Label lblErrorMessage
        {
            get { return (Label)loginView.FindControl("lblErrorMessage"); }
        }

        private MultiView mv
        {
            get { return (MultiView)loginView.FindControl("mv"); }
        }

        #endregion

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MembershipUser mu = Membership.GetUser();
                if (mu.ChangePassword(cp.OldPassword, cp.NewPassword))
                {
                    var c = new ReporterCriteria();
                    c.Expression = c.Expression.Where(x => x.CreatedBy, mu.UserName);
                    c.UsePaging = false;
                    int totalRecords;
                    var reporters = new ReporterRepository().FindReportersWithCriteria(c, out totalRecords);
                    if (totalRecords == 1)
                    {
                        var reporterID = reporters.First().ID;
                        if (reporters.First() is Publisher)
                        {
                            ServiceWorker.SendPublisherUpdate(reporters.First().ID);

                            var publisherDto = new HelpDeskViewsEntities().vPublisher.Single(x => x.ID == reporterID).ToJsonDto();
                            EudoxusOsyClient.Update(publisherDto);
                        }
                        else if (reporters.First() is Secretary)
                        {
                            ServiceWorker.SendSecretaryUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is PublicationsOffice)
                        {
                            ServiceWorker.SendPublicationsOfficeUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is DataCenter)
                        {
                            ServiceWorker.SendDataCenterUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is DistributionPoint)
                        {
                            ServiceWorker.SendDistributionPointUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is Library)
                        {
                            ServiceWorker.SendLibraryUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is BookSupplier)
                        {
                            ServiceWorker.SendBookSupplierUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is PricingCommittee)
                        {
                            ServiceWorker.SendPricingCommitteeUpdate(reporters.First().ID);
                        }
                        else if (reporters.First() is MinistryPaymentsUser)
                        {
                            var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == reporterID).ToJsonDto();
                            EudoxusOsyClient.Update(ministryPaymentsUserDto);
                        }
                    }
                    lblErrorMessage.Text = "";
                    cp.ClearInput();
                    mv.ActiveViewIndex = 1; // success
                    mpeChangePassword.Show();
                }
                else
                {
                    mpeChangePassword.Show();
                    lblErrorMessage.Text = "Ο παλιός κωδικός πρόσβασης δεν είναι σωστός. Ελέγξτε ότι τον πληκτρολογείτε σωστά.";
                }
            }
        }

        protected void btnSuccess_Click(object sender, EventArgs e)
        {
            mv.ActiveViewIndex = 0; // restore default view
            mpeChangePassword.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cp.ClearInput();
            mpeChangePassword.Hide();
        }

    }
}