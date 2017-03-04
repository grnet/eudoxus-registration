using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enHandlerStatus
    {
        /// <summary>
        /// Δεν έχει ανατεθεί σε supervisor
        /// </summary>
        NotHandledBySupervisor = 0,

        /// <summary>
        /// Εκκρεμεί
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Έχει κλείσει
        /// </summary>
        Closed = 2
    }
}
