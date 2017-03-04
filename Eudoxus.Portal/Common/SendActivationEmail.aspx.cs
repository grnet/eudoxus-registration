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

namespace Eudoxus.Portal.Stores
{
    public partial class SendActivationEmail : System.Web.UI.Page
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
                    lblInfo.Text = "Το e-mail που εισάγατε δεν αντιστοιχεί σε κάποιο χρήστη του πληροφοριακού συστήματος.<br/>Πιθανώς να το πληκτρολογήσατε λάθος κατά τη δημιουργία του λογαριασμού σας.<br/>Παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο 215 215 7850 για να διαπιστωθεί το πρόβλημα.";
                    return;
                }
                else if (users.Count() > 1)
                {
                    string usernames = string.Empty;
                    users.Select(x => x.UserName).ToList().ForEach(x => usernames += x + ", ");
                    lblInfo.Text = "Προκλήθηκε κάποιο σφάλμα.<br/>Παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο 215 215 7850";
                }
                else
                {
                    var user = users.Single();
                    if (user.IsLockedOut)
                    {
                        user.UnlockUser();
                    }

                    Uri baseUri = new Uri("https://" + HttpContext.Current.Request.Url.Authority + "/Common/");
                    Uri uri = new Uri(baseUri, "ActivateUser.aspx?id=" + user.ProviderUserKey.ToString().Replace("-", string.Empty));
                    
                    MailSender.SendUserActivation(user.Email, user.UserName, uri);
                    lblCompletionMessage.Text = string.Format("Το e-mail ενεργοποίησης στάλθηκε με επιτυχία στη διεύθυνση <b>{0}</b><br/><br/>Ακολουθήστε τις οδηγίες στο email που λάβατε για να ενεργοποιήσετε το λογαριασμό σας.<br/><br/>Σε περίπτωση που δεν λάβετε το e-mail ενεργοποίησης μέσα στα επόμενα <b>10 λεπτά</b>, παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο <b>215 215 7850</b>", user.Email);
                    mvActivationEmail.SetActiveView(vActivationEmailSent);
                }
            }
            catch (ThreadAbortException)
            {
                //swallow like a fish
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Ο χρήστης που εισάγατε δεν βρέθηκε. Παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο 215 215 7850";
            }
        }
    }
}
