using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class NewInstitutionRepository : DomainRepository<HelpDeskEntities, NewInstitution, int>
    {
        #region [ Base .ctors ]

        public NewInstitutionRepository() : base() { }

        public NewInstitutionRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}