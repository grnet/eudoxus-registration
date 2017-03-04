using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Eudoxus.eService
{
    [System.SerializableAttribute()]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "Publisher")]
    public partial class PublisherDto
    {
        private int _publisherType;

        private string _publisherName;

        private string _publisherTradeName;

        private string _publisherAFMField;

        private string _publisherDOYField;

        private string _publisherPhoneField;

        private string _publisherMobilePhoneField;

        private string _publisherFaxField;

        private string _publisherEmailField;

        private string _publisherURLField;

        private string _publisherAddressField;

        private string _publisherZipCodeField;

        private int _cityIdField;

        private int _prefectureIdField;

        private string _legalPersonNameField;

        private string _legalPersonPhoneField;

        private string _legalPersonEmailField;

        private int? _legalPersonIdentificationTypeField;

        private string _legalPersonIdentificationNumberField;

        private string _legalPersonIdentificationIssuerField;

        private System.DateTime? _legalPersonIdentificationIssueDateField;

        private int _selfRepresentedField;

        private int? _selfPublisherIdentificationTypeField;

        private string _selfPublisherIdentificationNumberField;

        private string _selfPublisherIdentificationIssuerField;

        private System.DateTime? selfPublisherIdentificationIssueDateField;        

        private string contactNameField;

        private string contactPhoneField;

        private string contactMobilePhoneField;

        private string contactEmailField;

        private int contactIdentificationTypeField;

        private string contactIdentificationNumberField;

        private string contactIdentificationIssuerField;

        private System.DateTime contactIdentificationIssueDateField;

        private string alternateContactNameField;

        private string alternateContactPhoneField;

        private string alternateContactMobilePhoneField;

        private string alternateContactEmailField;

        private string usernameField;

        private string passwordField;

        private string passwordSaltField;

        private string emailField;

        private int isActivatedField;

        private int verificationStatusField;

        private long idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int PublisherType
        {
            get
            {
                return this._publisherType;
            }
            set
            {
                this._publisherType = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherName
        {
            get
            {
                return this._publisherName;
            }
            set
            {
                this._publisherName = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherTradeName
        {
            get
            {
                return this._publisherTradeName;
            }
            set
            {
                this._publisherTradeName = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherAFM
        {
            get
            {
                return this._publisherAFMField;
            }
            set
            {
                this._publisherAFMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherDOY
        {
            get
            {
                return this._publisherDOYField;
            }
            set
            {
                this._publisherDOYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherPhone
        {
            get
            {
                return this._publisherPhoneField;
            }
            set
            {
                this._publisherPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherMobilePhone
        {
            get
            {
                return this._publisherMobilePhoneField;
            }
            set
            {
                this._publisherMobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherFax
        {
            get
            {
                return this._publisherFaxField;
            }
            set
            {
                this._publisherFaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherEmail
        {
            get
            {
                return this._publisherEmailField;
            }
            set
            {
                this._publisherEmailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherURL
        {
            get
            {
                return this._publisherURLField;
            }
            set
            {
                this._publisherURLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherAddress
        {
            get
            {
                return this._publisherAddressField;
            }
            set
            {
                this._publisherAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PublisherZipCode
        {
            get
            {
                return this._publisherZipCodeField;
            }
            set
            {
                this._publisherZipCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int CityId
        {
            get
            {
                return this._cityIdField;
            }
            set
            {
                this._cityIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int PrefectureId
        {
            get
            {
                return this._prefectureIdField;
            }
            set
            {
                this._prefectureIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LegalPersonName
        {
            get
            {
                return this._legalPersonNameField;
            }
            set
            {
                this._legalPersonNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LegalPersonPhone
        {
            get
            {
                return this._legalPersonPhoneField;
            }
            set
            {
                this._legalPersonPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LegalPersonEmail
        {
            get
            {
                return this._legalPersonEmailField;
            }
            set
            {
                this._legalPersonEmailField = value;
            }
        }

        public bool ShouldSerializeLegalPersonIdentificationType()
        {
            return LegalPersonIdentificationType != null;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int? LegalPersonIdentificationType
        {
            get
            {
                return this._legalPersonIdentificationTypeField;
            }
            set
            {
                this._legalPersonIdentificationTypeField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool LegalPersonIdentificationTypeSpecified
        //{
        //    get
        //    {
        //        return this._legalPersonIdentificationTypeFieldSpecified;
        //    }
        //    set
        //    {
        //        this._legalPersonIdentificationTypeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LegalPersonIdentificationNumber
        {
            get
            {
                return this._legalPersonIdentificationNumberField;
            }
            set
            {
                this._legalPersonIdentificationNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LegalPersonIdentificationIssuer
        {
            get
            {
                return this._legalPersonIdentificationIssuerField;
            }
            set
            {
                this._legalPersonIdentificationIssuerField = value;
            }
        }

        public bool ShouldSerializeLegalPersonIdentificationIssueDate()
        {
            return LegalPersonIdentificationIssueDate != null;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime? LegalPersonIdentificationIssueDate
        {
            get
            {
                return this._legalPersonIdentificationIssueDateField;
            }
            set
            {
                this._legalPersonIdentificationIssueDateField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool LegalPersonIdentificationIssueDateSpecified
        //{
        //    get
        //    {
        //        return this._legalPersonIdentificationIssueDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this._legalPersonIdentificationIssueDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int SelfRepresented
        {
            get
            {
                return this._selfRepresentedField;
            }
            set
            {
                this._selfRepresentedField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool SelfRepresentedSpecified
        //{
        //    get
        //    {
        //        return this._selfRepresentedFieldSpecified;
        //    }
        //    set
        //    {
        //        this._selfRepresentedFieldSpecified = value;
        //    }
        //}

        public bool ShouldSerializeSelfPublisherIdentificationType()
        {
            return SelfPublisherIdentificationType != null;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int? SelfPublisherIdentificationType
        {
            get
            {
                return this._selfPublisherIdentificationTypeField;
            }
            set
            {
                this._selfPublisherIdentificationTypeField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool SelfPublisherIdentificationTypeSpecified
        //{
        //    get
        //    {
        //        return this._selfPublisherIdentificationTypeFieldSpecified;
        //    }
        //    set
        //    {
        //        this._selfPublisherIdentificationTypeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SelfPublisherIdentificationNumber
        {
            get
            {
                return this._selfPublisherIdentificationNumberField;
            }
            set
            {
                this._selfPublisherIdentificationNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SelfPublisherIdentificationIssuer
        {
            get
            {
                return this._selfPublisherIdentificationIssuerField;
            }
            set
            {
                this._selfPublisherIdentificationIssuerField = value;
            }
        }

        public bool ShouldSerializeSelfPublisherIdentificationIssueDate()
        {
            return SelfPublisherIdentificationIssueDate != null;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime? SelfPublisherIdentificationIssueDate
        {
            get
            {
                return this.selfPublisherIdentificationIssueDateField;
            }
            set
            {
                this.selfPublisherIdentificationIssueDateField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool SelfPublisherIdentificationIssueDateSpecified
        //{
        //    get
        //    {
        //        return this.selfPublisherIdentificationIssueDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.selfPublisherIdentificationIssueDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactName
        {
            get
            {
                return this.contactNameField;
            }
            set
            {
                this.contactNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactPhone
        {
            get
            {
                return this.contactPhoneField;
            }
            set
            {
                this.contactPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactMobilePhone
        {
            get
            {
                return this.contactMobilePhoneField;
            }
            set
            {
                this.contactMobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactEmail
        {
            get
            {
                return this.contactEmailField;
            }
            set
            {
                this.contactEmailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int ContactIdentificationType
        {
            get
            {
                return this.contactIdentificationTypeField;
            }
            set
            {
                this.contactIdentificationTypeField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool ContactIdentificationTypeSpecified
        //{
        //    get
        //    {
        //        return this.contactIdentificationTypeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.contactIdentificationTypeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactIdentificationNumber
        {
            get
            {
                return this.contactIdentificationNumberField;
            }
            set
            {
                this.contactIdentificationNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ContactIdentificationIssuer
        {
            get
            {
                return this.contactIdentificationIssuerField;
            }
            set
            {
                this.contactIdentificationIssuerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime ContactIdentificationIssueDate
        {
            get
            {
                return this.contactIdentificationIssueDateField;
            }
            set
            {
                this.contactIdentificationIssueDateField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool ContactIdentificationIssueDateSpecified
        //{
        //    get
        //    {
        //        return this.contactIdentificationIssueDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.contactIdentificationIssueDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternateContactName
        {
            get
            {
                return this.alternateContactNameField;
            }
            set
            {
                this.alternateContactNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternateContactPhone
        {
            get
            {
                return this.alternateContactPhoneField;
            }
            set
            {
                this.alternateContactPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternateContactMobilePhone
        {
            get
            {
                return this.alternateContactMobilePhoneField;
            }
            set
            {
                this.alternateContactMobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlternateContactEmail
        {
            get
            {
                return this.alternateContactEmailField;
            }
            set
            {
                this.alternateContactEmailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Username
        {
            get
            {
                return this.usernameField;
            }
            set
            {
                this.usernameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PasswordSalt
        {
            get
            {
                return this.passwordSaltField;
            }
            set
            {
                this.passwordSaltField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int IsActivated
        {
            get
            {
                return this.isActivatedField;
            }
            set
            {
                this.isActivatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int VerificationStatus
        {
            get
            {
                return this.verificationStatusField;
            }
            set
            {
                this.verificationStatusField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool VerificationStatusSpecified
        //{
        //    get
        //    {
        //        return this.verificationStatusFieldSpecified;
        //    }
        //    set
        //    {
        //        this.verificationStatusFieldSpecified = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool CertificationNumberSpecified
        //{
        //    get
        //    {
        //        return this.certificationNumberFieldSpecified;
        //    }
        //    set
        //    {
        //        this.certificationNumberFieldSpecified = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool CertificationDateSpecified
        //{
        //    get
        //    {
        //        return this.certificationDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.certificationDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

}
