using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class PricingCommitteeExtensions
    {
        public static PricingCommitteeDto ToDto(this vPricingCommittee p)
        {
            PricingCommitteeDto dto = new PricingCommitteeDto();
            dto.ID = p.ID;

            dto.PricingCommitteeType = p.PricingCommitteeType;
            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;            
            dto.ContactEmail = p.ContactEmail;
            dto.PricingCommitteeAuthorization = p.PricingCommitteeAuthorization;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated;
            dto.VerificationStatus = (int)p.VerificationStatus;

            return dto;

        }
    }

    public partial class PricingCommittee
    {
        public enMinistryAuthorization MinistryAuthorization
        {
            get { return (enMinistryAuthorization)MinistryAuthorizationInt; }
            set
            {
                if (MinistryAuthorizationInt != (int)value)
                    MinistryAuthorizationInt = (int)value;
            }
        }

        public enVerificationStatus VerificationStatus
        {
            get { return (enVerificationStatus)VerificationStatusInt; }
            set
            {
                if (VerificationStatusInt != (int)value)
                    VerificationStatusInt = (int)value;
            }
        }

        public override string GetLabel()
        {
            return enReporterType.PricingCommittee.GetLabel();
        }
    }
}
