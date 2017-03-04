using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class ViewRegisteredPublicationsOffices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsPublicationsOffices_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<PublicationsOfficeDetailsView> criteria = new Criteria<PublicationsOfficeDetailsView>();

            e.InputParameters["criteria"] = criteria;
        }
    }
}