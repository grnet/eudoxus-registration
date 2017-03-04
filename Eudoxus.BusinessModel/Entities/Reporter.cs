using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Reporter : IUserChangeTracking
    {
        public static Reporter CreateReporterFromType(enReporterType reporterType)
        {
            switch (reporterType)
            {
                case enReporterType.Online:
                    return new Online();
                case enReporterType.Unknown:
                    return new Unknown();
                case enReporterType.Publisher:
                    return new Publisher();
                case enReporterType.Secretary:
                    return new Secretary();
                case enReporterType.DistributionPoint:
                    return new DistributionPoint();
                case enReporterType.Student:
                    return new Student();
                case enReporterType.Professor:
                    return new Professor();
                case enReporterType.PublicationsOffice:
                    return new PublicationsOffice();
                case enReporterType.DataCenter:
                    return new DataCenter();
                case enReporterType.Library:
                    return new Library();
                case enReporterType.BookSupplier:
                    return new BookSupplier();
                case enReporterType.PricingCommittee:
                    return new PricingCommittee();
                case enReporterType.MinistryPayments:
                    return new MinistryPaymentsUser();
            }
            return null;
        }

        public enReporterType ReporterType
        {
            get
            {
                if (this is Online)
                    return enReporterType.Online;
                else if (this is Unknown)
                    return enReporterType.Unknown;
                else if (this is Publisher)
                    return enReporterType.Publisher;
                else if (this is Secretary)
                    return enReporterType.Secretary;
                else if (this is DistributionPoint)
                    return enReporterType.DistributionPoint;
                else if (this is Student)
                    return enReporterType.Student;
                else if (this is Professor)
                    return enReporterType.Professor;
                else if (this is PublicationsOffice)
                    return enReporterType.PublicationsOffice;
                else if (this is DataCenter)
                    return enReporterType.DataCenter;
                else if (this is Library)
                    return enReporterType.Library;
                else if (this is BookSupplier)
                    return enReporterType.BookSupplier;
                else if (this is PricingCommittee)
                    return enReporterType.PricingCommittee;
                else if (this is MinistryPaymentsUser)
                    return enReporterType.MinistryPayments;

                return enReporterType.Unknown;
            }
        }

        public virtual string GetLabel()
        {   
            return string.Empty;
        }
    }
}
