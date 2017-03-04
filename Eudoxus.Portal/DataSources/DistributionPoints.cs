using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class DistributionPoints
    {
        private int _RecordCount = 0;

        public int CountDistributionPointsWithCriteria(Criteria<DistributionPoint> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<DistributionPoint> FindDistributionPointsWithCriteria(Criteria<DistributionPoint> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var distributionPoints = new DistributionPointRepository(uow).FindDistributionPointsWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return distributionPoints;
            }
        }
    }
}
