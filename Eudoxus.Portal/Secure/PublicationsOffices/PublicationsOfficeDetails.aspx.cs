using System;
using System.Web.Security;
using System.Web.UI;

using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Secure.PublicationsOffices
{
    public partial class PublicationsOfficeDetails : BaseEntityPortalPage<PublicationsOffice>
    {
        public enVerificationStatus? VerificationStatus
        {
            get
            {
                var status = ViewState["__VfStatus"] as enVerificationStatus?;
                if (status.HasValue)
                    return status.Value;
                return null;
            }
            set
            {
                ViewState["__VfStatus"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (VerificationStatus.HasValue && Entity.VerificationStatus != VerificationStatus.Value)
                fm.AddMessageEndRedirect("Πραγματοποιήθηκαν αλλαγές στα στοιχεία του λογαριασμού σας από την τελευταία φορά που φορτώθηκε η σελίδα. Παρακαλούμε ελέγξτε ξανά τα στοιχεία της.", "PublicationsOfficeDetails.aspx", true);

            if (!IsPostBack)
                VerificationStatus = Entity.VerificationStatus;

            publicationsOfficeInput.Entity = Entity;
            if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
            {
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                phCannotBeVerified.Visible = false;
                btnUpdatePublicationsOffice.OnClientClick = "return validate('vgPublicationsOffice')";
                btnUpdateProfile.OnClientClick = null;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
            {
                phCanBeVerified.Visible = false;

                publicationsOfficeInput.ReadOnly = true;
                btnUpdatePublicationsOffice.Visible = false;
                btnUpdateProfile.OnClientClick = null;
            }

            if (!Page.IsPostBack)
            {
                publicationsOfficeInput.Bind();
                FillUserDetails();
            }
            base.OnLoad(e);
        }

        #region [ Databind Methods ]

        protected override void Fill()
        {
            Entity = new PublicationsOfficeRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        private void FillUserDetails()
        {
            MembershipUser mu = Membership.GetUser(Entity.UserName);
            registerUserInput.EditMode = (int)Eudoxus.Portal.UserControls.RegisterUserInput.enEditMode.ExistingUser;
            registerUserInput.SetUser(mu);
        }

        #endregion

        #region [ Button Handlers ]

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                if (registerUserInput.UpdateUser(Entity.UserName))
                {
                    ServiceWorker.SendPublicationsOfficeUpdate(Entity.ID);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                throw;
            }

            fm.Text = "Η ενημέρωση των Στοιχείων Χρήστη έγινε επιτυχώς";
        }

        protected void btnUpdatePublicationsOffice_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            publicationsOfficeInput.Fill();
            UnitOfWork.Commit();
            ServiceWorker.SendPublicationsOfficeUpdate(Entity.ID);
            publicationsOfficeInput.Bind();
            fm.Text = "Η ενημέρωση των Στοιχείων του Γραφείου Διδακτικών Συγγραμμάτων πραγματοποιήθηκε επιτυχώς";
        }

        #endregion

    }
}
