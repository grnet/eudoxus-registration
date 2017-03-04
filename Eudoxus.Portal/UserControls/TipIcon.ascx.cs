using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Eudoxus.Portal.UserControls
{
    [DefaultProperty("Text")]
    [ParseChildren(true, "Text")]
    public partial class TipIcon : System.Web.UI.UserControl, ITextControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public string Text
        {
            get { return litTip.Text; }
            set { litTip.Text = value; }
        }


    }
}