using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Unknown
    {
        public enReporterType UnknownReporterType
        {
            get { return (enReporterType)UnknownReporterTypeInt; }
            set
            {
                if (UnknownReporterTypeInt != (int)value)
                    UnknownReporterTypeInt = (int)value;
            }
        }

        public override string GetLabel()
        {
            return enReporterType.Unknown.GetLabel();
        }
    }
}