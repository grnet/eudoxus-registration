using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Professor
    {
        public override string GetLabel()
        {
            return enReporterType.Professor.GetLabel();
        }
    }
}
