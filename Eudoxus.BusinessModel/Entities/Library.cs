using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class LibraryExtensions
    {
        public static LibraryDto ToDto(this vLibrary p)
        {
            LibraryDto dto = new LibraryDto();

            dto.InstitutionId = p.InstitutionID;
            dto.LibraryName = p.LibraryName;
            dto.LibraryOpeningHours = p.LibraryOpeningHours;
            dto.DirectorName = p.DirectorName;
            dto.LibraryPhone = p.LibraryPhone;
            dto.LibraryEmail = p.LibraryEmail;
            dto.LibraryURL = p.LibraryURL;

            dto.LibraryAddress = p.LibraryAddress;
            dto.LibraryZipCode = p.LibraryZipCode;
            dto.CityId = p.CityID;
            dto.PrefectureId = p.PrefectureID;
            dto.LibraryLocationURL = p.LibraryLocationURL;

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

    public partial class Library
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
            return enReporterType.Library.GetLabel();
        }
    }
}
