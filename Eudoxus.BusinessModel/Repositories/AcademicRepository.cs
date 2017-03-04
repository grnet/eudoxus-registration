using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class AcademicRepository : DomainRepository<HelpDeskEntities, Academic, int>
    {
        #region [ Base .ctors ]

        public AcademicRepository() : base() { }

        public AcademicRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}