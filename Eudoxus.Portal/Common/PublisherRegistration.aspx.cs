using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Imis.Domain.EF;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using Eudoxus.Mails;
using System.Threading;
using System.Security.Principal;

namespace Eudoxus.Portal.Common
{
    public partial class PublisherRegistration : BaseEntityPortalPage<Publisher>
    {
        protected override void Fill()
        {
            Entity = new Publisher();
        }

        enPublisherType publisherType = enPublisherType.LegalPerson;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Config.PublisherRegistrationAllowed)
            {
                mvRegistration.ActiveViewIndex = 2;
            }

            int registrationTypeInt;
            if (!int.TryParse(Request.QueryString["t"], out registrationTypeInt))
                Response.Redirect("~/Default.aspx");
            publisherType = (enPublisherType)registrationTypeInt;
            publisherInput.PublisherType = publisherType;
            publisherInput.IsForeignPublisher = false;

            if (publisherType == enPublisherType.LegalPerson)
            {
                lblTitle.Text = "Δημιουργία Χρήστη Εκδοτικού Οίκου (Νομικό Πρόσωπο)";
                Title = "Δημιουργία Χρήστη Εκδοτικού Οίκου (Νομικό Πρόσωπο)";
            }
            else if (publisherType == enPublisherType.SelfPublisher)
            {
                lblTitle.Text = "Δημιουργία Χρήστη Αυτοεκδότη (Φυσικό Πρόσωπο)";
                Title = "Δημιουργία Χρήστη Αυτοεκδότη (Φυσικό Πρόσωπο)";
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnAcceptTerms_Click(object sender, EventArgs e)
        {
            mvRegistration.SetActiveView(vRegister);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            var vs = Page.Validators.Cast<BaseValidator>().Where(x => !x.IsValid);

            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            PublisherRepository pRep = new PublisherRepository();


            publisherInput.FillPublisher(Entity);

            string username = null;

            try
            {
                username = registerUserInput.CreateUser();
                if (string.IsNullOrEmpty(username))
                    throw new MembershipCreateUserException("CreateUser returned empty username");
            }
            catch (MembershipCreateUserException)
            {
                return;
            }

            MembershipUser user = Membership.GetUser(username);


            Entity.UserName = user.UserName;
            Entity.Email = user.Email;
            Entity.CreatedBy = user.UserName;
            Entity.PublisherDetails.CreatedBy = user.UserName;

            try
            {
                UnitOfWork.MarkAsNew(Entity);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                Membership.DeleteUser(username);
                throw;
            }

            var httpPrefix = "http://";
            if (Config.IsSSL)
            {
                httpPrefix = "https://";
            }

            Uri baseUri = new Uri(httpPrefix + HttpContext.Current.Request.Url.Authority + "/Common/");
            Uri uri = new Uri(baseUri, "ActivateUser.aspx?id=" + registerUserInput.ProviderUserKey.Replace("-", string.Empty));

            MailSender.SendUserActivation(user.Email, user.UserName, uri);
            lblCompletionMessage.Text = string.Format("Ο λογαριασμός δημιουργήθηκε με επιτυχία και στάλθηκαν οδηγίες ενεργοποίησης στο e-mail <b>{0}</b><br/><br/>Ακολουθήστε τις οδηγίες στο email που λάβατε για να τον ενεργοποιήσετε.<br/><br/>Σε περίπτωση που δεν λάβετε το e-mail ενεργοποίησης μέσα στα επόμενα <b>10 λεπτά</b>, παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο <b>215 215 7850</b>", user.Email);
            mvRegistration.SetActiveView(vComplete);
        }
    }
}