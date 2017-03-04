using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class RegisteredPublishers
    {
        private int _RecordCount = 0;

        public int CountRegisteredPublishersWithCriteria(Criteria<RegisteredPublisherView> criteria)
        {
            return _RecordCount;
        }

        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public List<RegisteredPublisherView> FindRegisteredPublishersWithCriteria(Criteria<RegisteredPublisherView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        //{
        //    int recordCount;

        //    criteria.StartRowIndex = startRowIndex;
        //    criteria.MaximumRows = maximumRows;
        //    criteria.SortExpression = sortExpression;

        //    using (IUnitOfWork uow = UnitOfWorkFactory.Create())
        //    {
        //        var registeredPublishers = new ViewsRepository(uow).FindRegisteredPublishersWithCriteria(criteria, out recordCount);
        //        _RecordCount = recordCount;
        //        return registeredPublishers;
        //    }
        //}
    }
}
