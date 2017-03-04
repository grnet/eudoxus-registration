using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Online
    {
        public enReporterType OnlineReporterType
        {
            get { return (enReporterType)OnlineReporterTypeInt; }
            set
            {
                if (OnlineReporterTypeInt != (int)value)
                    OnlineReporterTypeInt = (int)value;
            }
        }

        public override string GetLabel()
        {
            return enReporterType.Online.GetLabel();
        }
    }
}
