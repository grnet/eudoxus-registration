using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Eudoxus.BusinessModel;
using Imis.Domain;
using Eudoxus.eService;

namespace Eudoxus.Portal
{
    public enum ActivateUserResult
    {
        Success,
        UserNotFound,
        UserAlreadyActivated
    }

    public static class ActivateHelper
    {
        public static ActivateUserResult Activate<T>(MembershipUser u) where T : Reporter
        {
            if (u == null)
                return ActivateUserResult.UserNotFound;

            if (u.IsApproved)
                return ActivateUserResult.UserAlreadyActivated;

            IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();

            if (typeof(T) == typeof(Publisher))
            {
                return ActivatePublisher(u, unitOfWork);
            }
            else if (typeof(T) == typeof(Secretary))
            {
                return ActivateSecretary(u, unitOfWork);
            }
            else if (typeof(T) == typeof(DistributionPoint))
            {
                return ActivateDistributionPoint(u, unitOfWork);
            }
            else if (typeof(T) == typeof(PublicationsOffice))
            {
                return ActivatePublicationsOffice(u, unitOfWork);
            }
            else if (typeof(T) == typeof(DataCenter))
            {
                return ActivateDataCenter(u, unitOfWork);
            }
            else if (typeof(T) == typeof(Library))
            {
                return ActivateLibrary(u, unitOfWork);
            }
            else if (typeof(T) == typeof(BookSupplier))
            {
                return ActivateBookSupplier(u, unitOfWork);
            }
            else if (typeof(T) == typeof(PricingCommittee))
            {
                return ActivatePricingCommittee(u, unitOfWork);
            }
            else if (typeof(T) == typeof(MinistryPaymentsUser))
            {
                return ActivateMinistryPaymentsUser(u, unitOfWork);
            }

            return ActivateUserResult.UserNotFound;
        }


        private static ActivateUserResult ActivatePublisher(MembershipUser u, IUnitOfWork unitOfWork)
        {
            Publisher s = new PublisherRepository(unitOfWork).FindByUsername(u.UserName);
            s.PublisherDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.PublisherUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendPublisherUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateSecretary(MembershipUser u, IUnitOfWork unitOfWork)
        {
            Secretary s = new SecretaryRepository(unitOfWork).FindByUsername(u.UserName);
            s.SecretaryDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.SecretaryUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendSecretaryUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateDistributionPoint(MembershipUser u, IUnitOfWork unitOfWork)
        {
            DistributionPoint s = new DistributionPointRepository(unitOfWork).FindByUsername(u.UserName);
            s.DistributionPointDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.DistributionPointUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendDistributionPointUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivatePublicationsOffice(MembershipUser u, IUnitOfWork unitOfWork)
        {
            PublicationsOffice s = new PublicationsOfficeRepository(unitOfWork).FindByUsername(u.UserName);
            s.PublicationsOfficeDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.PublicationsOfficeUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendPublicationsOfficeUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateDataCenter(MembershipUser u, IUnitOfWork unitOfWork)
        {
            DataCenter s = new DataCenterRepository(unitOfWork).FindByUsername(u.UserName);
            s.DataCenterDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.DataCenterUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendDataCenterUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateLibrary(MembershipUser u, IUnitOfWork unitOfWork)
        {
            Library s = new LibraryRepository(unitOfWork).FindByUsername(u.UserName);
            s.LibraryDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.LibraryUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendLibraryUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateBookSupplier(MembershipUser u, IUnitOfWork unitOfWork)
        {
            BookSupplier s = new BookSupplierRepository(unitOfWork).FindByUsername(u.UserName);
            s.BookSupplierDetailsReference.Load();
            try
            {
                s.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.BookSupplierUser);
                Membership.UpdateUser(u);
                ServiceWorker.SendBookSupplierUpdate(s.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivatePricingCommittee(MembershipUser u, IUnitOfWork unitOfWork)
        {
            PricingCommittee p = new PricingCommitteeRepository(unitOfWork).FindByUsername(u.UserName);
            
            try
            {
                p.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.PricingCommitteeUser);
                Membership.UpdateUser(u);
                //ServiceWorker.SendPricingCommitteeUpdate(p.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static ActivateUserResult ActivateMinistryPaymentsUser(MembershipUser u, IUnitOfWork unitOfWork)
        {
            MinistryPaymentsUser p = new MinistryPaymentsUserRepository(unitOfWork).FindByUsername(u.UserName);

            try
            {
                p.IsActivated = true;
                unitOfWork.Commit();

                u.IsApproved = true;
                Roles.AddUserToRole(u.UserName, RoleNames.MinistryPaymentsUser);
                Membership.UpdateUser(u);                
                //ServiceWorker.SendLibraryUpdate(p.ID);
                return ActivateUserResult.Success;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}