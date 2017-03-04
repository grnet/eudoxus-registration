using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class IncidentReportPost : IUserChangeTracking
    {
        public enCallType CallType
        {
            get { return (enCallType)CallTypeInt; }
            set { CallTypeInt = (int)value; }
        }
    }
}
