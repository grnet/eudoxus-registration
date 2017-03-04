using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Reporters
    {
        private int _RecordCount = 0;

        public int CountReportersWithCriteria(Criteria<Reporter> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Reporter> FindReportersWithCriteria(ReporterCriteria criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var reporters = new ReporterRepository().FindReportersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return reporters;
            }
        }
    }
}
