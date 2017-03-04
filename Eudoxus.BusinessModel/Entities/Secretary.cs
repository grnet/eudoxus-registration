using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class SecretaryExtensions
    {
        public static SecretaryDto ToDto(this vSecretary p)
        {
            SecretaryDto dto = new SecretaryDto();

            dto.SecretaryAcademicId = p.SecretaryAcademicID.Value;
            dto.RepresentativeType = p.RepresentativeType;
            dto.PresidentName = p.RepresentativeName;
            dto.SecretaryPhone = p.SecretaryPhone;
            dto.SecretaryEmail = p.SecretaryEmail;

            dto.SecretaryAddress = p.SecretaryAddress;
            dto.SecretaryZipCode = p.SecretaryZipCode;
            dto.SecretaryCityId = p.CityID;
            dto.SecretaryPrefectureId = p.PrefectureID;

            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;
            dto.ContactMobilePhone = p.ContactMobilePhone;
            dto.ContactEmail = p.ContactEmail;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated.Value;
            dto.VerificationStatus = p.VerificationStatus.Value;

            dto.AlternateContactName = p.AlternateContactName;
            dto.AlternateContactPhone = p.AlternateContactPhone;
            dto.AlternateContactMobilePhone = p.AlternateContactMobilePhone;
            dto.AlternateContactEmail = p.AlternateContactEmail;
            dto.StudiesDuration = p.Semesters;

            dto.ID = p.ID;

            return dto;
        }
    }
    public partial class Secretary
    {
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
            return enReporterType.Secretary.GetLabel();
        }
    }
}
