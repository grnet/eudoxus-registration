using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enMinistryAuthorization
    {
        None = 0,

        /// <summary>
        /// Read-Only
        /// </summary> 
        ReadOnly = 1,

        /// <summary>
        /// Διαχείρισης
        /// </summary>
        Admin = 2        
    }
}