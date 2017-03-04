using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class RegionRepository : DomainRepository<HelpDeskEntities, Region, int>
    {
        #region [ Base .ctors ]

        public RegionRepository() : base() { }

        public RegionRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}