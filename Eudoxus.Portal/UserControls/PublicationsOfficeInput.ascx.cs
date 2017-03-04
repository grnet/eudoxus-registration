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
    public partial class PublicationsOfficeInput : BaseEntityUserControl<PublicationsOffice, BaseEntityPortalPage<PublicationsOffice>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlPublicationsOfficeCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        #region [ Databind Methods ]

        public override PublicationsOffice Fill(PublicationsOffice entity)
        {
            if (entity == null)
                entity = new PublicationsOffice();
            PublicationsOfficeDetails pubOfficeDetails;
            if (entity.PublicationsOfficeDetails != null)
            {
                pubOfficeDetails = entity.PublicationsOfficeDetails;
            }
            else
                pubOfficeDetails = new PublicationsOfficeDetails();

            //Στοιχεία Γραμματείας 
            if (pubOfficeDetails.DirectorName != txtDirectorName.Text.ToNull())
                pubOfficeDetails.DirectorName = txtDirectorName.Text.ToNull();

            if (pubOfficeDetails.PublicationsOfficeEmail != txtPublicationsOfficeEmail.Text.ToNull())
                pubOfficeDetails.PublicationsOfficeEmail = txtPublicationsOfficeEmail.Text.ToNull();

            if (pubOfficeDetails.PublicationsOfficePhone != txtPublicationsOfficePhone.Text.ToNull())
                pubOfficeDetails.PublicationsOfficePhone = txtPublicationsOfficePhone.Text.ToNull();

            int academicID = int.Parse(ddlInstitution.SelectedValue);
            if (academicID != entity.InstitutionID)
                entity.InstitutionID = academicID;

            //Στοιχεία Διεύθυνσης 
            if (pubOfficeDetails.PublicationsOfficeAddress != txtPublicationsOfficeAddress.Text.ToNull())
                pubOfficeDetails.PublicationsOfficeAddress = txtPublicationsOfficeAddress.Text.ToNull();

            if (pubOfficeDetails.PublicationsOfficeZipCode != txtPublicationsOfficeZipCode.Text.ToNull())
                pubOfficeDetails.PublicationsOfficeZipCode = txtPublicationsOfficeZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlPublicationsOfficeCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (pubOfficeDetails.CityID != cityID)
                    pubOfficeDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlPublicationsOfficePrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (pubOfficeDetails.PrefectureID != prefectureID)
                    pubOfficeDetails.PrefectureID = prefectureID;
            }

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
            if (pubOfficeDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
                pubOfficeDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();

            if (pubOfficeDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
                pubOfficeDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();

            if (pubOfficeDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
                pubOfficeDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();

            if (pubOfficeDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
                pubOfficeDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();

            entity.PublicationsOfficeDetails = pubOfficeDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.PublicationsOfficeDetails == null)
                return;

            //Στοιχεία Γραμματείας
            txtDirectorName.Text = Entity.PublicationsOfficeDetails.DirectorName;
            txtPublicationsOfficeEmail.Text = Entity.PublicationsOfficeDetails.PublicationsOfficeEmail;
            txtPublicationsOfficePhone.Text = Entity.PublicationsOfficeDetails.PublicationsOfficePhone;
            var pref = CacheManager.Prefectures.Get(Entity.PublicationsOfficeDetails.PrefectureID);
            ddlPublicationsOfficePrefecture.SelectedValue = pref.ID.ToString();

            ddlPublicationsOfficeCity.Items.Clear();
            ddlPublicationsOfficeCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlPublicationsOfficeCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.PublicationsOfficeDetails.CityID);
            ddlPublicationsOfficeCity.SelectedValue = city.ID.ToString();
            cddPublicationsOfficeCity.SelectedValue = city.ID.ToString();
            //Στοιχεία Ιδρύματος
            if (Entity.InstitutionReference.EntityKey != null)
            {
                ddlInstitution.SelectedValue = Entity.InstitutionID.ToString();
            }

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;

            txtContactEmail.Text = Entity.ContactEmail;
            //Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος 
            txtAlternateContactName.Text = Entity.PublicationsOfficeDetails.AlternateContactName;
            txtAlternateContactPhone.Text = Entity.PublicationsOfficeDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = Entity.PublicationsOfficeDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = Entity.PublicationsOfficeDetails.AlternateContactEmail;

            txtPublicationsOfficeZipCode.Text = Entity.PublicationsOfficeDetails.PublicationsOfficeZipCode.ToString();
            txtPublicationsOfficeAddress.Text = Entity.PublicationsOfficeDetails.PublicationsOfficeAddress;
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

        protected void ddlPublicationsOfficePrefecture_Init(object sender, EventArgs e)
        {
            ddlPublicationsOfficePrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlPublicationsOfficePrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
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

        protected void cvCheckAcademic_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Entity != null)
                args.IsValid = !new PublicationsOfficeRepository().IsInstitutionVerified(Entity.ID, int.Parse(ddlInstitution.SelectedValue));
            else
                args.IsValid = !new PublicationsOfficeRepository().IsInstitutionVerified(int.Parse(ddlInstitution.SelectedValue));
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
                    txtPublicationsOfficeAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlPublicationsOfficeCity.Enabled && Entity != null && Entity.PublicationsOfficeDetails != null && Entity.PublicationsOfficeDetails.CityReference.EntityKey != null && Entity.PublicationsOfficeDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.PublicationsOfficeDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.PublicationsOfficeDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlPublicationsOfficeCity.Items.Clear();
                            ddlPublicationsOfficeCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlPublicationsOfficeCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlPublicationsOfficeCity.SelectedValue = cityID.ToString();
                            cddPublicationsOfficeCity.SelectedValue = cityID.ToString();
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
                cvCheckAcademic.Enabled =
                txtContactName.Enabled =
                rfvContactName.Enabled = true;
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            bool isEnabled = !isReadOnly;
            foreach (WebControl c in Controls.OfType<WebControl>())
                c.Enabled = isEnabled;

            cddPublicationsOfficeCity.Enabled = isEnabled;
            cvCheckAcademic.Enabled = true;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}