using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain;

namespace Eudoxus.BusinessModel
{
    public class QueueEntryRepository : Imis.Domain.EF.DomainRepository<HelpDeskEntities, QueueEntry, int>
    {
        public QueueEntryRepository() { }

        public QueueEntryRepository(IUnitOfWork uow) : base(uow) { }
    }
}
