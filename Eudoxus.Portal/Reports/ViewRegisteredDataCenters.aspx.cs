using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class ViewRegisteredDataCenters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsDataCenters_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<DataCenterDetailsView> criteria = new Criteria<DataCenterDetailsView>();

            e.InputParameters["criteria"] = criteria;
        }
    }
}