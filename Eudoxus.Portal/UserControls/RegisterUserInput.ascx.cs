using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Imis.Domain;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.UserControls
{
    public partial class RegisterUserInput : System.Web.UI.UserControl
    {
        public enum enEditMode
        {
            NewUser = 0,
            ExistingUser = 1,
            MasterUser = 2
        }

        public int EditMode
        {
            get { return Convert.ToInt32(ViewState["_EditMode"]); }
            set
            {
                ViewState["_EditMode"] = value;

                if ((EditMode == (int)enEditMode.ExistingUser) || (EditMode == (int)enEditMode.MasterUser))
                {
                    trPasswordInfo.Visible =
                    trPassword1.Visible =
                    rfvPassword1.Visible =
                    rfvPassword2.Visible =
                    trPassword2.Visible =
                    trEmailDescription.Visible =
                    txtUsername.Enabled = false;
                }
            }
        }

        public bool EmailInfoHidden
        {
            get
            {
                return !trEmailDescription.Visible;
            }
            set
            {
                trEmailDescription.Visible = !value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return !txtEmail.Enabled;
            }
            set
            {
                txtEmail.Enabled = !value;
            }
        }

        private MembershipUser _currentUser = null;
        protected MembershipUser CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                try
                {
                    if (EditMode == (int)enEditMode.ExistingUser)
                    {
                        _currentUser = Membership.GetUser(Page.User.Identity.Name.ToLower().Trim());
                    }
                    else if (EditMode == (int)enEditMode.MasterUser)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["username"]))
                        {
                            _currentUser = Membership.GetUser(Request.QueryString["username"]);
                        }
                    }
                }
                catch (FormatException)
                {
                }

                return _currentUser;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Attributes["onblur"] = "RemoveTags(this)";
            txtPassword1.Attributes["onblur"] = "RemoveTags(this)";
            txtPassword2.Attributes["onblur"] = "RemoveTags(this)";
            txtEmail.Attributes["onblur"] = "RemoveTags(this)";
        }

        public void SetUser(MembershipUser u)
        {
            txtUsername.Text = u.UserName;
            txtEmail.Text = u.Email;
            _CreatedUser = u;
        }

        public string CreateUser()
        {
            try
            {
                MembershipCreateStatus status;
                MembershipUser mu = Membership.CreateUser(txtUsername.Text, txtPassword1.Text, txtEmail.Text, null, null, false, out status);

                if (mu == null)
                    throw new MembershipCreateUserException(status);
                _CreatedUser = mu;
                return mu.UserName;
            }
            catch (MembershipCreateUserException ex)
            {
                throw;
            }
        }

        public bool UpdateUser(string username)
        {
            MembershipUser mu = Membership.GetUser(username);
            if (mu.Email != txtEmail.Text && !string.IsNullOrEmpty(txtEmail.Text))
            {
                mu.Email = txtEmail.Text;
                Membership.UpdateUser(mu);

                using (IUnitOfWork uow = UnitOfWorkFactory.Create())
                {
                    Reporter reporter = new ReporterRepository(uow).FindByUsername(username);
                    enReporterType reporterType = reporter.ReporterType;

                    switch (reporterType)
                    {
                        case enReporterType.Publisher:
                            (reporter as Publisher).Email = txtEmail.Text;
                            break;
                        case enReporterType.Secretary:
                            (reporter as Secretary).Email = txtEmail.Text;
                            break;
                        case enReporterType.DistributionPoint:
                            (reporter as DistributionPoint).Email = txtEmail.Text;
                            break;
                        case enReporterType.PublicationsOffice:
                            (reporter as PublicationsOffice).Email = txtEmail.Text;
                            break;
                        case enReporterType.DataCenter:
                            (reporter as DataCenter).Email = txtEmail.Text;
                            break;
                        case enReporterType.Library:
                            (reporter as Library).Email = txtEmail.Text;
                            break;
                        case enReporterType.BookSupplier:
                            (reporter as BookSupplier).Email = txtEmail.Text;
                            break;
                        case enReporterType.PricingCommittee:
                            (reporter as PricingCommittee).Email = txtEmail.Text;
                            break;
                        case enReporterType.MinistryPayments:
                            (reporter as MinistryPaymentsUser).Email = txtEmail.Text;
                            break;
                    }

                    uow.Commit();
                }

                return true;
            }
            return false;
        }

        protected void cvUsername_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (EditMode == (int)enEditMode.NewUser)
            {
                e.IsValid = Membership.GetUser(e.Value) == null;
            }
            else if (EditMode == (int)enEditMode.ExistingUser)
            {
                e.IsValid = Membership.GetUser(e.Value).UserName == e.Value;
            }
            else if (EditMode == (int)enEditMode.MasterUser)
            {
                e.IsValid = true;
            }
        }

        protected void cvEmail_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (EditMode == (int)enEditMode.NewUser)
            {
                e.IsValid = string.IsNullOrEmpty(Membership.GetUserNameByEmail(txtEmail.Text));
            }
            else if (EditMode == (int)enEditMode.ExistingUser)
            {
                e.IsValid = CurrentUser.Email == txtEmail.Text || string.IsNullOrEmpty(Membership.GetUserNameByEmail(txtEmail.Text));
            }
            else if (EditMode == (int)enEditMode.MasterUser)
            {
                e.IsValid = CurrentUser.Email == txtEmail.Text || string.IsNullOrEmpty(Membership.GetUserNameByEmail(txtEmail.Text));
            }
        }

        public string ValidationGroup
        {
            get
            {
                return rfvUsername.ValidationGroup;
            }
            set
            {
                rfvUsername.ValidationGroup = value;
                revUsername.ValidationGroup = value;
                cvUsername.ValidationGroup = value;
                rfvPassword1.ValidationGroup = value;
                revPassword1.ValidationGroup = value;
                rfvPassword2.ValidationGroup = value;
                cvPassword2.ValidationGroup = value;
                rfvEmail.ValidationGroup = value;
                revEmail.ValidationGroup = value;
                cvEmail.ValidationGroup = value;
            }
        }

        public string Email { get { return txtEmail.Text; } }
        public string Username { get { return txtUsername.Text; } }


        MembershipUser _CreatedUser = null;
        public string ProviderUserKey
        {
            get
            {
                if (_CreatedUser == null)
                    throw new InvalidOperationException("No MembershipUser was found. Please check CreateUser() or SetUser().");
                return _CreatedUser.ProviderUserKey.ToString();
            }
        }
    }
}