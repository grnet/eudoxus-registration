using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Imis.Domain;
using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class EditAcademicDetails : BaseEntityPortalPage<AcademicDetail>
    {
        private AcademicDetail _currentAcademicDetail = null;
        protected AcademicDetail CurrentAcademicDetail
        {
            get
            {
                if (_currentAcademicDetail != null)
                    return _currentAcademicDetail;

                try
                {
                    _currentAcademicDetail = new AcademicDetailRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["aID"]));
                }
                catch (FormatException)
                {
                }

                return _currentAcademicDetail;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack)
            {
                ucAcademicDetailInput.SetAcademicDetail(CurrentAcademicDetail);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ucAcademicDetailInput.FillAcademicDetail(CurrentAcademicDetail);

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
