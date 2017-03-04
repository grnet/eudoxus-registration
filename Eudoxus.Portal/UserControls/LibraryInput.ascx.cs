using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.UserControls
{
    public partial class LibraryInput : BaseEntityUserControl<Library, BaseEntityPortalPage<Library>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlLibraryCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        #region [ Databind Methods ]

        public override Library Fill(Library entity)
        {
            if (entity == null)
                entity = new Library();
            LibraryDetails libraryDetails;
            if (entity.LibraryDetails != null)
            {
                libraryDetails = entity.LibraryDetails;
            }
            else
                libraryDetails = new LibraryDetails();

            //Στοιχεία Βιβλιοθήκης 
            int academicID = int.Parse(ddlInstitution.SelectedValue);
            if (academicID != entity.InstitutionID)
                entity.InstitutionID = academicID;

            if (entity.LibraryName != txtLibraryName.Text.ToNull())
                entity.LibraryName = txtLibraryName.Text.ToNull();

            if (libraryDetails.LibraryOpeningHours != txtLibraryOpeningHours.Text.ToNull())
                libraryDetails.LibraryOpeningHours = txtLibraryOpeningHours.Text.ToNull();

            if (libraryDetails.DirectorName != txtDirectorName.Text.ToNull())
                libraryDetails.DirectorName = txtDirectorName.Text.ToNull();

            if (libraryDetails.LibraryEmail != txtLibraryEmail.Text.ToNull())
                libraryDetails.LibraryEmail = txtLibraryEmail.Text.ToNull();

            if (libraryDetails.LibraryPhone != txtLibraryPhone.Text.ToNull())
                libraryDetails.LibraryPhone = txtLibraryPhone.Text.ToNull();

            if (libraryDetails.LibraryURL != txtLibraryURL.Text.ToNull())
                libraryDetails.LibraryURL = txtLibraryURL.Text.ToNull();


            //Στοιχεία Διεύθυνσης 
            if (libraryDetails.LibraryAddress != txtLibraryAddress.Text.ToNull())
                libraryDetails.LibraryAddress = txtLibraryAddress.Text.ToNull();

            if (libraryDetails.LibraryZipCode != txtLibraryZipCode.Text.ToNull())
                libraryDetails.LibraryZipCode = txtLibraryZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlLibraryCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (libraryDetails.CityID != cityID)
                    libraryDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlLibraryPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (libraryDetails.PrefectureID != prefectureID)
                    libraryDetails.PrefectureID = prefectureID;
            }

            if (libraryDetails.LibraryLocationURL != txtLibraryLocationURL.Text.ToNull())
                libraryDetails.LibraryLocationURL = txtLibraryLocationURL.Text.ToNull();

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            if (entity.ContactName != txtContactName.Text.ToNull())
                entity.ContactName = txtContactName.Text.ToNull();

            if (entity.ContactPhone != txtContactPhone.Text.ToNull())
                entity.ContactPhone = txtContactPhone.Text.ToNull();

            if (entity.ContactMobilePhone != txtContactMobilePhone.Text.ToNull())
                entity.ContactMobilePhone = txtContactMobilePhone.Text.ToNull();

            if (entity.ContactEmail != txtContactEmail.Text.ToNull())
                entity.ContactEmail = txtContactEmail.Text.ToNull();

            //Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος 
            if (libraryDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
                libraryDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();

            if (libraryDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
                libraryDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();

            if (libraryDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
                libraryDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();

            if (libraryDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
                libraryDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();

            entity.LibraryDetails = libraryDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.LibraryDetails == null)
                return;

            //Στοιχεία Βιβλιοθήκης
            if (Entity.InstitutionReference.EntityKey != null)
            {
                ddlInstitution.SelectedValue = Entity.InstitutionID.ToString();
            }
            txtLibraryName.Text = Entity.LibraryName;
            txtLibraryOpeningHours.Text = Entity.LibraryDetails.LibraryOpeningHours;
            txtLibraryPhone.Text = Entity.LibraryDetails.LibraryPhone;
            txtLibraryEmail.Text = Entity.LibraryDetails.LibraryEmail;
            txtLibraryURL.Text = Entity.LibraryDetails.LibraryURL;
            txtDirectorName.Text = Entity.LibraryDetails.DirectorName;

            //Στοιχεία Διεύθυνσης Βιβλιοθήκης
            txtLibraryAddress.Text = Entity.LibraryDetails.LibraryAddress;
            txtLibraryZipCode.Text = Entity.LibraryDetails.LibraryZipCode.ToString();

            var pref = CacheManager.Prefectures.Get(Entity.LibraryDetails.PrefectureID);
            ddlLibraryPrefecture.SelectedValue = pref.ID.ToString();

            ddlLibraryCity.Items.Clear();
            ddlLibraryCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlLibraryCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.LibraryDetails.CityID);
            ddlLibraryCity.SelectedValue = city.ID.ToString();
            cddLibraryCity.SelectedValue = city.ID.ToString();

            txtLibraryLocationURL.Text = Entity.LibraryDetails.LibraryLocationURL;

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;

            txtContactEmail.Text = Entity.ContactEmail;
            //Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος 
            txtAlternateContactName.Text = Entity.LibraryDetails.AlternateContactName;
            txtAlternateContactPhone.Text = Entity.LibraryDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = Entity.LibraryDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = Entity.LibraryDetails.AlternateContactEmail;
        }

        #endregion

        #region [ Control Inits ]

        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- επιλέξτε ίδρυμα --", ""));

            foreach (var item in CacheManager.Institutions.GetItems())
            {
                ddlInstitution.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        protected void ddlLibraryPrefecture_Init(object sender, EventArgs e)
        {
            ddlLibraryPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlLibraryPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        #endregion

        #region [ Validation ]

        public string ValidationGroup
        {
            get { return rfvContactEmail.ValidationGroup; }
            set
            {
                foreach (var validator in this.RecursiveOfType<BaseValidator>())
                    validator.ValidationGroup = value;
            }
        }

        protected void cvAlternativeGroup_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlternateContactEmail.Text) //&& string.IsNullOrEmpty(txtAlternateContactMobilePhone.Text)
                && string.IsNullOrEmpty(txtAlternateContactName.Text) && string.IsNullOrEmpty(txtAlternateContactPhone.Text))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = !string.IsNullOrEmpty(e.Value);
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnPreRender(EventArgs e)
        {
            bool isHelpdesk = Roles.IsUserInRole(BusinessModel.RoleNames.Helpdesk) || Roles.IsUserInRole(BusinessModel.RoleNames.SuperHelpdesk);
            if (Entity != null)
            {
                if (ReadOnly.HasValue)
                    SetReadOnly(ReadOnly.Value);
                else
                {
                    bool isVerified = Entity.VerificationStatus == enVerificationStatus.Verified;
                    if (!isHelpdesk)
                    {
                        UpdateUIUser(isVerified);
                    }
                    else
                    {
                        UpdateUIHelpdesk(isVerified);
                    }
                }

                if (!Page.IsPostBack)
                {
                    txtDirectorName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtLibraryAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlLibraryCity.Enabled && Entity != null && Entity.LibraryDetails != null && Entity.LibraryDetails.CityReference.EntityKey != null && Entity.LibraryDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.LibraryDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.LibraryDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlLibraryCity.Items.Clear();
                            ddlLibraryCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlLibraryCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlLibraryCity.SelectedValue = cityID.ToString();
                            cddLibraryCity.SelectedValue = cityID.ToString();
                        }
                    }
                }
            }

            base.OnPreRender(e);
        }

        #endregion

        #region [ UI Region ]

        private void UpdateUIUser(bool isVerified)
        {
            if (isVerified)
            {
                ddlInstitution.Enabled =
                txtLibraryName.Enabled =
                txtContactName.Enabled = false;
            }
            else
            {
                SetReadOnly(false);
            }
        }

        /// <summary>
        /// O χρήστης είναι Helpdesk και πρέπει να ενημερωθεί το UI κατάλληλα.
        /// </summary>
        private void UpdateUIHelpdesk(bool isVerified)
        {
            //Τα θέτουμε όλα ReadOnly
            SetReadOnly(true);
            if (!isVerified)
            { /*Όλα non-editable άρα δεν χρειάζεται να αλλάξουμε κάτι*/ }
            else
            {
                //Όλα non-editable εκτός από
                ddlInstitution.Enabled =
                rfvInstitution.Enabled =
                txtLibraryName.Enabled =
                txtContactName.Enabled =
                rfvContactName.Enabled = true;
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            bool isEnabled = !isReadOnly;
            foreach (WebControl c in Controls.OfType<WebControl>())
                c.Enabled = isEnabled;

            cddLibraryCity.Enabled = isEnabled;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}