using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enPublisherType
    {
        /// <summary>
        /// Νομικό Πρόσωπο
        /// </summary> 
        LegalPerson = 1,

        /// <summary>
        /// Αυτοεκδότης
        /// </summary>
        SelfPublisher = 2,

        /// <summary>
        /// Διαθέτης Δωρεάν Ηλεκτρονικών Σημειώσεων
        /// </summary>
        EbookPublisher = 3
    }
}