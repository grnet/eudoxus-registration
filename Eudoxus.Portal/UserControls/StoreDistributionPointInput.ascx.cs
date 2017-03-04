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
    public partial class StoreDistributionPointInput : BaseEntityUserControl<DistributionPoint, BaseEntityPortalPage<DistributionPoint>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlDistributionPointCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        #region [ Databind Methods ]

        public override DistributionPoint Fill(DistributionPoint entity)
        {
            if (entity == null)
                entity = new DistributionPoint();
            DistributionPointDetails distributionPointDetails;
            if (entity.DistributionPointDetails != null)
            {
                distributionPointDetails = entity.DistributionPointDetails;
            }
            else
                distributionPointDetails = new DistributionPointDetails();

            //Στοιχεία Σημείου Διανομής 
            if (entity.DistributionPointName != txtDistributionPointName.Text.ToNull())
                entity.DistributionPointName = txtDistributionPointName.Text.ToNull();

            if (distributionPointDetails.DistributionPointOpeningHours != txtDistributionPointOpeningHours.Text.ToNull())
                distributionPointDetails.DistributionPointOpeningHours = txtDistributionPointOpeningHours.Text.ToNull();

            if (distributionPointDetails.DistributionPointPhone != txtDistributionPointPhone.Text.ToNull())
                distributionPointDetails.DistributionPointPhone = txtDistributionPointPhone.Text.ToNull();

            if (distributionPointDetails.DistributionPointMobilePhone != txtDistributionPointMobilePhone.Text.ToNull())
                distributionPointDetails.DistributionPointMobilePhone = txtDistributionPointMobilePhone.Text.ToNull();

            if (distributionPointDetails.DistributionPointFax != txtDistributionPointFax.Text.ToNull())
                distributionPointDetails.DistributionPointFax = txtDistributionPointFax.Text.ToNull();

            if (distributionPointDetails.DistributionPointEmail != txtDistributionPointEmail.Text.ToNull())
                distributionPointDetails.DistributionPointEmail = txtDistributionPointEmail.Text.ToNull();

            if (distributionPointDetails.DistributionPointURL != txtDistributionPointURL.Text.ToNull())
                distributionPointDetails.DistributionPointURL = txtDistributionPointURL.Text.ToNull();

            //Στοιχεία Διεύθυνσης 
            if (distributionPointDetails.DistributionPointAddress != txtDistributionPointAddress.Text.ToNull())
                distributionPointDetails.DistributionPointAddress = txtDistributionPointAddress.Text.ToNull();

            if (distributionPointDetails.DistributionPointZipCode != txtDistributionPointZipCode.Text.ToNull())
                distributionPointDetails.DistributionPointZipCode = txtDistributionPointZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlDistributionPointCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (distributionPointDetails.CityID != cityID)
                    distributionPointDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlDistributionPointPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (distributionPointDetails.PrefectureID != prefectureID)
                    distributionPointDetails.PrefectureID = prefectureID;
            }

            if (distributionPointDetails.DistributionPointLocationURL != txtDistributionPointLocationURL.Text.ToNull())
                distributionPointDetails.DistributionPointLocationURL = txtDistributionPointLocationURL.Text.ToNull();

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            if (entity.ContactName != txtContactName.Text.ToNull())
                entity.ContactName = txtContactName.Text.ToNull();

            if (entity.ContactPhone != txtContactPhone.Text.ToNull())
                entity.ContactPhone = txtContactPhone.Text.ToNull();

            if (entity.ContactMobilePhone != txtContactMobilePhone.Text.ToNull())
                entity.ContactMobilePhone = txtContactMobilePhone.Text.ToNull();

            if (entity.ContactEmail != txtContactEmail.Text.ToNull())
                entity.ContactEmail = txtContactEmail.Text.ToNull();

            entity.DistributionPointDetails = distributionPointDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.DistributionPointDetails == null)
                return;

            //Στοιχεία Σημείου Διανομής
            txtDistributionPointName.Text = Entity.DistributionPointName;
            txtDistributionPointOpeningHours.Text = Entity.DistributionPointDetails.DistributionPointOpeningHours;
            txtDistributionPointPhone.Text = Entity.DistributionPointDetails.DistributionPointPhone;
            txtDistributionPointMobilePhone.Text = Entity.DistributionPointDetails.DistributionPointMobilePhone;
            txtDistributionPointFax.Text = Entity.DistributionPointDetails.DistributionPointFax;
            txtDistributionPointEmail.Text = Entity.DistributionPointDetails.DistributionPointEmail;
            txtDistributionPointURL.Text = Entity.DistributionPointDetails.DistributionPointURL;

            //Στοιχεία Διεύθυνσης
            var pref = CacheManager.Prefectures.Get(Entity.DistributionPointDetails.PrefectureID);
            ddlDistributionPointPrefecture.SelectedValue = pref.ID.ToString();

            txtDistributionPointAddress.Text = Entity.DistributionPointDetails.DistributionPointAddress;
            txtDistributionPointZipCode.Text = Entity.DistributionPointDetails.DistributionPointZipCode.ToString();

            ddlDistributionPointCity.Items.Clear();
            ddlDistributionPointCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlDistributionPointCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.DistributionPointDetails.CityID);
            ddlDistributionPointCity.SelectedValue = city.ID.ToString();
            cddDistributionPointCity.SelectedValue = city.ID.ToString();

            txtDistributionPointLocationURL.Text = Entity.DistributionPointDetails.DistributionPointLocationURL;

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;
            txtContactEmail.Text = Entity.ContactEmail;
        }

        #endregion

        #region [ Control Inits ]

        protected void ddlDistributionPointPrefecture_Init(object sender, EventArgs e)
        {
            ddlDistributionPointPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlDistributionPointPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
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
                    txtDistributionPointName.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtDistributionPointAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlDistributionPointCity.Enabled && Entity != null && Entity.DistributionPointDetails != null && Entity.DistributionPointDetails.CityReference.EntityKey != null && Entity.DistributionPointDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.DistributionPointDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.DistributionPointDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlDistributionPointCity.Items.Clear();
                            ddlDistributionPointCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlDistributionPointCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlDistributionPointCity.SelectedValue = cityID.ToString();
                            cddDistributionPointCity.SelectedValue = cityID.ToString();
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
                txtDistributionPointName.Enabled =
                txtContactName.Enabled =
                ddlDistributionPointPrefecture.Enabled =
                rfvDistributionPointPrefecture.Enabled = false;
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
                txtDistributionPointName.Enabled =
                rfvDistributionPointName.Enabled =
                txtContactName.Enabled =
                rfvContactName.Enabled =
                ddlDistributionPointPrefecture.Enabled =
                rfvDistributionPointPrefecture.Enabled =
                ddlDistributionPointCity.Enabled =
                cddDistributionPointCity.Enabled =
                rfvDistributionPointCity.Enabled = true;
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            bool isEnabled = !isReadOnly;
            foreach (WebControl c in Controls.OfType<WebControl>())
                c.Enabled = isEnabled;

            cddDistributionPointCity.Enabled = isEnabled;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}