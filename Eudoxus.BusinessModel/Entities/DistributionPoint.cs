using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class DistributionPointExtensions
    {
        public static DistributionPointDto ToDto(this vDistributionPoint p)
        {
            DistributionPointDto dto = new DistributionPointDto();

            dto.DistributionPointName = p.DistributionPointName;
            dto.DistributionPointOpeningHours = p.DistributionPointOpeningHours;
            dto.DistributionPointPhone = p.DistributionPointPhone;
            dto.DistributionPointMobilePhone = p.DistributionPointMobilePhone;
            dto.DistributionPointFax = p.DistributionPointFax;
            dto.DistributionPointEmail = p.DistributionPointEmail;

            dto.DistributionPointAddress = p.DistributionPointAddress;
            dto.DistributionPointZipCode = p.DistributionPointZipCode;
            dto.DistributionPointCityID = p.DistributionPointCityID;
            dto.DistributionPointPrefectureID = p.DistributionPointPrefectureID;
            dto.DistributionPointLocationURL = p.DistributionPointLocationURL;

            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;
            dto.ContactMobilePhone = p.ContactMobilePhone;
            dto.ContactEmail = p.ContactEmail;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated;
            dto.VerificationStatus = p.VerificationStatus;

            switch ((enDistributionPointType)p.DistributionPointType)
            {
                case (enDistributionPointType.Store):
                    dto.DistributionPointType = "DistributionPoint";
                    break;
                default:
                    dto.DistributionPointType = "Secretariat";
                    dto.DistributionPointInstitutionID = p.DistributionPointInstitutionID.Value;
                    break;
            }

            dto.ID = p.ID;

            return dto;
        }
    }

    public partial class DistributionPoint
    {
        public enDistributionPointType DistributionPointType
        {
            get { return (enDistributionPointType)DistributionPointTypeInt; }
            set
            {
                if (DistributionPointTypeInt != (int)value)
                    DistributionPointTypeInt = (int)value;
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
            return enReporterType.DistributionPoint.GetLabel();
        }
    }
}
