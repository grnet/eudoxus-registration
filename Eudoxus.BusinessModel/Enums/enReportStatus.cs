using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enReportStatus
    {
        /// <summary>
        /// Εκκρεμεί
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Έχει απαντηθεί
        /// </summary>
        Answered = 2,

        /// <summary>
        /// Έχει κλείσει
        /// </summary>
        Closed = 3
    }
}
