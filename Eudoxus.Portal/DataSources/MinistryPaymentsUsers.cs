using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class MinistryPaymentsUsers
    {
        private int _RecordCount = 0;

        public int CountMinistryPaymentsUsersWithCriteria(Criteria<MinistryPaymentsUser> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<MinistryPaymentsUser> FindMinistryPaymentsUsersWithCriteria(Criteria<MinistryPaymentsUser> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var MinistryPaymentsUsers = new MinistryPaymentsUserRepository(uow).FindMinistryPaymentsUsersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return MinistryPaymentsUsers;
            }
        }
    }
}
