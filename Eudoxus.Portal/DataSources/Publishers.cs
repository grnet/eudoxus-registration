using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Publishers
    {
        private int _RecordCount = 0;

        public int CountPublishersWithCriteria(Criteria<Publisher> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Publisher> FindPublishersWithCriteria(Criteria<Publisher> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var publishers = new PublisherRepository(uow).FindPublishersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return publishers;
            }
        }
    }
}
