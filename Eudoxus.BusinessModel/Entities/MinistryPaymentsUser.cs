using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class MinistryPaymentsUserExtensions
    {
        public static MinistryPaymentsUserDto ToJsonDto(this vMinistryPaymentsUser m)
        {
            MinistryPaymentsUserDto dto = new MinistryPaymentsUserDto()
            {
                ID = m.ID,
                ContactName = m.ContactName,
                ContactPhone = m.ContactPhone,
                ContactEmail = m.ContactEmail,
                MinistryAuthorization = m.MinistryAuthorization,
                Description = m.Description,
                Username = m.UserName,
                Email = m.Email,
                VerificationStatus = (int)m.VerificationStatus,
                IsActivated = m.IsActivated,
                Password = m.Password,
                PasswordSalt = m.PasswordSalt
            };

            return dto;

        }
    }

    public partial class MinistryPaymentsUser
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
            return enReporterType.MinistryPayments.GetLabel();
        }
    }
}
