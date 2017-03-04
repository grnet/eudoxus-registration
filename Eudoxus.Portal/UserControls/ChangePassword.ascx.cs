using System;
using System.ComponentModel;

namespace Eudoxus.Portal.UserControls.Generic
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        [DefaultValue(true)]
        public bool RequestOldPassword
        {
            get { return trOldPassword.Visible; }
            set { trOldPassword.Visible = value; }
        }

        public string NewPassword
        {
            get { return txtPassword1.Text; }
        }

        public string OldPassword
        {
            get { return txtOldPassword.Text; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ValidationGroup
        {
            get
            {
                return rfvPassword1.ValidationGroup;
            }
            set
            {
                rfvPassword1.ValidationGroup = value;
                rfvPassword2.ValidationGroup = value;
                revPassword.ValidationGroup = value;
                rfvOldPassword.ValidationGroup = value;
                cvPassword2.ValidationGroup = value;
            }
        }

        public void ClearInput()
        {
            txtOldPassword.Text = "";
            txtPassword1.Text = "";
            txtPassword2.Text = "";
        }
    }
}