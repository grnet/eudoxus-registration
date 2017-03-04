using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Helpdesk.UserControls
{
    public partial class AcademicDetailInput : BaseUserControl<BaseEntityPortalPage<AcademicDetail>>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillAcademicDetail(AcademicDetail academicDetail)
        {
            if (academicDetail.Address != txtAddress.Text.ToNull())
            {
                academicDetail.Address = txtAddress.Text.ToNull();
            }

            if (academicDetail.Phone != txtPhone.Text.ToNull())
            {
                academicDetail.Phone = txtPhone.Text.ToNull();
            }

            if (academicDetail.Fax != txtFax.Text.ToNull())
            {
                academicDetail.Fax = txtFax.Text.ToNull();
            }

            if (academicDetail.Email != txtEmail.Text.ToNull())
            {
                academicDetail.Email = txtEmail.Text.ToNull();
            }

            if (academicDetail.Semesters != txtSemesters.Text.ToNull())
            {
                academicDetail.Semesters = txtSemesters.Text.ToNull();
            }

            if (academicDetail.Prefix != txtPrefix.Text.ToNull())
            {
                academicDetail.Prefix = txtPrefix.Text.ToNull();
            }

            if (academicDetail.IsNotified != chbxIsNotified.Checked)
            {
                academicDetail.IsNotified = chbxIsNotified.Checked;
            }
        }

        public void SetAcademicDetail(AcademicDetail academicDetail)
        {
            lblInstitution.Text = academicDetail.Institution;
            lblSchool.Text = academicDetail.School;
            lblDepartment.Text = academicDetail.Department;

            txtAddress.Text = academicDetail.Address;
            txtPhone.Text = academicDetail.Phone;
            txtFax.Text = academicDetail.Fax;
            txtEmail.Text = academicDetail.Email;
            txtSemesters.Text = academicDetail.Semesters;
            txtPrefix.Text = academicDetail.Prefix;
            chbxIsNotified.Checked = academicDetail.IsNotified;
        }
    }
}