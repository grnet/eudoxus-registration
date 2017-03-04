using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class AcademicsToCloneRepository : DomainRepository<HelpDeskEntities, AcademicsToClone, int>
    {
        #region [ Base .ctors ]

        public AcademicsToCloneRepository() : base() { }

        public AcademicsToCloneRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}