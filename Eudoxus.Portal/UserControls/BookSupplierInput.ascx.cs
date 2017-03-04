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
    public partial class BookSupplierInput : BaseEntityUserControl<BookSupplier, BaseEntityPortalPage<BookSupplier>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlBookSupplierCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        #region [ Databind Methods ]

        public override BookSupplier Fill(BookSupplier entity)
        {
            if (entity == null)
                entity = new BookSupplier();
            BookSupplierDetails bookSupplierDetails;
            if (entity.BookSupplierDetails != null)
            {
                bookSupplierDetails = entity.BookSupplierDetails;
            }
            else
                bookSupplierDetails = new BookSupplierDetails();

            //Στοιχεία Υπευθύνου Παραγγελίας Βιβλίων 
            if (bookSupplierDetails.CertifierName != txtCertifierName.Text.ToNull())
                bookSupplierDetails.CertifierName = txtCertifierName.Text.ToNull();

            int academicID = int.Parse(ddlInstitution.SelectedValue);
            if (academicID != entity.InstitutionID)
                entity.InstitutionID = academicID;

            bookSupplierDetails.CertifierType = (enCertifierType)int.Parse(rbtlCertifierType.SelectedItem.Value);
            if (bookSupplierDetails.CertifierName != txtCertifierName.Text.ToNull())
                bookSupplierDetails.CertifierName = txtCertifierName.Text.ToNull();

            //Στοιχεία Διεύθυνσης 
            if (bookSupplierDetails.BookSupplierAddress != txtBookSupplierAddress.Text.ToNull())
                bookSupplierDetails.BookSupplierAddress = txtBookSupplierAddress.Text.ToNull();

            if (bookSupplierDetails.BookSupplierZipCode != txtBookSupplierZipCode.Text.ToNull())
                bookSupplierDetails.BookSupplierZipCode = txtBookSupplierZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlBookSupplierCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (bookSupplierDetails.CityID != cityID)
                    bookSupplierDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlBookSupplierPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (bookSupplierDetails.PrefectureID != prefectureID)
                    bookSupplierDetails.PrefectureID = prefectureID;
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
            if (bookSupplierDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
                bookSupplierDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();

            if (bookSupplierDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
                bookSupplierDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();

            if (bookSupplierDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
                bookSupplierDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();

            if (bookSupplierDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
                bookSupplierDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();

            entity.BookSupplierDetails = bookSupplierDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.BookSupplierDetails == null)
                return;

            //Στοιχεία Υπευθύνου Παραγγελίας Βιβλίων
            var rblItem = rbtlCertifierType.Items.FindByValue(Entity.BookSupplierDetails.CertifierType.ToString("D"));
            if (rblItem != null)
                rbtlCertifierType.SelectedValue = rblItem.Value;
            txtCertifierName.Text = Entity.BookSupplierDetails.CertifierName;


            var pref = CacheManager.Prefectures.Get(Entity.BookSupplierDetails.PrefectureID);
            ddlBookSupplierPrefecture.SelectedValue = pref.ID.ToString();

            ddlBookSupplierCity.Items.Clear();
            ddlBookSupplierCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlBookSupplierCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.BookSupplierDetails.CityID);
            ddlBookSupplierCity.SelectedValue = city.ID.ToString();
            cddBookSupplierCity.SelectedValue = city.ID.ToString();
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
            txtAlternateContactName.Text = Entity.BookSupplierDetails.AlternateContactName;
            txtAlternateContactPhone.Text = Entity.BookSupplierDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = Entity.BookSupplierDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = Entity.BookSupplierDetails.AlternateContactEmail;

            txtBookSupplierZipCode.Text = Entity.BookSupplierDetails.BookSupplierZipCode.ToString();
            txtBookSupplierAddress.Text = Entity.BookSupplierDetails.BookSupplierAddress;
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

        protected void rbtlCertifierType_Init(object sender, EventArgs e)
        {
            foreach (enCertifierType item in Enum.GetValues(typeof(enCertifierType)))
            {
                if (item == enCertifierType.None)
                    continue;
                rbtlCertifierType.Items.Add(new ListItem(item.GetLabel(), item.ToString("D")));
            }
        }

        protected void ddlBookSupplierPrefecture_Init(object sender, EventArgs e)
        {
            ddlBookSupplierPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlBookSupplierPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
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
                args.IsValid = !new BookSupplierRepository().IsInstitutionVerified(Entity.ID, int.Parse(ddlInstitution.SelectedValue));
            else
                args.IsValid = !new BookSupplierRepository().IsInstitutionVerified(int.Parse(ddlInstitution.SelectedValue));
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
                    txtCertifierName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtBookSupplierAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlBookSupplierCity.Enabled && Entity != null && Entity.BookSupplierDetails != null && Entity.BookSupplierDetails.CityReference.EntityKey != null && Entity.BookSupplierDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.BookSupplierDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.BookSupplierDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlBookSupplierCity.Items.Clear();
                            ddlBookSupplierCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlBookSupplierCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlBookSupplierCity.SelectedValue = cityID.ToString();
                            cddBookSupplierCity.SelectedValue = cityID.ToString();
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

            cddBookSupplierCity.Enabled = isEnabled;
            cvCheckAcademic.Enabled = true;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}