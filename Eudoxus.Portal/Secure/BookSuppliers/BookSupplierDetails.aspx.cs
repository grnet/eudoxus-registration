using System;
using System.Web.Security;
using System.Web.UI;

using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Secure.BookSuppliers
{
    public partial class BookSupplierDetails : BaseEntityPortalPage<BookSupplier>
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
                fm.AddMessageEndRedirect("Πραγματοποιήθηκαν αλλαγές στα στοιχεία του λογαριασμού σας από την τελευταία φορά που φορτώθηκε η σελίδα. Παρακαλούμε ελέγξτε ξανά τα στοιχεία της.", "BookSupplierDetails.aspx", true);

            if (!IsPostBack)
                VerificationStatus = Entity.VerificationStatus;

            ucBookSupplierInput.Entity = Entity;
            if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
            {
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                phCannotBeVerified.Visible = false;
                btnUpdateBookSupplier.OnClientClick = "return validate('vgBookSupplier')";
                btnUpdateProfile.OnClientClick = null;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
            {
                phCanBeVerified.Visible = false;

                ucBookSupplierInput.ReadOnly = true;
                btnUpdateBookSupplier.Visible = false;
                btnUpdateProfile.OnClientClick = null;
            }

            if (!Page.IsPostBack)
            {
                ucBookSupplierInput.Bind();
                FillUserDetails();
            }
            base.OnLoad(e);
        }

        #region [ Databind Methods ]

        protected override void Fill()
        {
            Entity = new BookSupplierRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
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
                    ServiceWorker.SendBookSupplierUpdate(Entity.ID);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                throw;
            }

            fm.Text = "Η ενημέρωση των Στοιχείων Χρήστη έγινε επιτυχώς";
        }

        protected void btnUpdateBookSupplier_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucBookSupplierInput.Fill();
            UnitOfWork.Commit();
            ServiceWorker.SendBookSupplierUpdate(Entity.ID);
            ucBookSupplierInput.Bind();
            fm.Text = "Η ενημέρωση των Στοιχείων του Υπεύθυνου Παραγγελίας Βιβλίων πραγματοποιήθηκε επιτυχώς";
        }

        #endregion

    }
}
