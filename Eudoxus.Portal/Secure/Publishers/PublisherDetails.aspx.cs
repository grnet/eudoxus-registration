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

using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using Eudoxus.eService;
using Eudoxus.Utils;

namespace Eudoxus.Portal.Secure.Publishers
{
    public partial class PublisherDetails : BaseEntityPortalPage<Publisher>
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

        protected override void Fill()
        {
            Entity = new PublisherRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack)
            {
                FillUserDetails();
                FillPublisher();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (VerificationStatus.HasValue && Entity.VerificationStatus != VerificationStatus.Value)
                fm.AddMessageEndRedirect("Πραγματοποιήθηκαν αλλαγές στα στοιχεία του λογαριασμού σας από την τελευταία φορά που φορτώθηκε η σελίδα. Παρακαλούμε ελέγξτε ξανά τα στοιχεία της.", "PublisherDetails.aspx", true);

            if (!IsPostBack)
                VerificationStatus = Entity.VerificationStatus;

            if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
            {
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                phCannotBeVerified.Visible = false;

                if (Entity.PublisherType == enPublisherType.SelfPublisher)
                {
                    if (!Entity.PublisherDetails.HasLogisticBooks.HasValue)
                    {
                        publisherInput.IsVerifiedWithoutHasLogisticBooks = true;
                    }
                    else
                    {
                        publisherInput.IsVerified = true;
                    }
                }
                else
                {
                    publisherInput.IsVerified = true;
                }
                
                btnUpdatePublisher.OnClientClick = "return validate('vgPublisher')";
                btnUpdateProfile.OnClientClick = null;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
            {
                phCanBeVerified.Visible = false;

                publisherInput.ReadOnly = true;
                btnUpdatePublisher.Visible = false;
                btnUpdateProfile.OnClientClick = null;
            }
            publisherInput.PublisherType = Entity.PublisherType;
            publisherInput.PublisherID = Entity.ID;
        }

        private void FillUserDetails()
        {
            MembershipUser mu = Membership.GetUser(Entity.UserName);

            registerUserInput.EditMode = (int)Eudoxus.Portal.UserControls.RegisterUserInput.enEditMode.ExistingUser;
            registerUserInput.SetUser(mu);
        }

        private void FillPublisher()
        {
            publisherInput.SetPublisher(Entity);
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                if (registerUserInput.UpdateUser(Entity.UserName))
                {
                    ServiceWorker.SendPublisherUpdate(Entity.ID);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                throw;
            }

            fm.Text = "Η ενημέρωση των Στοιχείων Χρήστη έγινε επιτυχώς";
        }

        protected void btnUpdatePublisher_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            publisherInput.FillPublisher(Entity);
            UnitOfWork.Commit();
            ServiceWorker.SendPublisherUpdate(Entity.ID);

            if (Config.UsePaymentEService)
            {
                try
                {
                    var publisherDto = new HelpDeskViewsEntities().vPublisher.Single(x => x.ID == Entity.ID).ToJsonDto();

                    if (publisherDto.PublisherType != (int)enPublisherType.EbookPublisher)
                    {
                        //ServiceClientForPayment.Update(publisherDto);
                        EudoxusOsyClient.Update(publisherDto);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError(ex, this, string.Format("Update failed for publisher with id {0}", Entity.ID));
                }
            }

            if (Entity.PublisherType == enPublisherType.SelfPublisher && Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                publisherInput.IsVerified = true;
            }

            publisherInput.SetPublisher(Entity);

            fm.Text = "Η ενημέρωση των Στοιχείων του Εκδότη πραγματοποιήθηκε επιτυχώς";
        }
    }
}
