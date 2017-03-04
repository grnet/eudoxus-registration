using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public class AcademicDetailCriteria : Criteria<AcademicDetail>
    {
        public AcademicDetailCriteria()
        {
            SecretaryExists = new CriteriaField<bool>();
        }

        public CriteriaField<bool> SecretaryExists { get; set; }
    }
}
