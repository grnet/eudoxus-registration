using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Queue;
using DevExpress.Web.ASPxGridView;
using System.IO;

namespace Eudoxus.Portal.Admin
{
    public partial class ViewQueueEntries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvQueueEntries_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "QueueData")
            {
                var x = gvQueueEntries.GetRow(e.VisibleIndex) as QueueEntry;
                var qd = ((IQueueWorker)QueueWorker.Current).GetQueueData<List<GenericQueueData>>(x);
                var gv = new ASPxGridView();
                gv.AutoGenerateColumns = true;
                gv.DataSource = qd;
                gv.SettingsPager.PageSize = 20;
                var sw = new StringWriter();
                var html = new HtmlTextWriter(sw);
                gv.DataBind();
                gv.RenderControl(html);
                e.Cell.Controls.Add(new LiteralControl(sw.ToString()));
            }
        }
    }
}