using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class SubSystemReporterType
    {
        public enReporterType ReporterType
        {
            get { return (enReporterType)ReporterTypeInt; }
            set
            {
                if (ReporterTypeInt != (int)value)
                    ReporterTypeInt = (int)value;
            }
        }
    }
}
