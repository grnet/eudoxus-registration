using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enReportSubmissionType
    {
        /// <summary>
        /// Από το Γραφείο Αρωγής
        /// </summary>
        Helpdesk = 1,

        /// <summary>
        /// Από το Portal
        /// </summary>
        Portal = 2,

        /// <summary>
        /// Από συνδεδεμένο χρήστη
        /// </summary>
        LoggedInUser = 3,

        /// <summary>
        /// Από τον εσωτερικό reporter
        /// </summary>
        InternalReporter = 4
    }
}
