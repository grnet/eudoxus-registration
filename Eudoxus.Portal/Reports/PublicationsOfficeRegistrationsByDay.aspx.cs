using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eudoxus.Portal.Reports
{
    public partial class PublicationsOfficeRegistrationsByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvPublicationsOffices.SettingsPager.PageSize = int.MaxValue;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvePublicationsOffices.FileName = String.Format("PublicationsOfficesByDay_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gvePublicationsOffices.WriteXlsToResponse(true);
        }
    }
}