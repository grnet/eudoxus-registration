using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public static class HelpDeskConstants
    {
        public const int DEFAULT_SUBSYSTEM_ID = 1;
        public const int FOREIGN_PREFECTURE = 52;
        public const int FOREIGN_CITY = 9999;
    }

    public static class RoleNames
    {
        public const string PublisherUser = "PublisherUser";
        public const string SecretaryUser = "SecretaryUser";
        public const string DistributionPointUser = "DistributionPointUser";
        public const string PublicationsOfficeUser = "PublicationsOfficeUser";
        public const string DataCenterUser = "DataCenterUser";
        public const string LibraryUser = "LibraryUser";
        public const string BookSupplierUser = "BookSupplierUser";
        public const string PricingCommitteeUser = "PricingCommitteeUser";
        public const string MinistryPaymentsUser = "MinistryPaymentsUser";

        public const string Helpdesk = "Helpdesk";
        public const string SuperHelpdesk = "SuperHelpdesk";
        public const string Supervisor = "Supervisor";

        public const string Reports = "Reports";
        public const string SuperReports = "SuperReports";

        public const string PublisherCommunicator = "PublisherCommunicator";
        public const string SecretaryCommunicator = "SecretaryCommunicator";
        public const string PublisherSecretaryCommunicator = "PublisherSecretaryCommunicator";

        public const string SystemAdministrator = "SystemAdministrator";
    }

    public static class AutomaticIncidentTypes
    {
        public const int PublisherVerification = 3;
        public const int PublisherGeneralInfo = 7;
        public const int SecretaryVerification = 9;
        public const int SecretaryLessonSubmission = 10;
        public const int DistributionPointVerification = 15;
        public const int PublicationsOfficeVerification = 27;
        public const int DataCenterVerification = 33;
        public const int LibraryVerification = 40;
        public const int BookSupplierVerification = 47;
        public const int PricingCommitteeVerification = 53;
        public const int MinistryPaymentsUserVerification = 56;
    }

    public static class AutomaticIncidentReportMessages
    {
        public const string UserUnLocked = "Ξεκλείδωμα χρήστη με username {0} από το χρήστη {1}";
        public const string UserVerified = "Πιστοποίηση από το χρήστη {0}";
        public const string UserUnVerified = "Από-πιστοποίηση από το χρήστη {0}";
        public const string UserRejected = "Απόρριψη από το χρήστη {0}";
        public const string UserRestored = "Επαναφορά από το χρήστη {0}";
        public const string UserEmailChanged = "Αλλαγή e-mail χρήστη με username {0} από το χρήστη {1} από {2} σε {3}";
        public const string LessonSubmission = "Ακαδημαϊκό Έτος {0} - Έλεγχος για σωστή καταχώριση μαθημάτων/συγγραμμάτων";
    }
}
