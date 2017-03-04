using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.eService;

namespace Eudoxus.BusinessModel
{
    public static class PublisherExtensions
    {
        public static PublisherDto ToDto(this vPublisher p)
        {
            PublisherDto dto = new PublisherDto();
            dto.ID = p.ID;

            dto.PublisherType = p.PublisherType.Value;
            dto.PublisherName = p.PublisherName;
            dto.PublisherTradeName = p.PublisherTradeName;
            dto.PublisherAFM = p.PublisherAFM;
            dto.PublisherDOY = p.PublisherDOY;
            dto.PublisherPhone = p.PublisherPhone;
            dto.PublisherMobilePhone = p.PublisherMobilePhone;
            dto.PublisherFax = p.PublisherFax;
            dto.PublisherEmail = p.PublisherEmail;
            dto.PublisherURL = p.PublisherURL;

            dto.PublisherAddress = p.PublisherAddress;
            dto.PublisherZipCode = p.PublisherZipCode;
            dto.CityId = p.CityID;
            dto.PrefectureId = p.PrefectureID;

            dto.LegalPersonName = p.LegalPersonName;
            dto.LegalPersonPhone = p.LegalPersonPhone;
            dto.LegalPersonEmail = p.LegalPersonEmail;
            if (p.LegalPersonIdentificationType.HasValue)
                dto.LegalPersonIdentificationType = p.LegalPersonIdentificationType.Value;
            dto.LegalPersonIdentificationNumber = p.LegalPersonIdentificationNumber;
            dto.LegalPersonIdentificationIssuer = p.LegalPersonIdentificationIssuer;
            if (p.LegalPersonIdentificationIssueDate.HasValue)
                dto.LegalPersonIdentificationIssueDate = p.LegalPersonIdentificationIssueDate.Value;

            if (p.SelfRepresented.HasValue)
                dto.SelfRepresented = p.SelfRepresented.Value ? 1 : 0;

            if (p.SelfPublisherIdentificationType.HasValue)
                dto.SelfPublisherIdentificationType = p.SelfPublisherIdentificationType.Value;
            dto.SelfPublisherIdentificationNumber = p.SelfPublisherIdentificationNumber;
            dto.SelfPublisherIdentificationIssuer = p.SelfPublisherIdentificationIssuer;
            if (p.SelfPublisherIdentificationIssueDate.HasValue)
                dto.SelfPublisherIdentificationIssueDate = p.SelfPublisherIdentificationIssueDate.Value;

            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;
            dto.ContactMobilePhone = p.ContactMobilePhone;
            dto.ContactEmail = p.ContactEmail;
            dto.ContactIdentificationType = p.ContactIdentificationType.Value;
            dto.ContactIdentificationNumber = p.ContactIdentificationNumber;
            if (p.ContactIdentificationIssueDate.HasValue)
                dto.ContactIdentificationIssueDate = p.ContactIdentificationIssueDate.Value;
            dto.ContactIdentificationIssuer = p.ContactIdentificationIssuer;

            dto.AlternateContactName = p.AlternateContactName;
            dto.AlternateContactPhone = p.AlternateContactPhone;
            dto.AlternateContactMobilePhone = p.AlternateContactMobilePhone;
            dto.AlternateContactEmail = p.AlternateContactEmail;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated.Value ? 1 : 0;
            dto.VerificationStatus = p.VerificationStatus.Value;

            return dto;

        }

        public static PublisherEudoxusOsyDto ToJsonDto(this vPublisher p)
        {
            PublisherEudoxusOsyDto dto = new PublisherEudoxusOsyDto();

            dto.PublisherKpsID = p.ID;
            dto.PublisherType = p.PublisherType.Value;
            dto.PublisherName = p.PublisherName;
            dto.PublisherTradeName = p.PublisherTradeName;
            dto.PublisherAFM = p.PublisherAFM;
            dto.PublisherDOY = p.PublisherDOY;
            dto.PublisherPhone = p.PublisherPhone;
            dto.PublisherMobilePhone = p.PublisherMobilePhone;
            dto.PublisherFax = p.PublisherFax;
            dto.PublisherEmail = p.PublisherEmail;
            dto.PublisherUrl = p.PublisherURL;

            dto.PublisherAddress = p.PublisherAddress;
            dto.PublisherZipCode = p.PublisherZipCode;
            dto.PublisherCityID = p.CityID;
            dto.PublisherPrefectureID = p.PrefectureID;

            dto.LegalPersonName = p.LegalPersonName;
            dto.LegalPersonPhone = p.LegalPersonPhone;
            dto.LegalPersonEmail = p.LegalPersonEmail;
            if (p.LegalPersonIdentificationType.HasValue)
                dto.LegalPersonIdentificationType = p.LegalPersonIdentificationType.Value;
            dto.LegalPersonIdentificationNumber = p.LegalPersonIdentificationNumber;
            dto.LegalPersonIdentificationIssuer = p.LegalPersonIdentificationIssuer;
            if (p.LegalPersonIdentificationIssueDate.HasValue)
                dto.LegalPersonIdentificationIssueDate = p.LegalPersonIdentificationIssueDate.Value;

            dto.IsSelfRepresented = p.SelfRepresented;

            if (p.SelfPublisherIdentificationType.HasValue)
                dto.SelfPublisherIdentificationType = p.SelfPublisherIdentificationType.Value;
            dto.SelfPublisherIdentificationNumber = p.SelfPublisherIdentificationNumber;
            dto.SelfPublisherIdentificationIssuer = p.SelfPublisherIdentificationIssuer;
            if (p.SelfPublisherIdentificationIssueDate.HasValue)
                dto.SelfPublisherIdentificationIssueDate = p.SelfPublisherIdentificationIssueDate.Value;

            dto.ContactName = p.ContactName;
            dto.ContactPhone = p.ContactPhone;
            dto.ContactMobilePhone = p.ContactMobilePhone;
            dto.ContactEmail = p.ContactEmail;
            dto.ContactIdentificationType = p.ContactIdentificationType.Value;
            dto.ContactIdentificationNumber = p.ContactIdentificationNumber;
            if (p.ContactIdentificationIssueDate.HasValue)
                dto.ContactIdentificationIssueDate = p.ContactIdentificationIssueDate.Value;
            dto.ContactIdentificationIssuer = p.ContactIdentificationIssuer;

            dto.AlternateContactName = p.AlternateContactName;
            dto.AlternateContactPhone = p.AlternateContactPhone;
            dto.AlternateContactMobilePhone = p.AlternateContactMobilePhone;
            dto.AlternateContactEmail = p.AlternateContactEmail;

            dto.Username = p.UserName;
            dto.Password = p.Password;
            dto.PasswordSalt = p.PasswordSalt;
            dto.Email = p.Email;
            dto.IsActivated = p.IsActivated.Value;
            dto.VerificationStatus = p.VerificationStatus.Value;

            return dto;

        }
    }

    public partial class Publisher
    {

        public enPublisherType PublisherType
        {
            get { return (enPublisherType)PublisherTypeInt; }
            set
            {
                if (PublisherTypeInt != (int)value)
                    PublisherTypeInt = (int)value;
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
            return enReporterType.Publisher.GetLabel();
        }
    }
}
