using System;
using System.Web.Security;
using System.Web.UI;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using Eudoxus.eService;
using System.Linq;

namespace Eudoxus.Portal.Secure.MinistryPayments
{
    public partial class MinistryPaymentsUserDetails : BaseEntityPortalPage<MinistryPaymentsUser>
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
                fm.AddMessageEndRedirect("Πραγματοποιήθηκαν αλλαγές στα στοιχεία του λογαριασμού σας από την τελευταία φορά που φορτώθηκε η σελίδα. Παρακαλούμε ελέγξτε ξανά τα στοιχεία της.", "MinistryPaymentsUserDetails.aspx", true);

            if (!IsPostBack)
                VerificationStatus = Entity.VerificationStatus;

            ucMinistryPaymentsUserInput.Entity = Entity;
            if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
            {
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                phCannotBeVerified.Visible = false;
                btnUpdateMinistryPaymentsUser.OnClientClick = "return validate('vgMinistryPaymentsUser')";
                btnUpdateProfile.OnClientClick = null;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
            {
                phCanBeVerified.Visible = false;

                ucMinistryPaymentsUserInput.ReadOnly = true;
                btnUpdateMinistryPaymentsUser.Visible = false;
                btnUpdateProfile.OnClientClick = null;
            }

            if (!Page.IsPostBack)
            {
                ucMinistryPaymentsUserInput.Bind();
                FillUserDetails();
            }
            base.OnLoad(e);
        }

        #region [ Databind Methods ]

        protected override void Fill()
        {
            Entity = new MinistryPaymentsUserRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
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
                    var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
                    EudoxusOsyClient.Update(ministryPaymentsUserDto);
                    //ServiceWorker.SendMinistryPaymentsUserUpdate(Entity.ID);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                throw;
            }

            fm.Text = "Η ενημέρωση των Στοιχείων Χρήστη έγινε επιτυχώς";
        }

        protected void btnUpdateMinistryPaymentsUser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucMinistryPaymentsUserInput.Fill();
            UnitOfWork.Commit();

            var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
            EudoxusOsyClient.Update(ministryPaymentsUserDto);
            //ServiceWorker.SendMinistryPaymentsUserUpdate(Entity.ID);
            ucMinistryPaymentsUserInput.Bind();
            fm.Text = "Η ενημέρωση των Στοιχείων του χρήστη Υπουργείου - Πληρωμών πραγματοποιήθηκε επιτυχώς";
        }

        #endregion

    }
}
