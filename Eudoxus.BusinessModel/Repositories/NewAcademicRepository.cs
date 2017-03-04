using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class NewAcademicRepository : DomainRepository<HelpDeskEntities, NewAcademic, int>
    {
        #region [ Base .ctors ]

        public NewAcademicRepository() : base() { }

        public NewAcademicRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}