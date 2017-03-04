using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class AcademicsToAddRepository : DomainRepository<HelpDeskEntities, AcademicsToAdd, int>
    {
        #region [ Base .ctors ]

        public AcademicsToAddRepository() : base() { }

        public AcademicsToAddRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}