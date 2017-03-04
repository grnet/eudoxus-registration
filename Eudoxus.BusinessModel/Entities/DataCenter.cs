using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class DataCenterExtensions
    {
        public static DataCenterDto ToDto(this vDataCenter p)
        {
            DataCenterDto dto = new DataCenterDto();

            dto.InstitutionId = p.InstitutionID;
            dto.DirectorName = p.DirectorName;
            dto.DataCenterPhone = p.DataCenterPhone;
            dto.DataCenterEmail = p.DataCenterEmail;

            dto.DataCenterAddress = p.DataCenterAddress;
            dto.DataCenterZipCode = p.DataCenterZipCode;
            dto.CityId = p.CityID;
            dto.PrefectureId = p.PrefectureID;

            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;
            dto.ContactMobilePhone = p.ContactMobilePhone;
            dto.ContactEmail = p.ContactEmail;

            dto.AlternateContactName = p.AlternateContactName;
            dto.AlternateContactPhone = p.AlternateContactPhone;
            dto.AlternateContactMobilePhone = p.AlternateContactMobilePhone;
            dto.AlternateContactEmail = p.AlternateContactEmail;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated;
            dto.VerificationStatus = p.VerificationStatus;

            dto.ID = p.ID;

            return dto;
        }
    }

    public partial class DataCenter
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
            return enReporterType.DataCenter.GetLabel();
        }
    }
}
