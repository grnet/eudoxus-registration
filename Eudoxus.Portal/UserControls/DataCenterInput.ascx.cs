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
    public partial class DataCenterInput : BaseEntityUserControl<DataCenter, BaseEntityPortalPage<DataCenter>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlDataCenterCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        #region [ Databind Methods ]

        public override DataCenter Fill(DataCenter entity)
        {
            if (entity == null)
                entity = new DataCenter();
            DataCenterDetails dataCenterDetails;
            if (entity.DataCenterDetails != null)
            {
                dataCenterDetails = entity.DataCenterDetails;
            }
            else
                dataCenterDetails = new DataCenterDetails();

            //Στοιχεία Γραμματείας 
            if (dataCenterDetails.DirectorName != txtDirectorName.Text.ToNull())
                dataCenterDetails.DirectorName = txtDirectorName.Text.ToNull();

            if (dataCenterDetails.DataCenterEmail != txtDataCenterEmail.Text.ToNull())
                dataCenterDetails.DataCenterEmail = txtDataCenterEmail.Text.ToNull();

            if (dataCenterDetails.DataCenterPhone != txtDataCenterPhone.Text.ToNull())
                dataCenterDetails.DataCenterPhone = txtDataCenterPhone.Text.ToNull();

            int academicID = int.Parse(ddlInstitution.SelectedValue);
            if (academicID != entity.InstitutionID)
                entity.InstitutionID = academicID;

            //Στοιχεία Διεύθυνσης 
            if (dataCenterDetails.DataCenterAddress != txtDataCenterAddress.Text.ToNull())
                dataCenterDetails.DataCenterAddress = txtDataCenterAddress.Text.ToNull();

            if (dataCenterDetails.DataCenterZipCode != txtDataCenterZipCode.Text.ToNull())
                dataCenterDetails.DataCenterZipCode = txtDataCenterZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlDataCenterCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (dataCenterDetails.CityID != cityID)
                    dataCenterDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlDataCenterPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (dataCenterDetails.PrefectureID != prefectureID)
                    dataCenterDetails.PrefectureID = prefectureID;
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
            if (dataCenterDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
                dataCenterDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();

            if (dataCenterDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
                dataCenterDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();

            if (dataCenterDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
                dataCenterDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();

            if (dataCenterDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
                dataCenterDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();

            entity.DataCenterDetails = dataCenterDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.DataCenterDetails == null)
                return;

            //Στοιχεία Γραμματείας
            txtDirectorName.Text = Entity.DataCenterDetails.DirectorName;
            txtDataCenterEmail.Text = Entity.DataCenterDetails.DataCenterEmail;
            txtDataCenterPhone.Text = Entity.DataCenterDetails.DataCenterPhone;
            var pref = CacheManager.Prefectures.Get(Entity.DataCenterDetails.PrefectureID);
            ddlDataCenterPrefecture.SelectedValue = pref.ID.ToString();

            ddlDataCenterCity.Items.Clear();
            ddlDataCenterCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlDataCenterCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.DataCenterDetails.CityID);
            ddlDataCenterCity.SelectedValue = city.ID.ToString();
            cddDataCenterCity.SelectedValue = city.ID.ToString();
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
            txtAlternateContactName.Text = Entity.DataCenterDetails.AlternateContactName;
            txtAlternateContactPhone.Text = Entity.DataCenterDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = Entity.DataCenterDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = Entity.DataCenterDetails.AlternateContactEmail;

            txtDataCenterZipCode.Text = Entity.DataCenterDetails.DataCenterZipCode.ToString();
            txtDataCenterAddress.Text = Entity.DataCenterDetails.DataCenterAddress;
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

        protected void ddlDataCenterPrefecture_Init(object sender, EventArgs e)
        {
            ddlDataCenterPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlDataCenterPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
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
                args.IsValid = !new DataCenterRepository().IsInstitutionVerified(Entity.ID, int.Parse(ddlInstitution.SelectedValue));
            else
                args.IsValid = !new DataCenterRepository().IsInstitutionVerified(int.Parse(ddlInstitution.SelectedValue));
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
                    txtDataCenterAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlDataCenterCity.Enabled && Entity != null && Entity.DataCenterDetails != null && Entity.DataCenterDetails.CityReference.EntityKey != null && Entity.DataCenterDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.DataCenterDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.DataCenterDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlDataCenterCity.Items.Clear();
                            ddlDataCenterCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlDataCenterCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlDataCenterCity.SelectedValue = cityID.ToString();
                            cddDataCenterCity.SelectedValue = cityID.ToString();
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

            cddDataCenterCity.Enabled = isEnabled;
            cvCheckAcademic.Enabled = true;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}