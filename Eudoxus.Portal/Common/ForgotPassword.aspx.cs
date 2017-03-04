using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Threading;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;
using System.Web.Profile;
using Imis.Domain;
using Eudoxus.eService;

namespace Eudoxus.Portal.Common
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "";
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var users = Membership.FindUsersByEmail(txtEmail.Text).OfType<MembershipUser>();
                if (users.Count() == 0)
                {
                    lblInfo.Text = "Το e-mail που εισάγατε δεν αντιστοιχεί σε κάποιο χρήστη του πληροφοριακού συστήματος.<br/>Παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο 215 215 7850";
                    return;
                }
                else if (users.Count() > 1)
                {
                    string usernames = string.Empty;
                    users.Select(x => x.UserName).ToList().ForEach(x => usernames += x + ", ");
                    lblInfo.Text = "Προκλήθηκε σφάλμα. Επικοινωνήστε με το Helpdesk.";
                }
                else
                {
                    var user = users.Single();
                    if (user.IsLockedOut)
                    {
                        user.UnlockUser();
                    }

                    string oldPassword = user.ResetPassword();
                    string newPassword = Guid.NewGuid().ToString().Substring(0, 8);

                    user.ChangePassword(oldPassword, newPassword);
                    Membership.UpdateUser(user);

                    MailSender.SendForgotPassword(user.Email, user.UserName, newPassword);

                    var c = new ReporterCriteria();
                    c.Expression = c.Expression.Where(x => x.UserName, user.UserName);
                    c.UsePaging = false;
                    int totalRecords;
                    using (IUnitOfWork uow = UnitOfWorkFactory.Create())
                    {
                        var reporters = new ReporterRepository(uow).FindReportersWithCriteria(c, out totalRecords);
                        if (totalRecords == 1)
                        {
                            reporters.FirstOrDefault().MustChangePassword = true;
                            uow.Commit();

                            if (reporters.First() is Publisher)
                            {
                                ServiceWorker.SendPublisherUpdate(reporters.First().ID);
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
                                var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == reporters.First().ID).ToJsonDto();
                                EudoxusOsyClient.Update(ministryPaymentsUserDto);
                            }
                        }
                    }

                    lblInfo.Text = "Ο νέος κωδικός πρόσβασης στάλθηκε με επιτυχία.";
                }
            }
            catch (ThreadAbortException)
            {
                //swallow like a fish
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Δεν βρέθηκε ο χρήστης.";
            }
        }
    }
}
