using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public enum enReporterType
    {
        /// <summary>
        /// Online Χρήστης
        /// </summary>
        Online = 1,

        /// <summary>
        /// Χρήστης χωρίς λογαριασμό
        /// </summary>
        Unknown = 2,

        /// <summary>
        /// Εκδότης
        /// </summary>
        Publisher = 3,

        /// <summary>
        /// Γραμματεία
        /// </summary>
        Secretary = 4,

        /// <summary>
        /// Σημείο Διανομής
        /// </summary>
        DistributionPoint = 5,

        /// <summary>
        /// Φοιτητής
        /// </summary>
        Student = 6,

        /// <summary>
        /// Καθηγητής
        /// </summary>
        Professor = 7,

        /// <summary>
        /// Γραφείο Δημοσιευμάτων
        /// </summary>
        PublicationsOffice = 8,

        /// <summary>
        /// Γραφείο Μηχανογράφησης
        /// </summary>
        DataCenter = 9,

        /// <summary>
        /// Βιβλιοθήκη
        /// </summary>
        Library = 10,

        /// <summary>
        /// Προμηθευτής Βιβλίων
        /// </summary>
        BookSupplier = 11,

        /// <summary>
        /// Χρήστης Γραφείου Αρωγής
        /// </summary>
        HelpdeskUser = 12,

        /// <summary>
        /// Admin Χρήστης
        /// </summary>
        AdminUser = 13,

        /// <summary>
        /// Επιτροπή Κοστολόγησης
        /// </summary>
        PricingCommittee = 14,

        /// <summary>
        /// Υπουργείο Πληρωμές
        /// </summary>
        MinistryPayments = 15
    }
}
