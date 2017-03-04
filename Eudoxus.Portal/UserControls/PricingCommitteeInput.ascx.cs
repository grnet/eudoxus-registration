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
    public partial class PricingCommitteeInput : BaseEntityUserControl<PricingCommittee, BaseEntityPortalPage<PricingCommittee>>
    {
        #region [ Databind Methods ]

        public override PricingCommittee Fill(PricingCommittee entity)
        {
            if (entity == null)
                entity = new PricingCommittee();

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            if (entity.ContactName != txtContactName.Text.ToNull())
                entity.ContactName = txtContactName.Text.ToNull();

            if (entity.ContactPhone != txtContactPhone.Text.ToNull())
                entity.ContactPhone = txtContactPhone.Text.ToNull();

            if (entity.ContactEmail != txtContactEmail.Text.ToNull())
                entity.ContactEmail = txtContactEmail.Text.ToNull();

            if (entity.PricingCommitteeType != txtPricingCommitteeType.Text.ToNull())
                entity.PricingCommitteeType = txtPricingCommitteeType.Text.ToNull();

            return entity;
        }

        public override void Bind()
        {
            if (Entity == null)
                return;

            //Στοιχεία Υπεύθυνου για το Εύδοξος
            txtContactName.Text = Entity.ContactName;
            txtContactPhone.Text = Entity.ContactPhone;
            txtContactEmail.Text = Entity.ContactEmail;
            txtPricingCommitteeType.Text = Entity.PricingCommitteeType;
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
                    txtContactName.Attributes["onkeyup"] = "Imis.Lib.ToUpperOnlyLetters(this)";                    
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
                txtContactName.Enabled = false;
                txtPricingCommitteeType.Enabled = false;
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
                txtContactName.Enabled =
                rfvContactName.Enabled =
                txtPricingCommitteeType.Enabled = true;
            }
        }

        private void SetReadOnly(bool isReadOnly)
        {
            bool isEnabled = !isReadOnly;
            foreach (WebControl c in Controls.OfType<WebControl>())
                c.Enabled = isEnabled;
        }

        /// <summary>
        /// Χρησιμοποιείται μόνο όταν θέλουμε να θέσουμε όλα τα πεδία στη φόρμα ReadOnly ανεξάρτητα από το ποιος τα βλέπει και για ποιο λόγο
        /// </summary>
        public bool? ReadOnly { get; set; }

        #endregion
    }
}