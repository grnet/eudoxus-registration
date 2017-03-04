using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class LessonReports
    {
        private int _RecordCount = 0;

        public int CountLessonReportsWithCriteria(Criteria<LessonReport> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<LessonReport> FindLessonReportsWithCriteria(LessonReportCriteria criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            IUnitOfWork uow = UnitOfWorkFactory.Create();

            var incidentReports = new LessonReportRepository(uow).FindLessonReportsWithCriteria(criteria, out recordCount);
            _RecordCount = recordCount;
            return incidentReports;

        }
    }
}
