using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eudoxus.Portal.Reports
{
    public partial class LibraryRegistrationsByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvLibraries.SettingsPager.PageSize = int.MaxValue;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gveLibraries.FileName = String.Format("LibrariesByDay_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveLibraries.WriteXlsToResponse(true);
        }
    }
}