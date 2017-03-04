using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain;
using Eudoxus.eService;
using System.Threading;
using Eudoxus.Utils;

namespace Eudoxus.BusinessModel
{
    public static class ServiceWorker
    {
        #region [ Public Update Methods ]

        public static void SendPublisherUpdate(int publisherID, bool async = true)
        {
            GenericUpdateMethod(publisherID, UpdatePublisherMethod, async);
        }

        public static void SendSecretaryUpdate(int secretaryID, bool async = true)
        {
            GenericUpdateMethod(secretaryID, UpdateSecretaryMethod, async);
        }

        public static void SendPublicationsOfficeUpdate(int publicationsOfficeID, bool async = true)
        {
            GenericUpdateMethod(publicationsOfficeID, UpdatePublicationsOfficeMethod, async);
        }

        public static void SendDataCenterUpdate(int dataCenterID, bool async = true)
        {
            GenericUpdateMethod(dataCenterID, UpdateDataCenterMethod, async);
        }

        public static void SendDistributionPointUpdate(int distributionPointID, bool async = true)
        {
            GenericUpdateMethod(distributionPointID, UpdateDistributionPointMethod, async);
        }

        public static void SendLibraryUpdate(int libraryID, bool async = true)
        {
            GenericUpdateMethod(libraryID, UpdateLibraryMethod, async);
        }

        public static void SendBookSupplierUpdate(int bookSupplierID, bool async = true)
        {
            GenericUpdateMethod(bookSupplierID, UpdateBookSupplierMethod, async);
        }

        public static void SendPricingCommitteeUpdate(int pricingCommitteeID, bool async = true)
        {
            GenericUpdateMethod(pricingCommitteeID, UpdatePricingCommitteeMethod, async);
        }
        
        #endregion

        #region [ Private Update Methods ]
        private static void UpdatePublisherMethod(int publisherID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = publisherID };
                if (QueueWorker.IsPublisherQueued(publisherID))
                {//remove from queue
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                try
                {
                    ServiceClient.Update(QueueWorker.Current.GetPublisherForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.Publisher);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue publisher with ID : {0}", publisherID));
                    }
                }
            }
        }

        private static void UpdateSecretaryMethod(int secretaryID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = secretaryID };
                if (QueueWorker.IsSecretaryQueued(secretaryID))
                {//remove from queue
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                try
                {
                    ServiceClient.Update(QueueWorker.Current.GetSecretaryForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.Secretary);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue secretary with ID : {0}", secretaryID));
                    }
                }
            }
        }

        private static void UpdateDistributionPointMethod(int distributionPointID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = distributionPointID };
                try
                {
                    if (QueueWorker.IsDistributionPointQueued(distributionPointID))
                    {//remove from queue
                        QueueWorker.Current.RemoveFromQueue(tmpEntry);
                    }
                    ServiceClient.Update(QueueWorker.Current.GetDistributionPointForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.DistributionPoint);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue distributionPoint with ID : {0}", distributionPointID.ToString()));
                    }
                }
            }
        }

        private static void UpdatePublicationsOfficeMethod(int officeID)
        {
            var tmpEntry = new QueueEntry() { QueueDataID = officeID };
            if (QueueWorker.IsPublicationsOfficeQueued(officeID))
            {//remove from queue
                QueueWorker.Current.RemoveFromQueue(tmpEntry);
            }
            try
            {
                ServiceClient.Update(QueueWorker.Current.GetPublicationsOfficeForUpdate(tmpEntry, false).ToDto());
                QueueWorker.Current.RemoveFromQueue(tmpEntry);
            }
            catch (Exception ex)
            {
                try
                {
                    QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.PublicationsOffice);
                }
                catch (Exception ex2)
                {
                    LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue publicationsOffice with ID : {0}", officeID));
                }
            }
        }

        private static void UpdateDataCenterMethod(int dataCenterID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = dataCenterID };
                if (QueueWorker.IsDataCenterQueued(dataCenterID))
                {//remove from queue
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                try
                {
                    ServiceClient.Update(QueueWorker.Current.GetDataCenterForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.DataCenter);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue dataCenter with ID : {0}", dataCenterID));
                    }
                }
            }
        }

        private static void UpdateLibraryMethod(int libraryID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = libraryID };
                if (QueueWorker.IsLibraryQueued(libraryID))
                {//remove from queue
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                try
                {
                    ServiceClient.Update(QueueWorker.Current.GetLibraryForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.Library);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue library with ID : {0}", libraryID));
                    }
                }
            }
        }

        private static void UpdateBookSupplierMethod(int bookSupplierID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = bookSupplierID };
                if (QueueWorker.IsBookSupplierQueued(bookSupplierID))
                {//remove from queue
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                try
                {
                    ServiceClient.Update(QueueWorker.Current.GetBookSupplierForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.BookSupplier);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue bookSupplier with ID : {0}", bookSupplierID));
                    }
                }
            }
        }

        private static void UpdatePricingCommitteeMethod(int pricingCommitteeID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var tmpEntry = new QueueEntry() { QueueDataID = pricingCommitteeID };
                try
                {
                    if (QueueWorker.IsPricingCommitteeQueued(pricingCommitteeID))
                    {//remove from queue
                        QueueWorker.Current.RemoveFromQueue(tmpEntry);
                    }
                    ServiceClient.Update(QueueWorker.Current.GetPricingCommitteeForUpdate(tmpEntry, false).ToDto());
                    QueueWorker.Current.RemoveFromQueue(tmpEntry);
                }
                catch (Exception ex)
                {
                    try
                    {
                        QueueWorker.Current.AddToQueue(tmpEntry.QueueDataID, ex.Message, enReporterType.PricingCommittee);
                    }
                    catch (Exception ex2)
                    {
                        LogHelper.LogError(ex2, typeof(ServiceWorker), string.Format("Fail to add to queue pricingCommittee with ID : {0}", pricingCommitteeID.ToString()));
                    }
                }
            }
        }        

        #endregion

        #region [ Helpers ]

        static void GenericUpdateMethod(int id, Action<int> updateMethod, bool async)
        {
            if (async)
            {
                if (!QueueWorker.Current.IsInitialized)
                    return;
                ThreadPool.QueueUserWorkItem((x) =>
                {
                    updateMethod((int)x);
                }, id);
            }
            else
            {
                updateMethod(id);
            }
        }

        #endregion
    }
}
