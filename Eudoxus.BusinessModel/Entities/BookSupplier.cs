using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class BookSupplierExtensions
    {
        public static BookSupplierDto ToDto(this vBookSupplier p)
        {
            BookSupplierDto dto = new BookSupplierDto();

            dto.InstitutionId = p.InstitutionID;
            dto.CertifierType = p.CertifierType;
            dto.CertifierName = p.CertifierName;

            dto.BookSupplierAddress = p.BookSupplierAddress;
            dto.BookSupplierZipCode = p.BookSupplierZipCode;
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

    public partial class BookSupplier
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
            return enReporterType.BookSupplier.GetLabel();
        }
    }
}
