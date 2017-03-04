using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Imis.Domain;
using Eudoxus.Queue;
using System.Threading;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public class QueueWorker : IQueueWorker
    {
        #region [ Thread-safe, lazy Singleton ]

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static QueueWorker Current
        {
            get
            {
                return Nested.dispatcher;
            }
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        class Nested
        {
            static Nested() { }
            internal static readonly QueueWorker dispatcher = new QueueWorker();
        }

        #endregion

        internal bool IsInitialized { get; set; }

        public static void Inititalize()
        {
            QueueWorker.Current.IsInitialized = true;
            ServiceQueue.Instance.Initialize(Current);
        }

        public static bool IsPublisherQueued(int publisherID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == publisherID);
        }

        public static bool IsSecretaryQueued(int secretaryID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == secretaryID);
        }

        public static bool IsDistributionPointQueued(int distributionPointID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == distributionPointID);
        }

        public static bool IsPublicationsOfficeQueued(int publicationsOfficeID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == publicationsOfficeID);
        }

        public static bool IsDataCenterQueued(int dataCenterID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == dataCenterID);
        }

        public static bool IsLibraryQueued(int libraryID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == libraryID);
        }

        public static bool IsBookSupplierQueued(int bookSupplierID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == bookSupplierID);
        }

        public static bool IsPricingCommitteeQueued(int pricingCommitteeID)
        {
            return new QueueEntryRepository().LoadAll().Any(x => x.QueueDataID == pricingCommitteeID);
        }

        void IQueueWorker.AddToQueue(IQueueEntry entry) { throw new NotImplementedException(); }

        public void AddToQueue(int reporterID, string message, enReporterType repoterType)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var entry = new QueueEntry();
                entry.QueueDataID = reporterID;
                entry.NoOfRetries = 0;
                entry.MaxNoOfRetries = ServiceQueue.Instance.Config.MaxNoOfRetries;
                entry.LastAttemptDate = DateTime.Now;
                ((IQueueWorker)this).SetQueueData(entry, new GenericQueueDataCollection() { new GenericQueueData() { 
                    NoOfRetry = 0, 
                    Message = message, 
                    ServerName = Environment.MachineName ,
                    ReporterType = repoterType
                } });
                uow.MarkAsNew(entry);
                uow.Commit();
            }
        }

        public void RemoveFromQueue(IQueueEntry entry)
        {
            using (HelpDeskEntities uow = new HelpDeskEntities())
            {
                var dbEntry = new QueueEntryRepository(uow).LoadAll().FirstOrDefault(x => x.QueueDataID == (int)entry.QueueDataID);
                if (dbEntry != null)
                {
                    uow.MarkAsDeleted(dbEntry);
                    uow.SaveChanges();
                }
            }
        }

        T IQueueWorker.GetQueueData<T>(IQueueEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.QueueDataXml))
                return default(T);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new StringReader(entry.QueueDataXml));
        }

        void IQueueWorker.SetQueueData(IQueueEntry queueEntry, object queueData)
        {
            XmlSerializer xs = new XmlSerializer(queueData.GetType());
            StringBuilder sb = new StringBuilder();
            xs.Serialize(XmlTextWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }), queueData);
            queueEntry.QueueDataXml = sb.ToString();
        }

        public void ProcessQueueEntry(IQueueEntry entry)
        {
            if (entry.MaxNoOfRetries == entry.NoOfRetries)
                return;

            using (HelpDeskEntities uow = new HelpDeskEntities())
            {
                var e = uow.QueueEntrySet.Single(x => x.QueueDataID == (int)entry.QueueDataID);
                try
                {
                    var data = ((IQueueWorker)this).GetQueueData<GenericQueueDataCollection>(e);
                    if (data.First().ReporterType == enReporterType.Publisher)
                    {
                        ServiceClient.Update(GetPublisherForUpdate(e, true).ToDto());
                        EudoxusOsyClient.Update(GetPublisherForUpdate(e, true).ToJsonDto());
                    }
                    else if (data.First().ReporterType == enReporterType.Secretary)
                    {
                        ServiceClient.Update(GetSecretaryForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.DistributionPoint)
                    {
                        ServiceClient.Update(GetDistributionPointForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.PublicationsOffice)
                    {
                        ServiceClient.Update(GetPublicationsOfficeForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.DataCenter)
                    {
                        ServiceClient.Update(GetDataCenterForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.Library)
                    {
                        ServiceClient.Update(GetLibraryForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.BookSupplier)
                    {
                        ServiceClient.Update(GetBookSupplierForUpdate(e, true).ToDto());
                    }
                    else if (data.First().ReporterType == enReporterType.PricingCommittee)
                    {   
                        ServiceClient.Update(GetPricingCommitteeForUpdate(e, true).ToDto());
                    }
                    
                    uow.QueueEntrySet.DeleteObject(e);
                }
                catch (Exception ex)
                {
                    e.NoOfRetries++;
                    e.LastAttemptDate = DateTime.Now;
                    var data = ((IQueueWorker)this).GetQueueData<GenericQueueDataCollection>(e);
                    data.Add(new GenericQueueData()
                    {
                        NoOfRetry = e.NoOfRetries,
                        Message = ex.Message,
                        ServerName = Environment.MachineName,
                        ReporterType = data.FirstOrDefault().ReporterType
                    });
                    ((IQueueWorker)this).SetQueueData(e, data);
                }
                uow.SaveChanges();
            }
        }

        bool QueueEntryExists(int queueDataID)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                return new QueueEntryRepository(uow).LoadAll().Any(x => x.QueueDataID == queueDataID);
            }
        }

        public vPublisher GetPublisherForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vPublisher.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vPublisher.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vSecretary GetSecretaryForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vSecretary.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vSecretary.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vDistributionPoint GetDistributionPointForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vDistributionPoint.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vDistributionPoint.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }            
        }

        public vPublicationsOffice GetPublicationsOfficeForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vPublicationsOffice.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vPublicationsOffice.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vDataCenter GetDataCenterForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vDataCenter.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vDataCenter.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vLibrary GetLibraryForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vLibrary.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vLibrary.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vBookSupplier GetBookSupplierForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vBookSupplier.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vBookSupplier.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public vPricingCommittee GetPricingCommitteeForUpdate(IQueueEntry queueEntry, bool checkIfExistsInQueue)
        {
            if (checkIfExistsInQueue)
            {
                if (QueueEntryExists((int)queueEntry.QueueDataID))
                    return new HelpDeskViewsEntities().vPricingCommittee.Single(x => x.ID == (int)queueEntry.QueueDataID);
                else
                    return null;
            }
            else
            {
                return new HelpDeskViewsEntities().vPricingCommittee.Single(x => x.ID == (int)queueEntry.QueueDataID);
            }
        }

        public void ProcessQueue(Action<bool> callback)
        {
            bool queueProcessed = new QueueEntryRepository().LoadAll().Any(x => x.NoOfRetries < x.MaxNoOfRetries);

            if (queueProcessed)
            {
                var entries = new QueueEntryRepository().LoadAll().Where(x => x.NoOfRetries < x.MaxNoOfRetries);
                foreach (var entry in entries)
                {
                    ProcessQueueEntry(entry);
                }

            }

            callback.Invoke(queueProcessed);
        }
    }
}
