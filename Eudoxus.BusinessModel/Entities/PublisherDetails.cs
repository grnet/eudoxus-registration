using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class PublisherDetails : IUserChangeTracking
    {
        public enIdentificationType LegalPersonIdentificationType
        {
            get
            {
                if (LegalPersonIdentificationTypeInt.HasValue)
                    return (enIdentificationType)LegalPersonIdentificationTypeInt.Value;
                return enIdentificationType.None;
            }
            set
            {
                if (!LegalPersonIdentificationTypeInt.HasValue || LegalPersonIdentificationTypeInt.Value != (int)value)
                    LegalPersonIdentificationTypeInt = (int)value;
            }
        }

        public enIdentificationType SelfPublisherIdentificationType
        {
            get
            {
                if (SelfPublisherIdentificationTypeInt.HasValue)
                    return (enIdentificationType)SelfPublisherIdentificationTypeInt.Value;
                return enIdentificationType.None;
            }
            set
            {
                if (!SelfPublisherIdentificationTypeInt.HasValue || SelfPublisherIdentificationTypeInt.Value != (int)value)
                    SelfPublisherIdentificationTypeInt = (int)value;
            }
        }

        public enIdentificationType ContactIdentificationType
        {
            get
            {
                if (ContactIdentificationTypeInt.HasValue)
                    return (enIdentificationType)ContactIdentificationTypeInt;
                return enIdentificationType.None;
            }
            set
            {
                if (!ContactIdentificationTypeInt.HasValue || ContactIdentificationTypeInt.Value != (int)value)
                    ContactIdentificationTypeInt = (int)value;
            }
        }
    }
}
