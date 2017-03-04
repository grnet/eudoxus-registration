using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Eudoxus.Mails;

namespace Eudoxus.Portal.Common
{
    public partial class DistributionPointRegistration : BaseEntityPortalPage<DistributionPoint>
    {
        protected override void Fill()
        {
            Entity = new DistributionPoint();
            Entity.DistributionPointType = enDistributionPointType.Store;
        }

        protected override void OnInit(EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            ucDistributionPointInput.Entity = Entity;

            if (!Config.DistributionPointRegistrationAllowed)
                mvRegistration.ActiveViewIndex = 2;

            base.OnLoad(e);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

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


            ucDistributionPointInput.Fill(Entity);
            Entity.UserName = registerUserInput.Username;
            Entity.Email = registerUserInput.Email;
            Entity.CreatedBy = registerUserInput.Username;

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

            MailSender.SendUserActivation(Entity.Email, Entity.UserName, uri);
            lblCompletionMessage.Text = string.Format("Ο λογαριασμός δημιουργήθηκε με επιτυχία και στάλθηκαν οδηγίες ενεργοποίησης στο e-mail <b>{0}</b><br/><br/>Ακολουθήστε τις οδηγίες στο email που λάβατε για να τον ενεργοποιήσετε.<br/><br/>Σε περίπτωση που δεν λάβετε το e-mail ενεργοποίησης μέσα στα επόμενα <b>10 λεπτά</b>, παρακαλούμε επικοινωνήστε με το Γραφείο Αρωγής Χρηστών στο τηλέφωνο <b>215 215 7850</b>", Entity.Email);
            mvRegistration.SetActiveView(vComplete);
        }

    }
}