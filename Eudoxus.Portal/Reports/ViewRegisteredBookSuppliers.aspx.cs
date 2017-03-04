using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class ViewRegisteredBookSuppliers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsBookSuppliers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<BookSupplierDetailsView> criteria = new Criteria<BookSupplierDetailsView>();

            e.InputParameters["criteria"] = criteria;
        }
    }
}