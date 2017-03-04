using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Institution
    {
        public enInstitutionType InstitutionType
        {
            get { return (enInstitutionType)InstitutionTypeInt; }
            set
            {
                if (InstitutionTypeInt != (int)value)
                    InstitutionTypeInt = (int)value;
            }
        }
    }
}
