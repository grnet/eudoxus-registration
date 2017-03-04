using System;
using System.Configuration;

namespace Eudoxus.Portal
{
    public static class Config
    {
        public static bool PublisherRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["PublisherRegistrationAllowed"]);
            }
        }

        public static bool SecretaryRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["SecretaryRegistrationAllowed"]);
            }
        }

        public static bool PublicationsOfficeRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["PublicationsOfficeRegistrationAllowed"]);
            }
        }

        public static bool DataCenterRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["DataCenterRegistrationAllowed"]);
            }
        }

        public static bool DistributionPointRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["DistributionPointRegistrationAllowed"]);
            }
        }

        public static bool LibraryRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["LibraryRegistrationAllowed"]);
            }
        }

        public static bool BookSupplierRegistrationAllowed
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["BookSupplierRegistrationAllowed"]);
            }
        }

        public static bool UseEService
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["UseeService"]);
            }
        }

        public static bool UsePaymentEService
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["UsePaymenteService"]);
            }
        }

        public static bool IsSSL
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
            }
        }
    }
}
