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
    public partial class EbookPublisherInput : BaseUserControl<BaseEntityPortalPage<Publisher>>
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

            // Στοιχεία Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων
            publisher.PublisherType = enPublisherType.EbookPublisher;

            if (publisher.PublisherName != txtPublisherName.Text.ToNull())
            {
                publisher.PublisherName =
                    publisher.ContactName = txtPublisherName.Text.ToNull();
            }

            if (publisherDetails.PublisherPhone != txtPublisherPhone.Text.ToNull())
            {
                publisherDetails.PublisherPhone =
                    publisher.ContactPhone = txtPublisherPhone.Text.ToNull();
            }

            if (publisherDetails.PublisherMobilePhone != txtPublisherMobilePhone.Text.ToNull())
            {
                publisherDetails.PublisherMobilePhone =
                    publisher.ContactMobilePhone = txtPublisherMobilePhone.Text.ToNull();
            }

            if (publisherDetails.PublisherEmail != txtPublisherEmail.Text.ToNull())
            {
                publisherDetails.PublisherEmail =
                    publisher.ContactEmail = txtPublisherEmail.Text.ToNull();
            }

            if (publisherDetails.PublisherURL != txtPublisherURL.Text.ToNull())
            {
                publisherDetails.PublisherURL = txtPublisherURL.Text.ToNull();
            }

            IdentificationDetails contactId = new IdentificationDetails();
            idEbookPublisher.FillIdDetails(contactId);

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

            publisher.PublisherDetails = publisherDetails;
        }

        public void SetPublisher(Publisher publisher)
        {
            PublisherID = publisher.ID;
            PublisherDetails publisherDetails = publisher.PublisherDetails;

            // Στοιχεία Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων           
            txtPublisherName.Text = publisher.PublisherName;
            txtPublisherPhone.Text = publisherDetails.PublisherPhone;
            txtPublisherMobilePhone.Text = publisherDetails.PublisherMobilePhone;
            txtPublisherEmail.Text = publisherDetails.PublisherEmail;
            txtPublisherURL.Text = publisherDetails.PublisherURL;

            //Identification Details
            IdentificationDetails idEbookP = new IdentificationDetails();

            idEbookP.IdNumber = publisherDetails.ContactIdentificationNumber;
            idEbookP.IdType = publisherDetails.ContactIdentificationType;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                idEbookP.IdIssuer = publisherDetails.ContactIdentificationIssuer;
                idEbookP.IdIssueDate = publisherDetails.ContactIdentificationIssueDate;
            }

            idEbookPublisher.SetIdDetails(idEbookP);

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
                idEbookPublisher.ValidationGroup = value;
            }
        }

        public bool IsVerified
        {
            set
            {
                txtPublisherName.Enabled = !value;

                idEbookPublisher.ReadOnly = value;
            }
        }

        public bool HelpDeskEditMode
        {
            get { return !txtPublisherPhone.Enabled; }
            set
            {

                txtPublisherPhone.Enabled =
                txtPublisherMobilePhone.Enabled =
                txtPublisherEmail.Enabled =
                txtPublisherURL.Enabled =
                txtPublisherAddress.Enabled =
                txtPublisherZipCode.Enabled =
                ddlPublisherPrefecture.Enabled = !value;

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

                idEbookPublisher.ReadOnly = value;
                cddPublisherCity.Enabled = !value;
            }
        }
    }
}