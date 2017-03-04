using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Eudoxus.eService
{
    [System.SerializableAttribute()]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "PricingCommittee")]
    public partial class PricingCommitteeDto
    {
        private string pricingCommitteeTypeField;

        private string contactNameField;

        private string contactPhoneField;

        private string contactEmailField;

        private int pricingCommitteeAuthorizationField;

        private string usernameField;

        private string passwordField;

        private string passwordSaltField;

        private string emailField;

        private bool isActivatedField;

        private int verificationStatusField;

        private long idField;

        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PricingCommitteeType
        {
            get
            {
                return this.pricingCommitteeTypeField;
            }
            set
            {
                this.pricingCommitteeTypeField = value;
            }
        }

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
        public int PricingCommitteeAuthorization
        {
            get
            {
                return this.pricingCommitteeAuthorizationField;
            }
            set
            {
                this.pricingCommitteeAuthorizationField = value;
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
        public bool IsActivated
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
