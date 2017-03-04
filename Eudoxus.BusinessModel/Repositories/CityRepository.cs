using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class CityRepository : DomainRepository<HelpDeskEntities, City, int>
    {
        #region [ Base .ctors ]

        public CityRepository() : base() { }

        public CityRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}