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
    public partial class SecretaryInput : BaseEntityUserControl<Secretary, BaseEntityPortalPage<Secretary>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<City> cities = CacheManager.Cities.GetItems();
            foreach (City city in cities)
            {
                Page.ClientScript.RegisterForEventValidation(ddlSecretaryCity.UniqueID, city.ID.ToString());
            }
            base.Render(writer);
        }

        protected void ddlSemesters_Init(object sender, EventArgs e)
        {
            ddlSemesters.Items.Add(new ListItem("-- επιλέξτε αριθμό εξαμήνων --", ""));

            for (int i = 1; i <= 12; i++)
            {
                ddlSemesters.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        #region [ Databind Methods ]

        public override Secretary Fill(Secretary entity)
        {
            if (entity == null)
                entity = new Secretary();
            SecretaryDetails secDetails;
            if (entity.SecretaryDetails != null)
            {
                secDetails = entity.SecretaryDetails;
            }
            else
                secDetails = new SecretaryDetails();

            //Στοιχεία Γραμματείας 
            if (secDetails.RepresentativeName != txtPresidentName.Text.ToNull())
                secDetails.RepresentativeName = txtPresidentName.Text.ToNull();

            if (secDetails.SecretaryEmail != txtSecretaryEmail.Text.ToNull())
                secDetails.SecretaryEmail = txtSecretaryEmail.Text.ToNull();

            if (secDetails.SecretaryPhone != txtSecretaryPhone.Text.ToNull())
                secDetails.SecretaryPhone = txtSecretaryPhone.Text.ToNull();

            int academicID = int.Parse(hfSchoolCode.Value);
            if (academicID != entity.AcademicID)
                entity.AcademicID = academicID;

            int semesters;
            if (int.TryParse(ddlSemesters.SelectedItem.Value, out semesters) && semesters >= 0)
            {
                if (secDetails.Semesters != semesters)
                    secDetails.Semesters = semesters;
            }

            secDetails.RepresentativeType = (enRepresentativeType)int.Parse(rbtlRepresentativeType.SelectedItem.Value);

            //Στοιχεία Διεύθυνσης 
            if (secDetails.SecretaryAddress != txtSecretaryAddress.Text.ToNull())
                secDetails.SecretaryAddress = txtSecretaryAddress.Text.ToNull();

            if (secDetails.SecretaryZipCode != txtSecretaryZipCode.Text.ToNull())
                secDetails.SecretaryZipCode = txtSecretaryZipCode.Text.ToNull();

            int cityID;
            if (int.TryParse(ddlSecretaryCity.SelectedValue, out cityID) && cityID > 0)
            {
                if (secDetails.CityID != cityID)
                    secDetails.CityID = cityID;
            }

            int prefectureID;
            if (int.TryParse(ddlSecretaryPrefecture.SelectedValue, out prefectureID) && prefectureID > 0)
            {
                if (secDetails.PrefectureID != prefectureID)
                    secDetails.PrefectureID = prefectureID;
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
            if (secDetails.AlternateContactName != txtAlternateContactName.Text.ToNull())
                secDetails.AlternateContactName = txtAlternateContactName.Text.ToNull();

            if (secDetails.AlternateContactPhone != txtAlternateContactPhone.Text.ToNull())
                secDetails.AlternateContactPhone = txtAlternateContactPhone.Text.ToNull();

            if (secDetails.AlternateContactMobilePhone != txtAlternateContactMobilePhone.Text.ToNull())
                secDetails.AlternateContactMobilePhone = txtAlternateContactMobilePhone.Text.ToNull();

            if (secDetails.AlternateContactEmail != txtAlternateContactEmail.Text.ToNull())
                secDetails.AlternateContactEmail = txtAlternateContactEmail.Text.ToNull();

            entity.SecretaryDetails = secDetails;

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null || Entity.SecretaryDetails == null)
                return;

            //Στοιχεία Γραμματείας
            ddlSemesters.SelectedValue = Entity.SecretaryDetails.Semesters.ToString();
            txtPresidentName.Text = Entity.SecretaryDetails.RepresentativeName;
            txtSecretaryEmail.Text = Entity.SecretaryDetails.SecretaryEmail;
            txtSecretaryPhone.Text = Entity.SecretaryDetails.SecretaryPhone;
            var pref = CacheManager.Prefectures.Get(Entity.SecretaryDetails.PrefectureID);
            ddlSecretaryPrefecture.SelectedValue = pref.ID.ToString();

            ddlSecretaryCity.Items.Clear();
            ddlSecretaryCity.Items.Add(new ListItem("-- επιλέξτε νομό --", "-1"));

            foreach (var item in pref.Cities)
            {
                ddlSecretaryCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }

            var city = CacheManager.Cities.Get(Entity.SecretaryDetails.CityID);
            ddlSecretaryCity.SelectedValue = city.ID.ToString();
            cddSecretaryCity.SelectedValue = city.ID.ToString();
            //Στοιχεία Ιδρύματος
            if (Entity.AcademicReference.EntityKey != null)
            {
                var academic = CacheManager.Academics.Get(Entity.AcademicID);
                txtInstitutionName.Text = academic.Institution;
                txtSchoolName.Text = academic.School;
                txtDepartmentName.Text = academic.Department;
                hfSchoolCode.Value = academic.ID.ToString();
            }

            var rblItem = rbtlRepresentativeType.Items.FindByValue(Entity.SecretaryDetails.RepresentativeType.ToString("D"));
            if (rblItem != null)
                rbtlRepresentativeType.SelectedValue = rblItem.Value;

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactMobilePhone.Text = Entity.ContactMobilePhone;

            txtContactEmail.Text = Entity.ContactEmail;
            //Στοιχεία Αναπληρωτή Υπευθύνου για το Εύδοξος 
            txtAlternateContactName.Text = Entity.SecretaryDetails.AlternateContactName;
            txtAlternateContactPhone.Text = Entity.SecretaryDetails.AlternateContactPhone;
            txtAlternateContactMobilePhone.Text = Entity.SecretaryDetails.AlternateContactMobilePhone;
            txtAlternateContactEmail.Text = Entity.SecretaryDetails.AlternateContactEmail;

            txtSecretaryZipCode.Text = Entity.SecretaryDetails.SecretaryZipCode.ToString();
            txtSecretaryAddress.Text = Entity.SecretaryDetails.SecretaryAddress;
        }

        #endregion

        #region [ Control Inits ]

        protected void ddlSecretaryPrefecture_Init(object sender, EventArgs e)
        {
            ddlSecretaryPrefecture.Items.Add(new ListItem("-- επιλέξτε νομό --", ""));

            foreach (var item in CacheManager.Prefectures.GetItems())
            {
                ddlSecretaryPrefecture.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        protected void rbtlRepresentativeType_Init(object sender, EventArgs e)
        {
            foreach (enRepresentativeType item in Enum.GetValues(typeof(enRepresentativeType)))
            {
                if (item == enRepresentativeType.None)
                    continue;
                rbtlRepresentativeType.Items.Add(new ListItem(item.GetLabel(), item.ToString("D")));
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
                args.IsValid = !new SecretaryRepository().IsAcademicVerified(Entity.ID, int.Parse(hfSchoolCode.Value));
            else
                args.IsValid = !new SecretaryRepository().IsAcademicVerified(int.Parse(hfSchoolCode.Value));
        }

        #endregion

        #region [ Overrides ]

        protected override void OnLoad(EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            base.OnLoad(e);
        }

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
                    txtPresidentName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtSecretaryAddress.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                    txtAlternateContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";
                }
                else
                {
                    if (!ddlSecretaryCity.Enabled && Entity != null && Entity.SecretaryDetails != null && Entity.SecretaryDetails.CityReference.EntityKey != null && Entity.SecretaryDetails.PrefectureReference.EntityKey != null)
                    {

                        int prefectureID = (int)Entity.SecretaryDetails.PrefectureReference.GetKey();
                        int cityID = (int)Entity.SecretaryDetails.CityReference.GetKey();
                        if (prefectureID > 0)
                        {
                            ddlSecretaryCity.Items.Clear();
                            ddlSecretaryCity.Items.Add(new ListItem("-- επιλέξτε πόλη --", "-1"));

                            foreach (var item in CacheManager.Prefectures.Get(prefectureID).Cities)
                            {
                                ddlSecretaryCity.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }

                            ddlSecretaryCity.SelectedValue = cityID.ToString();
                            cddSecretaryCity.SelectedValue = cityID.ToString();
                        }
                    }
                }

            }

            if (phSelectAcademic.Visible)
            {
                txtInstitutionName.Attributes.Add("onclick", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
                txtInstitutionName.Attributes.Add("onfocus", "popUp.show('/Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            }

            base.OnPreRender(e);
        }

        #endregion

        #region [ UI Region ]

        private void UpdateUIUser(bool isVerified)
        {
            if (isVerified)
            {
                txtInstitutionName.Enabled =
                phSelectAcademic.Visible =
                txtSchoolName.Enabled =
                txtDepartmentName.Enabled =
                txtContactName.Enabled =
                ddlSecretaryCity.Enabled =
                cddSecretaryCity.Enabled =
                rfvSecretaryCity.Enabled =
                ddlSecretaryPrefecture.Enabled =
                rfvSecretaryPrefecture.Enabled =
                ddlSemesters.Enabled = false;
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
            trRepresentative.Visible = false;

            //Τα θέτουμε όλα ReadOnly
            SetReadOnly(true);
            if (!isVerified)
            { /*Όλα non-editable άρα δεν χρειάζεται να αλλάξουμε κάτι*/ }
            else
            {
                //Όλα non-editable εκτός από
                txtInstitutionName.Enabled =
                phSelectAcademic.Visible =
                txtContactName.Enabled =
                txtSchoolName.Enabled =
                ddlSemesters.Enabled =
                dxpcPopup.Enabled =
                txtDepartmentName.Enabled =
                rfvInstitutionName.Enabled =
                txtInstitutionName.Enabled =
                cvCheckAcademic.Enabled =
                rfvContactName.Enabled =
                ddlSecretaryCity.Enabled =
                cddSecretaryCity.Enabled =
                rfvSecretaryCity.Enabled =
                ddlSecretaryPrefecture.Enabled =
                rfvSecretaryPrefecture.Enabled = true;
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
                trRepresentative.Visible = false;

            bool isEnabled = !isReadOnly;
            foreach (WebControl c in Controls.OfType<WebControl>())
                c.Enabled = isEnabled;
            phSelectAcademic.Visible = isEnabled;
            cddSecretaryCity.Enabled = isEnabled;
            cvCheckAcademic.Enabled = true;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}