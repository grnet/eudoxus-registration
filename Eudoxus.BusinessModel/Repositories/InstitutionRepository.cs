using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class InstitutionRepository : DomainRepository<HelpDeskEntities, Institution, int>
    {
        #region [ Base .ctors ]

        public InstitutionRepository() : base() { }

        public InstitutionRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}