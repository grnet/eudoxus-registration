using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public class ReporterCriteria : Criteria<Reporter>
    {
        public ReporterCriteria()
        {
            ReporterType = new CriteriaField<enReporterType>();
            UnknownReporterType = new CriteriaField<enReporterType>();
            IdentificationNumber = new CriteriaField<string>();
            Description = new CriteriaField<string>();
            AcademicIdentifier = new CriteriaField<string>();
            PublisherAFM = new CriteriaField<string>();
            PublisherName = new CriteriaField<string>();
            PublisherTradeName = new CriteriaField<string>();
            AcademicID = new CriteriaField<int>();
            InstitutionID = new CriteriaField<int>();
            LibraryName = new CriteriaField<string>();
            DistributionPointName = new CriteriaField<string>();
            CertificationNumber = new CriteriaField<int>();
        }

        public CriteriaField<enReporterType> ReporterType { get; set; }

        public CriteriaField<enReporterType> UnknownReporterType { get; set; }

        public CriteriaField<string> IdentificationNumber { get; set; }

        public CriteriaField<string> Description { get; set; }

        public CriteriaField<string> AcademicIdentifier { get; set; }

        public CriteriaField<string> PublisherAFM { get; set; }

        public CriteriaField<string> PublisherName { get; set; }

        public CriteriaField<string> PublisherTradeName { get; set; }

        public CriteriaField<int> AcademicID { get; set; }

        public CriteriaField<int> InstitutionID { get; set; }

        public CriteriaField<string> LibraryName { get; set; }

        public CriteriaField<string> DistributionPointName { get; set; }

        public CriteriaField<int> CertificationNumber { get; set; }
    }
}
