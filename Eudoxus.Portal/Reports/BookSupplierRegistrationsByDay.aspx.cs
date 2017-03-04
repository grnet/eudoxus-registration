using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eudoxus.Portal.Reports
{
    public partial class BookSupplierRegistrationsByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvBookSuppliers.SettingsPager.PageSize = int.MaxValue;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gveBookSuppliers.FileName = String.Format("BookSuppliersByDay_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveBookSuppliers.WriteXlsToResponse(true);
        }
    }
}