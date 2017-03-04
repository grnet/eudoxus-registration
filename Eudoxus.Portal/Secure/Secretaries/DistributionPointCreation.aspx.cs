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

namespace Eudoxus.Portal.Secure.Secretaries
{
    public partial class DistributionPointCreation : BaseEntityPortalPage<DistributionPoint>
    {
        public Secretary CurrentSecretary;

        protected override void Fill()
        {
            var distributionPoint = new DistributionPointRepository(UnitOfWork).FindByCreator(Page.User.Identity.Name);

            CurrentSecretary = new SecretaryRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);

            if (distributionPoint == null)
            {   
                var academic = CacheManager.Academics.Get(CurrentSecretary.AcademicID);
                var institution = CacheManager.Institutions.Get(academic.InstitutionID);

                Entity = new DistributionPoint();
                Entity.DistributionPointType = enDistributionPointType.Institution;
                Entity.InstitutionID = institution.ID;
            }
            else
            {
                Entity = distributionPoint;
            }
        }

        protected override void OnInit(EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            if (CurrentSecretary.VerificationStatus != enVerificationStatus.Verified) {
                mvAccount.SetActiveView(vNotVerified);
                return;
            }

            if (!Entity.IsNew)
            {
                phMessage.Visible = true;
                registerUserInput.Visible = false;
                btnCreate.Visible = false;

                txtUsername.Text = Entity.UserName;
                txtEmail.Text = Entity.Email;
                ucDistributionPointInput.ReadOnly = true;
            }

            ucDistributionPointInput.Entity = Entity;
            ucDistributionPointInput.Bind();

            base.OnLoad(e);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

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
            Entity.DistributionPointCreator = Page.User.Identity.Name;
            Entity.UserName = registerUserInput.Username;
            Entity.Email = registerUserInput.Email;
            Entity.CreatedBy = registerUserInput.Username;
            Entity.VerificationStatus = enVerificationStatus.Verified;
            Entity.VerificationDate = DateTime.Now;

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

            MembershipUser u = Membership.GetUser(username);
            ActivateUserResult r = ActivateUserResult.UserNotFound;
            r = ActivateHelper.Activate<DistributionPoint>(u);

            if (r == ActivateUserResult.Success)
            {
                Response.Redirect("~/Secure/Secretaries/DistributionPointCreation.aspx");
            }
        }
    }
}