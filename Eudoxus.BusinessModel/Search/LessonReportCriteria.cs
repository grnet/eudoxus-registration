using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public class LessonReportCriteria : Criteria<LessonReport>
    {
        public LessonReportCriteria()
        {
            AcademicID = new CriteriaField<int>();
        }

        public CriteriaField<int> AcademicID { get; set; }
    }
}
