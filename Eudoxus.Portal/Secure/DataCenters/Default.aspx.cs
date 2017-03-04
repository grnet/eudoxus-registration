﻿using System;
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
using Eudoxus.BusinessModel;
using System.Web.Profile;

using Eudoxus.Portal.Controls;

namespace Eudoxus.Portal.Secure.DataCenters
{
    public partial class Default : BaseEntityPortalPage<DataCenter>
    {
        protected override void Fill()
        {
            Entity = new DataCenterRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
            {
                phVerified.Visible = false;
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.Verified)
            {
                phNotVerified.Visible = false;
                phCannotBeVerified.Visible = false;
            }
            else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
            {
                phVerified.Visible = false;
                phNotVerified.Visible = false;
            }
        }
    }
}
