using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class PricingCommittees
    {
        private int _RecordCount = 0;

        public int CountPricingCommitteesWithCriteria(Criteria<PricingCommittee> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<PricingCommittee> FindPricingCommitteesWithCriteria(Criteria<PricingCommittee> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var PricingCommittees = new PricingCommitteeRepository(uow).FindPricingCommitteesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return PricingCommittees;
            }
        }
    }
}
