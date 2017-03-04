using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using System.Threading;
using Eudoxus.Mails;
using Eudoxus.Utils;

namespace Eudoxus.Portal.Admin
{
    public partial class SendMessageToSecretaries : System.Web.UI.Page
    {
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Criteria<Secretary> criteria = new Criteria<Secretary>();

            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, enVerificationStatus.Verified);
            criteria.UsePaging = false;

            int recordCount;
            List<Secretary> secretaries = new SecretaryRepository().FindSecretariesWithCriteria(criteria, out recordCount);

            string subject = "Εξέλιξη προγράμματος Εύδοξος";
            string bodyBase = @"Αγαπητοί συνεργάτες,

Σύμφωνα με την ισχύουσα νομοθεσία, οι Γραμματείες όλων των Τμημάτων οφείλουν να δημοσιεύουν τον κατάλογο με τα υποχρεωτικά και επιλεγόμενα μαθήματα του προγράμματος σπουδών τους και τα αντίστοιχα σε αυτά προτεινόμενα διδακτικά συγγράμματα στο διαδικτυακό τόπο του Ιδρύματος και στην επίσημη ιστοσελίδα του Τμήματός τους.

Προς διευκόλυνσή σας, σας αποστέλλουμε το σύνδεσμο στον Εύδοξο, που περιλαμβάνει την αντίστοιχη πληροφορία προκειμένου να τον αναρτήσετε στο διαδικτυακό τόπο του Ιδρύματος και του Τμήματός σας:

https://service.eudoxus.gr/secapp/public/?secretariatId={0}

Επιπλέον, σας ενημερώνουμε πως τα στοιχεία που έχετε καταχωρίσει στο Εύδοξος, θα έχετε τη δυνατότητα να τα τροποποιείτε μέχρι την Δευτέρα, 13 Σεπτεμβρίου. Αυτή είναι η καταληκτική ημερομηνία, πέρα της οποίας καμία αλλαγή δε θα είναι εφικτή. Σας παρακαλούμε να προβείτε σε μία τελική επαλήθευση των στοιχείων για τα βιβλία που έχετε καταχωρίσει έως τώρα, και να συμπληρώσετε τυχόν ελλιπή πληροφοριακά στοιχεία (π.χ παράμετροι διανομής).

Στη διάθεσή σας,

Γραφείο Αρωγής Χρηστών Εύδοξος";

            string body;

            ThreadPool.QueueUserWorkItem((state) =>
            {
                foreach (var s in secretaries)
                {
                    List<string> recipients = new List<string>();

                    recipients.Add(s.Email);
                    recipients.Add(s.ContactEmail);

                    s.SecretaryDetailsReference.Load();
                    if (!string.IsNullOrEmpty(s.SecretaryDetails.AlternateContactEmail))
                    {
                        recipients.Add(s.SecretaryDetails.AlternateContactEmail);
                    }

                    recipients = recipients.Distinct().ToList();

                    body = string.Format(bodyBase, s.ID);

                    foreach (var r in recipients)
                    {
                        try
                        {
                            MailSender.SendCustomMessage(r, subject, body);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError<SendMessageToSecretaries>(ex);
                            continue;
                        }
                    }
                }
            });
        }
    }
}