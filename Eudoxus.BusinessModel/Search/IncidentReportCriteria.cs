using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public class IncidentReportCriteria : Criteria<IncidentReport>
    {
        public IncidentReportCriteria()
        {
            OnlineReporterType = new CriteriaField<enReporterType>();
            ReportText = new CriteriaField<string>();
            AcademicID = new CriteriaField<int>();
            OnlineAcademicID = new CriteriaField<int>();
        }

        public CriteriaField<enReporterType> OnlineReporterType { get; set; }
        public CriteriaField<string> ReportText { get; set; }
        public CriteriaField<int> AcademicID { get; set; }
        public CriteriaField<int> OnlineAcademicID { get; set; }
    }
}
