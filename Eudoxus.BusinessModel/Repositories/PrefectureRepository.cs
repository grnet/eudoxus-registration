using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class PrefectureRepository : DomainRepository<HelpDeskEntities, Prefecture, int>
    {
        #region [ Base .ctors ]

        public PrefectureRepository() : base() { }

        public PrefectureRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion
    }
}