﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class HelpdeskUsers
    {
        private int _RecordCount = 0;

        public int CountHelpdeskUsersWithCriteria(Criteria<HelpdeskUser> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<HelpdeskUser> FindHelpdeskUsersWithCriteria(Criteria<HelpdeskUser> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var helpdeskUsers = new HelpdeskUserRepository(uow).FindHelpdeskUsersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return helpdeskUsers;
            }
        }
    }
}
