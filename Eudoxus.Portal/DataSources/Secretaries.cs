using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Secretaries
    {
        private int _RecordCount = 0;

        public int CountSecretariesWithCriteria(Criteria<Secretary> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Secretary> FindSecretariesWithCriteria(Criteria<Secretary> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var secretaries = new SecretaryRepository(uow).FindSecretariesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return secretaries;
            }
        }
    }
}
