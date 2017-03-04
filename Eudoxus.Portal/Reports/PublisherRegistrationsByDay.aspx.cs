using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Eudoxus.Portal.Reports
{
    public partial class PublisherRegistrationsByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvPublishers.SettingsPager.PageSize = int.MaxValue;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvePublshers.FileName = String.Format("OrdersByDay_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gvePublshers.WriteXlsToResponse(true);
        }
    }
}
