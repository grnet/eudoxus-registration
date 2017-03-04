﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eudoxus.Portal.Reports
{
    public partial class SecretaryRegistrationsByDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvSecretaries.SettingsPager.PageSize = int.MaxValue;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gveSecretaries.FileName = String.Format("OrdersByDay_{0}", DateTime.Now.ToString("yyyyMMdd"));
            gveSecretaries.WriteXlsToResponse(true);
        }
    }
}