using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using System.Web.Services;
using Imis.Domain;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class ViewAccountDetails : BaseEntityPortalPage<object>
    {
        #region [ Private Fields and Properties ]

        private enReporterType ReporterType
        {
            get
            {
                int i;
                if (!int.TryParse(Request.QueryString["t"], out i))
                {
                    InvalidatePage();
                    return enReporterType.Unknown;
                }
                return (enReporterType)i;
            }
        }
        private int ReporterID
        {
            get
            {
                int id;
                if (!int.TryParse(Request.QueryString["rid"], out id))
                    InvalidatePage();
                return id;
            }
        }
        private Reporter _Reporter = null;
        private MembershipUser _User = null;

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            ScriptManager.GetCurrent(this).EnablePageMethods = true;
            LoadData();
            Bind();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "", string.Format(@"var _REPORTERID = {0};
var _USERNAME = '{1}';
var _REPORTERTYPE = {2};
", ReporterID, _User.UserName, ReporterType.ToString("D")), true);
            base.OnLoad(e);
        }

        private void InvalidatePage()
        {
            Response.Clear();
            Response.Write("<html><body><script type='text/javascript'>window.close();</script></body></html>");
            Response.End();
        }

        #region [ Data Methods ]

        private void LoadData()
        {
            ReporterRepository rep = new ReporterRepository(UnitOfWork);
            switch (ReporterType)
            {
                case enReporterType.Publisher:
                    _Reporter = rep.FindByID<Publisher>(ReporterID);
                    break;
                case enReporterType.Secretary:
                    _Reporter = rep.FindByID<Secretary>(ReporterID);
                    break;
                case enReporterType.DistributionPoint:
                    _Reporter = rep.FindByID<DistributionPoint>(ReporterID);
                    break;
                case enReporterType.PublicationsOffice:
                    _Reporter = rep.FindByID<PublicationsOffice>(ReporterID);
                    break;
                case enReporterType.DataCenter:
                    _Reporter = rep.FindByID<DataCenter>(ReporterID);
                    break;
                case enReporterType.Library:
                    _Reporter = rep.FindByID<Library>(ReporterID);
                    break;
                case enReporterType.BookSupplier:
                    _Reporter = rep.FindByID<BookSupplier>(ReporterID);
                    break;
                case enReporterType.PricingCommittee:
                    _Reporter = rep.FindByID<PricingCommittee>(ReporterID);
                    break;
                case enReporterType.MinistryPayments:
                    _Reporter = rep.FindByID<MinistryPaymentsUser>(ReporterID);
                    break;
            }
            if (_Reporter != null)
            {
                _User = Membership.GetUser(_Reporter.CreatedBy);
            }
        }

        private void Bind()
        {
            if (_Reporter == null || _User == null)
                return;
            ltrEmail.Text = _User.Email;
            ltrIsActivated.Text = _User.IsApproved ? "Ενεργοποιημένος" : "Μη ενεργοποιημένος";
            ltrIsLockedOut.Text = _User.IsLockedOut ? "Ναι" : "Όχι";
            ltrUsername.Text = _User.UserName;

            phActivate.Visible = !_User.IsApproved;
            phIsLocked.Visible = _User.IsLockedOut;
        }


        #endregion

        #region [ Web Services ]

        [WebMethod]
        public static bool? ActivateUser(string username, int reporterID, enReporterType reporterType)
        {
            var user = Membership.GetUser(username);
            if (user == null)
                return null;
            if (user.IsApproved)
                return null;
            user.IsApproved = true;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                Reporter r = null;

                switch (reporterType)
                {
                    case enReporterType.Publisher:
                        r = new ReporterRepository(uow).FindByID<Publisher>(reporterID);
                        break;
                    case enReporterType.Secretary:
                        r = new ReporterRepository(uow).FindByID<Secretary>(reporterID);
                        break;
                    case enReporterType.DistributionPoint:
                        r = new ReporterRepository(uow).FindByID<DistributionPoint>(reporterID);
                        break;
                    case enReporterType.PublicationsOffice:
                        r = new ReporterRepository(uow).FindByID<PublicationsOffice>(reporterID);
                        break;
                    case enReporterType.DataCenter:
                        r = new ReporterRepository(uow).FindByID<DataCenter>(reporterID);
                        break;
                    case enReporterType.Library:
                        r = new ReporterRepository(uow).FindByID<Library>(reporterID);
                        break;
                    case enReporterType.BookSupplier:
                        r = new ReporterRepository(uow).FindByID<BookSupplier>(reporterID);
                        break;
                    case enReporterType.PricingCommittee:
                        r = new ReporterRepository(uow).FindByID<PricingCommittee>(reporterID);
                        break;
                    case enReporterType.MinistryPayments:
                        r = new ReporterRepository(uow).FindByID<MinistryPaymentsUser>(reporterID);
                        break;
                }

                if (r == null)
                    return null;

                if (r is Publisher)
                {
                    if (!Roles.IsUserInRole(username, RoleNames.PublisherUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.PublisherUser);
                    }

                    (r as Publisher).IsActivated = true;
                }
                else if (r is Secretary)
                {
                    if (!Roles.IsUserInRole(RoleNames.SecretaryUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.SecretaryUser);
                    }
                    
                    (r as Secretary).IsActivated = true;
                }
                else if (r is DistributionPoint)
                {
                    if (!Roles.IsUserInRole(RoleNames.DistributionPointUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.DistributionPointUser);
                    }
                    
                    (r as DistributionPoint).IsActivated = true;
                }
                else if (r is PublicationsOffice)
                {
                    if (!Roles.IsUserInRole(RoleNames.PublicationsOfficeUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.PublicationsOfficeUser);
                    }
                    
                    (r as PublicationsOffice).IsActivated = true;
                }
                else if (r is DataCenter)
                {
                    if (!Roles.IsUserInRole(RoleNames.DataCenterUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.DataCenterUser);
                    }
                    
                    (r as DataCenter).IsActivated = true;
                }
                else if (r is Library)
                {
                    if (!Roles.IsUserInRole(RoleNames.LibraryUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.LibraryUser);
                    }
                    
                    (r as Library).IsActivated = true;
                }
                else if (r is BookSupplier)
                {
                    if (!Roles.IsUserInRole(RoleNames.BookSupplierUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.BookSupplierUser);
                    }
                    
                    (r as BookSupplier).IsActivated = true;
                }
                else if (r is PricingCommittee)
                {
                    if (!Roles.IsUserInRole(RoleNames.PricingCommitteeUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.PricingCommitteeUser);
                    }

                    (r as PricingCommittee).IsActivated = true;
                }
                else if (r is MinistryPaymentsUser)
                {
                    if (!Roles.IsUserInRole(RoleNames.MinistryPaymentsUser))
                    {
                        Roles.AddUserToRole(user.UserName, RoleNames.MinistryPaymentsUser);
                    }

                    (r as MinistryPaymentsUser).IsActivated = true;
                }
                uow.Commit();
                Membership.UpdateUser(user);
            }

            return user.IsApproved;
        }

        [WebMethod]
        public static bool? UnlockUser(string username)
        {
            var user = Membership.GetUser(username);
            if (user == null)
                return null;
            if (!user.IsLockedOut)
                return true;
            user.UnlockUser();
            return true;
        }

        [WebMethod]
        public static bool? ChangeEmail(string username, string newEmail, int reporterID, enReporterType reporterType)
        {
            var user = Membership.GetUser(username);
            if (user == null)
                return null;

            if (Membership.FindUsersByEmail(newEmail).Count > 0)
                return false;

            if (user.Email == newEmail)
                return true;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                Reporter r = null;

                switch (reporterType)
                {
                    case enReporterType.Publisher:
                        r = new ReporterRepository(uow).FindByID<Publisher>(reporterID);
                        break;
                    case enReporterType.Secretary:
                        r = new ReporterRepository(uow).FindByID<Secretary>(reporterID);
                        break;
                    case enReporterType.DistributionPoint:
                        r = new ReporterRepository(uow).FindByID<DistributionPoint>(reporterID);
                        break;
                    case enReporterType.PublicationsOffice:
                        r = new ReporterRepository(uow).FindByID<PublicationsOffice>(reporterID);
                        break;
                    case enReporterType.DataCenter:
                        r = new ReporterRepository(uow).FindByID<DataCenter>(reporterID);
                        break;
                    case enReporterType.Library:
                        r = new ReporterRepository(uow).FindByID<Library>(reporterID);
                        break;
                    case enReporterType.BookSupplier:
                        r = new ReporterRepository(uow).FindByID<BookSupplier>(reporterID);
                        break;
                    case enReporterType.PricingCommittee:
                        r = new ReporterRepository(uow).FindByID<PricingCommittee>(reporterID);
                        break;
                    case enReporterType.MinistryPayments:
                        r = new ReporterRepository(uow).FindByID<MinistryPaymentsUser>(reporterID);
                        break;
                }

                if (r == null)
                    return null;

                if (r is Publisher)
                    (r as Publisher).Email = newEmail;
                else if (r is Secretary)
                    (r as Secretary).Email = newEmail;
                else if (r is DistributionPoint)
                    (r as DistributionPoint).Email = newEmail;
                else if (r is PublicationsOffice)
                    (r as PublicationsOffice).Email = newEmail;
                else if (r is DataCenter)
                    (r as DataCenter).Email = newEmail;
                else if (r is Library)
                    (r as Library).Email = newEmail;
                else if (r is BookSupplier)
                    (r as BookSupplier).Email = newEmail;
                else if (r is PricingCommittee)
                    (r as PricingCommittee).Email = newEmail;

                uow.Commit();

                user.Email = newEmail;
                Membership.UpdateUser(user);

                if (r is Publisher)
                    ServiceWorker.SendPublisherUpdate(r.ID);
                else if (r is Secretary)
                    ServiceWorker.SendSecretaryUpdate(r.ID);
                else if (r is PublicationsOffice)
                    ServiceWorker.SendPublicationsOfficeUpdate(r.ID);
                else if (r is DataCenter)
                    ServiceWorker.SendDataCenterUpdate(r.ID);
                else if (r is DistributionPoint)
                    ServiceWorker.SendDistributionPointUpdate(r.ID);
                else if (r is Library)
                    ServiceWorker.SendLibraryUpdate(r.ID);
                else if (r is BookSupplier)
                    ServiceWorker.SendBookSupplierUpdate(r.ID);
                else if (r is PricingCommittee)
                    ServiceWorker.SendPricingCommitteeUpdate(r.ID);
                return true;
            }
        }

        #endregion

    }
}
