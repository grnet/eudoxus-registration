﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

using Eudoxus.Portal.Controls;
using System.Xml.Linq;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.UserControls
{
    public partial class ForeignLegalPersonInput : BaseUserControl<BaseEntityPortalPage<Publisher>>
    {
        public int PublisherID
        {
            get
            {
                int? publisherID = ViewState["__PublisherID"] as int?;
                if (publisherID.HasValue)
                    return publisherID.Value;
                return 0;
            }
            set
            {
                ViewState["__PublisherID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPublisherAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                txtLegalPersonName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
            }

            txtPublisherCity.Text = CacheManager.Cities.Get(HelpDeskConstants.FOREIGN_CITY).Name;
            txtPublisherPrefecture.Text = CacheManager.Prefectures.Get(HelpDeskConstants.FOREIGN_PREFECTURE).Name;
        }

        public void FillPublisher(Publisher publisher)
        {
            PublisherID = publisher.ID;
            PublisherDetails publisherDetails;

            if (publisher.PublisherDetails != null)
            {
                publisherDetails = publisher.PublisherDetails;
            }
            else
            {
                publisherDetails = new PublisherDetails();
            }

            // Στοιχεία Εκδοτικού Οίκου
            publisher.PublisherType = enPublisherType.LegalPerson;

            if (publisher.PublisherName != txtPublisherName.Text.ToNull())
            {
                publisher.PublisherName = txtPublisherName.Text.ToNull();
            }

            if (publisher.PublisherTradeName != txtPublisherTradeName.Text.ToNull())
            {
                publisher.PublisherTradeName = txtPublisherTradeName.Text.ToNull();
            }

            if (publisher.PublisherAFM != txtPublisherAFM.Text.ToNull())
            {
                publisher.PublisherAFM = txtPublisherAFM.Text.ToNull();
            }

            if (publisherDetails.PublisherDOY != txtPublisherDOY.Text.ToNull())
            {
                publisherDetails.PublisherDOY = txtPublisherDOY.Text.ToNull();
            }

            if (publisherDetails.PublisherPhone != txtPublisherPhone.Text.ToNull())
            {
                publisherDetails.PublisherPhone = txtPublisherPhone.Text.ToNull();
            }

            if (publisherDetails.PublisherMobilePhone != txtPublisherMobilePhone.Text.ToNull())
            {
                publisherDetails.PublisherMobilePhone = txtPublisherMobilePhone.Text.ToNull();
            }

            if (publisherDetails.PublisherFax != txtPublisherFax.Text.ToNull())
            {
                publisherDetails.PublisherFax = txtPublisherFax.Text.ToNull();
            }

            if (publisherDetails.PublisherEmail != txtPublisherEmail.Text.ToNull())
            {
                publisherDetails.PublisherEmail = txtPublisherEmail.Text.ToNull();
            }

            if (publisherDetails.PublisherURL != txtPublisherURL.Text.ToNull())
            {
                publisherDetails.PublisherURL = txtPublisherURL.Text.ToNull();
            }

            // Στοιχεία Διεύθυνσης Έδρας
            if (publisherDetails.PublisherAddress != txtPublisherAddress.Text.ToNull())
            {
                publisherDetails.PublisherAddress = txtPublisherAddress.Text.ToNull();
            }

            if (publisherDetails.PublisherZipCode != txtPublisherZipCode.Text.ToNull())
            {
                publisherDetails.PublisherZipCode = txtPublisherZipCode.Text.ToNull();
            }

            if (publisherDetails.CityID != HelpDeskConstants.FOREIGN_CITY)
            {
                publisherDetails.CityID = HelpDeskConstants.FOREIGN_CITY;
            }

            if (publisherDetails.PrefectureID != HelpDeskConstants.FOREIGN_PREFECTURE)
            {
                publisherDetails.PrefectureID = HelpDeskConstants.FOREIGN_PREFECTURE;
            }

            // Στοιχεία Νομίμου Εκπροσώπου
            if (publisherDetails.LegalPersonName != txtLegalPersonName.Text.ToNull())
            {
                publisherDetails.LegalPersonName = txtLegalPersonName.Text.ToNull();
            }

            if (publisherDetails.LegalPersonPhone != txtLegalPersonPhone.Text.ToNull())
            {
                publisherDetails.LegalPersonPhone = txtLegalPersonPhone.Text.ToNull();
            }

            if (publisherDetails.LegalPersonEmail != txtLegalPersonEmail.Text.ToNull())
            {
                publisherDetails.LegalPersonEmail = txtLegalPersonEmail.Text.ToNull();
            }

            IdentificationDetails legalPersonId = new IdentificationDetails();
            idLegalPerson.FillIdDetails(legalPersonId);

            publisherDetails.LegalPersonIdentificationType = legalPersonId.IdType;

            if (publisherDetails.LegalPersonIdentificationNumber != legalPersonId.IdNumber)
            {
                publisherDetails.LegalPersonIdentificationNumber = legalPersonId.IdNumber;
            }

            if (publisherDetails.LegalPersonIdentificationIssuer != legalPersonId.IdIssuer)
            {
                publisherDetails.LegalPersonIdentificationIssuer = legalPersonId.IdIssuer;
            }

            if (publisherDetails.LegalPersonIdentificationIssueDate != legalPersonId.IdIssueDate)
            {
                publisherDetails.LegalPersonIdentificationIssueDate = legalPersonId.IdIssueDate;
            }

            // Στοιχεία 1ου Ατόμου Επικοινωνίας
            if (publisher.ContactName != txtContactName.Text.ToNull())
            {
                publisher.ContactName = txtContactName.Text.ToNull();
            }

            if (publisher.ContactPhone != txtContactPhone.Text.ToNull())
            {
                publisher.ContactPhone = txtContactPhone.Text.ToNull();
            }

            if (publisher.ContactMobilePhone != txtContactMobilePhone.Text.ToNull())
            {
                publisher.ContactMobilePhone = txtContactMobilePhone.Text.ToNull();
            }

            if (publisher.ContactEmail != txtContactEmail.Text.ToNull())
            {
                publisher.ContactEmail = txtContactEmail.Text.ToNull();
            }

            IdentificationDetails contactId = new IdentificationDetails();
            idContact.FillIdDetails(contactId);

            publisherDetails.ContactIdentificationType = contactId.IdType;

            if (publisherDetails.ContactIdentificationNumber != contactId.IdNumber)
            {
                publisherDetails.ContactIdentificationNumber = contactId.IdNumber;
            }

            if (publisherDetails.ContactIdentificationIssuer != contactId.IdIssuer)
            {
                publisherDetails.ContactIdentificationIssuer = contactId.IdIssuer;
            }

            if (publisherDetails.ContactIdentificationIssueDate != contactId.IdIssueDate)
            {
                publisherDetails.ContactIdentificationIssueDate = contactId.IdIssueDate;
            }

            // Στοιχεία 2ου Ατόμου Επικοινωνίας
            if (publisherDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
            {
                publisherDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();
            }

            if (publisherDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
            {
                publisherDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();
            }

            if (publisherDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
            {
                publisherDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();
            }

            if (publisherDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
            {
                publisherDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();
            }

            publisher.PublisherDetails = publisherDetails;
        }

        public void SetPublisher(Publisher publisher)
        {
            PublisherID = publisher.ID;
            PublisherDetails publisherDetails = publisher.PublisherDetails;

            // Στοιχεία Εκδοτικού Οίκου            
            txtPublisherName.Text = publisher.PublisherName;
            txtPublisherTradeName.Text = publisher.PublisherTradeName;
            txtPublisherAFM.Text = publisher.PublisherAFM;
            txtPublisherDOY.Text = publisherDetails.PublisherDOY;
            txtPublisherPhone.Text = publisherDetails.PublisherPhone;
            txtPublisherMobilePhone.Text = publisherDetails.PublisherMobilePhone;
            txtPublisherFax.Text = publisherDetails.PublisherFax;
            txtPublisherEmail.Text = publisherDetails.PublisherEmail;
            txtPublisherURL.Text = publisherDetails.PublisherURL;

            // Στοιχεία Διεύθυνσης Έδρας
            txtPublisherAddress.Text = publisherDetails.PublisherAddress;
            txtPublisherZipCode.Text = publisherDetails.PublisherZipCode;

            // Στοιχεία Νομίμου Εκπροσώπου
            txtLegalPersonName.Text = publisherDetails.LegalPersonName;
            txtLegalPersonPhone.Text = publisherDetails.LegalPersonPhone;
            txtLegalPersonEmail.Text = publisherDetails.LegalPersonEmail;
            IdentificationDetails idLR = new IdentificationDetails();
            idLR.IdNumber = publisherDetails.LegalPersonIdentificationNumber;
            idLR.IdType = publisherDetails.LegalPersonIdentificationType;
            if (publisherDetails.LegalPersonIdentificationType == enIdentificationType.ID)
            {
                idLR.IdIssuer = publisherDetails.LegalPersonIdentificationIssuer;
                idLR.IdIssueDate = publisherDetails.LegalPersonIdentificationIssueDate;
            }
            idLegalPerson.SetIdDetails(idLR);

            // Στοιχεία 1ου Ατόμου Επικοινωνίας
            txtContactName.Text = publisher.ContactName;
            txtContactPhone.Text = publisher.ContactPhone;
            txtContactMobilePhone.Text = publisher.ContactMobilePhone;
            txtContactEmail.Text = publisher.ContactEmail;
            IdentificationDetails idCont = new IdentificationDetails();
            idCont.IdNumber = publisherDetails.ContactIdentificationNumber;
            idCont.IdType = publisherDetails.ContactIdentificationType;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                idCont.IdIssuer = publisherDetails.ContactIdentificationIssuer;
                idCont.IdIssueDate = publisherDetails.ContactIdentificationIssueDate;
            }
            idContact.SetIdDetails(idCont);

            txtAlternateContactName.Text = publisherDetails.AlternateContactName;
            txtAlternateContactPhone.Text = publisherDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = publisherDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = publisherDetails.AlternateContactEmail;
        }

        protected void cvAlternativeGroup_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlternateContactEmail.Text) && string.IsNullOrEmpty(txtAlternateContactMobilePhone.Text)
                && string.IsNullOrEmpty(txtAlternateContactName.Text) && string.IsNullOrEmpty(txtAlternateContactPhone.Text))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = !string.IsNullOrEmpty(e.Value);
            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfvPublisherName.ValidationGroup;
            }
            set
            {
                foreach (var validator in this.RecursiveOfType<BaseValidator>())
                    validator.ValidationGroup = value;

                idLegalPerson.ValidationGroup = value;
                idContact.ValidationGroup = value;
            }
        }

        public bool IsVerified
        {
            set
            {
                txtPublisherName.Enabled =
                txtPublisherAFM.Enabled =
                txtPublisherDOY.Enabled =
                txtLegalPersonName.Enabled =
                txtContactName.Enabled = !value;

                idContact.ReadOnly =
                idLegalPerson.ReadOnly = value;
            }
        }

        public bool HelpDeskEditMode
        {
            set
            {
                txtPublisherTradeName.Enabled =
                txtPublisherPhone.Enabled =
                txtPublisherMobilePhone.Enabled =
                txtPublisherFax.Enabled =
                txtPublisherEmail.Enabled =
                txtPublisherURL.Enabled =
                txtPublisherAddress.Enabled =
                txtPublisherZipCode.Enabled =
                txtLegalPersonPhone.Enabled =
                txtLegalPersonEmail.Enabled =
                txtContactPhone.Enabled =
                txtContactMobilePhone.Enabled =
                txtContactEmail.Enabled =
                txtAlternateContactName.Enabled =
                txtAlternateContactPhone.Enabled =
                txtAlternateContactMobilePhone.Enabled =
                txtAlternateContactEmail.Enabled = !value;
            }
        }

        public bool ReadOnly
        {
            set
            {
                foreach (WebControl c in Controls.OfType<WebControl>())
                    c.Enabled = !value;

                idContact.ReadOnly = value;
                idLegalPerson.ReadOnly = value;
            }
        }
    }
}