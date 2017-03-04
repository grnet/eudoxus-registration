using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class IncidentReports
    {
        private int _RecordCount = 0;

        public int CountIncidentReportsWithCriteria(Criteria<IncidentReport> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<IncidentReport> FindIncidentReportsWithCriteria(IncidentReportCriteria criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            IUnitOfWork uow = UnitOfWorkFactory.Create();

            var incidentReports = new IncidentReportRepository(uow).FindIncidentReportsWithCriteria(criteria, out recordCount);
            _RecordCount = recordCount;
            return incidentReports;

        }
    }
}
