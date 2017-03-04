using System;
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
    public partial class SelfPublisherInput : BaseUserControl<BaseEntityPortalPage<Publisher>>
    {
        public int CityID
        {
            get
            {
                int? cityID = ViewState["__CityID"] as int?;
                if (cityID.HasValue)
                    return cityID.Value;
                return 0;
            }
            set
            {
                ViewState["__CityID"] = value;
            }
        }

        public int PublisherID { get; set; }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlPublisherCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtPublisherName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                txtPublisherAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
            }
            else
            {
                if (ReadOnly && CityID > 0)
                {

                    int prefectureID;
                    if (int.TryParse(ddlPublisherPrefecture.SelectedValue, out  prefectureID))
                    {
                        ddlPublisherCity.Items.Clear();
                        ddlPublisherCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

                        foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                        {
                            ddlPublisherCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                        }

                        ddlPublisherCity.SelectedValue = CityID.ToString();
                        cddPublisherCity.SelectedValue = CityID.ToString();
                    }
                }
            }
        }

        protected void ddlDOY_Init(object sender, EventArgs e)
        {
            ddlPublisherDOY.Items.Add(new ListItem("-- επιλέξτε Δ.Ο.Υ. --", ""));

            foreach (XElement elem in DOY.DOYsXml.Descendants("DOY"))
            {
                ddlPublisherDOY.Items.Add(new ListItem(elem.Value, elem.Value));
            }
        }

        protected void ddlPublisherPrefecture_Init(object sender, EventArgs e)
        {
            ddlPublisherPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                if (item.ID != HelpDeskConstants.FOREIGN_PREFECTURE)
                {
                    ddlPublisherPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                }
            }
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
            publisher.PublisherType = enPublisherType.SelfPublisher;

            if (publisher.PublisherName != txtPublisherName.Text.ToNull())
            {
                publisher.PublisherName = txtPublisherName.Text.ToNull();
            }

            if (publisher.PublisherAFM != txtPublisherAFM.Text.ToNull())
            {
                publisher.PublisherAFM = txtPublisherAFM.Text.ToNull();
            }

            if (!string.IsNullOrEmpty(ddlPublisherDOY.SelectedValue))
            {
                if (publisherDetails.PublisherDOY != ddlPublisherDOY.SelectedValue)
                {
                    publisherDetails.PublisherDOY = ddlPublisherDOY.SelectedValue;
                }
            }

            int hasLogisticBooks;
            if (int.TryParse(ddlHasLogisticBooks.SelectedItem.Value, out hasLogisticBooks) && hasLogisticBooks >= 0)
            {
                if (hasLogisticBooks == 0)
                {
                    publisherDetails.HasLogisticBooks = false;
                }
                else if (hasLogisticBooks == 1)
                {
                    publisherDetails.HasLogisticBooks = true;
                }
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

            int cityID;
            if (int.TryParse(ddlPublisherCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (publisherDetails.CityID != cityID)
                    publisherDetails.CityID = cityID;
                CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlPublisherPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (publisherDetails.PrefectureID != prefectureID)
                    publisherDetails.PrefectureID = prefectureID;
            }

            IdentificationDetails selfPublisherId = new IdentificationDetails();
            idSelfPublisher.FillIdDetails(selfPublisherId);

            publisherDetails.SelfPublisherIdentificationType = selfPublisherId.IdType;

            if (publisherDetails.SelfPublisherIdentificationNumber != selfPublisherId.IdNumber)
            {
                publisherDetails.SelfPublisherIdentificationNumber = selfPublisherId.IdNumber;
            }

            if (publisherDetails.SelfPublisherIdentificationIssuer != selfPublisherId.IdIssuer)
            {
                publisherDetails.SelfPublisherIdentificationIssuer = selfPublisherId.IdIssuer;
            }

            if (publisherDetails.SelfPublisherIdentificationIssueDate != selfPublisherId.IdIssueDate)
            {
                publisherDetails.SelfPublisherIdentificationIssueDate = selfPublisherId.IdIssueDate;
            }

            // Στοιχεία 1ου Ατόμου Επικοινωνίας
            IdentificationDetails contactId = new IdentificationDetails();
            if (chbxSelfRepresented.Checked)
            {
                if (!publisherDetails.SelfRepresented.HasValue || !publisherDetails.SelfRepresented.Value)
                {
                    publisherDetails.SelfRepresented = true;
                }

                if (publisher.ContactName != txtPublisherName.Text.ToNull())
                {
                    publisher.ContactName = txtPublisherName.Text.ToNull();
                }

                if (publisher.ContactPhone != txtPublisherPhone.Text.ToNull())
                {
                    publisher.ContactPhone = txtPublisherPhone.Text.ToNull();
                }

                if (publisher.ContactMobilePhone != txtPublisherMobilePhone.Text.ToNull())
                {
                    publisher.ContactMobilePhone = txtPublisherMobilePhone.Text.ToNull();
                }

                if (publisher.ContactEmail != txtPublisherEmail.Text.ToNull())
                {
                    publisher.ContactEmail = txtPublisherEmail.Text.ToNull();
                }

                idSelfPublisher.FillIdDetails(contactId);
            }
            else
            {
                if (!publisherDetails.SelfRepresented.HasValue || publisherDetails.SelfRepresented.Value)
                {
                    publisherDetails.SelfRepresented = false;
                }

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

                idContact.FillIdDetails(contactId);
            }

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

            publisher.PublisherDetails = publisherDetails;
        }

        public void SetPublisher(Publisher publisher)
        {
            PublisherID = publisher.ID;
            PublisherDetails publisherDetails = publisher.PublisherDetails;

            // Στοιχεία Εκδοτικού Οίκου            
            txtPublisherName.Text = publisher.PublisherName;
            txtPublisherAFM.Text = publisher.PublisherAFM;
            ddlPublisherDOY.Items.FindByValue(publisherDetails.PublisherDOY).Selected = true;

            if (publisherDetails.HasLogisticBooks.HasValue)
            {
                if (publisherDetails.HasLogisticBooks.Value)
                {
                    ddlHasLogisticBooks.Items.FindByValue("1").Selected = true;
                }
                else
                {
                    ddlHasLogisticBooks.Items.FindByValue("0").Selected = true;
                }
            }

            txtPublisherPhone.Text = publisherDetails.PublisherPhone;
            txtPublisherMobilePhone.Text = publisherDetails.PublisherMobilePhone;
            txtPublisherFax.Text = publisherDetails.PublisherFax;
            txtPublisherEmail.Text = publisherDetails.PublisherEmail;
            txtPublisherURL.Text = publisherDetails.PublisherURL;


            //Self Publisher
            IdentificationDetails idSelfP = new IdentificationDetails();
            idSelfP.IdNumber = publisherDetails.SelfPublisherIdentificationNumber;
            idSelfP.IdType = publisherDetails.SelfPublisherIdentificationType;
            if (publisherDetails.SelfPublisherIdentificationType == enIdentificationType.ID)
            {
                idSelfP.IdIssuer = publisherDetails.SelfPublisherIdentificationIssuer;
                idSelfP.IdIssueDate = publisherDetails.SelfPublisherIdentificationIssueDate;
            }
            idSelfPublisher.SetIdDetails(idSelfP);




            // Στοιχεία Διεύθυνσης Έδρας
            txtPublisherAddress.Text = publisherDetails.PublisherAddress;
            txtPublisherZipCode.Text = publisherDetails.PublisherZipCode;


            var pref = CacheManager.Prefectures.Get((int)publisherDetails.PrefectureReference.GetKey());
            ddlPublisherPrefecture.SelectedValue = pref.ID.ToString();

            ddlPublisherCity.Items.Clear();
            ddlPublisherCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlPublisherCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get((int)publisherDetails.CityReference.GetKey());
            ddlPublisherCity.SelectedValue = city.ID.ToString();
            cddPublisherCity.SelectedValue = city.ID.ToString();
            CityID = city.ID;

            // Στοιχεία 1ου Ατόμου Επικοινωνίας
            chbxSelfRepresented.Checked = (bool)publisherDetails.SelfRepresented;

            IdentificationDetails idCont = new IdentificationDetails();
            idCont.IdNumber = publisherDetails.ContactIdentificationNumber;
            idCont.IdType = publisherDetails.ContactIdentificationType;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                idCont.IdIssuer = publisherDetails.ContactIdentificationIssuer;
                idCont.IdIssueDate = publisherDetails.ContactIdentificationIssueDate;
            }
            idContact.SetIdDetails(idCont);


            txtContactName.Text = publisher.ContactName;
            txtContactPhone.Text = publisher.ContactPhone;
            txtContactMobilePhone.Text = publisher.ContactMobilePhone;
            txtContactEmail.Text = publisher.ContactEmail;
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
                txtPublisherAFM.ValidationGroup = value;
                idSelfPublisher.ValidationGroup = value;
                idContact.ValidationGroup = value;
            }
        }

        public bool IsVerified
        {
            set
            {
                txtPublisherName.Enabled =
                txtPublisherAFM.Enabled =
                ddlPublisherDOY.Enabled =
                chbxSelfRepresented.Enabled =
                txtContactName.Enabled =
                ddlHasLogisticBooks.Enabled = !value;

                idContact.ReadOnly =
                idSelfPublisher.ReadOnly = value;
            }
        }

        public bool IsVerifiedWithoutHasLogisticBooks
        {
            set
            {
                txtPublisherName.Enabled =
                txtPublisherAFM.Enabled =
                ddlPublisherDOY.Enabled =
                chbxSelfRepresented.Enabled =
                txtContactName.Enabled = !value;

                idContact.ReadOnly =
                idSelfPublisher.ReadOnly = value;
            }
        }

        public bool HelpDeskEditMode
        {
            get { return !txtPublisherPhone.Enabled; }
            set
            {
                txtPublisherAFM.Enabled =
                txtPublisherPhone.Enabled =
                txtPublisherMobilePhone.Enabled =
                txtPublisherFax.Enabled =
                txtPublisherEmail.Enabled =
                txtPublisherURL.Enabled =
                txtPublisherAddress.Enabled =
                txtPublisherZipCode.Enabled =
                ddlPublisherPrefecture.Enabled =
                txtContactPhone.Enabled =
                txtContactMobilePhone.Enabled =
                txtContactEmail.Enabled = !value;

                ddlPublisherCity.Attributes["disabled"] = "true";
                cddPublisherCity.Enabled = false;
            }
        }

        public bool ReadOnly
        {
            get { return !cddPublisherCity.Enabled; }
            set
            {
                foreach (WebControl c in Controls.OfType<WebControl>())
                    c.Enabled = !value;
                idContact.ReadOnly = value;
                idSelfPublisher.ReadOnly = value;
                cddPublisherCity.Enabled = !value;
            }
        }

    }
}