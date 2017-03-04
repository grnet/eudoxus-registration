using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.ComponentModel;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Imis.Domain;
using Eudoxus.eService;

namespace Eudoxus.Portal.Common
{
    public partial class ChangeDefaultPassword : BaseEntityPortalPage<Reporter>
    {
        protected override void Fill()
        {
            Entity = new ReporterRepository(UnitOfWork).FindByUsername(Page.User.Identity.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var user = Membership.GetUser(Page.User.Identity.Name);

            if (user.ChangePassword(txtOldPassword.Text.ToNull(), txtPassword1.Text.ToNull()))
            {
                Entity.MustChangePassword = false;
                UnitOfWork.Commit();

                var reporter = new ReporterRepository(UnitOfWork).FindByUsername(user.UserName);

                if (reporter != null)
                {
                    if (reporter is Publisher)
                    {
                        ServiceWorker.SendPublisherUpdate(reporter.ID);
                    }
                    else if (reporter is Secretary)
                    {
                        ServiceWorker.SendSecretaryUpdate(reporter.ID);
                    }
                    else if (reporter is PublicationsOffice)
                    {
                        ServiceWorker.SendPublicationsOfficeUpdate(reporter.ID);
                    }
                    else if (reporter is DataCenter)
                    {
                        ServiceWorker.SendDataCenterUpdate(reporter.ID);
                    }
                    else if (reporter is DistributionPoint)
                    {
                        ServiceWorker.SendDistributionPointUpdate(reporter.ID);
                    }
                    else if (reporter is Library)
                    {
                        ServiceWorker.SendLibraryUpdate(reporter.ID);
                    }
                    else if (reporter is BookSupplier)
                    {
                        ServiceWorker.SendBookSupplierUpdate(reporter.ID);
                    }
                    else if (reporter is PricingCommittee)
                    {
                        ServiceWorker.SendPricingCommitteeUpdate(reporter.ID);
                    }
                    else if (reporter is MinistryPaymentsUser)
                    {
                        var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == reporter.ID).ToJsonDto();
                        EudoxusOsyClient.Update(ministryPaymentsUserDto);
                    }
                }

                mvChangePassword.SetActiveView(vPasswordChanged);
            }
            else
            {
                lblInfo.Text = "Ο παλιός κωδικός πρόσβασης δεν είναι σωστός. Βεβαιωθείτε ότι εισάγετε σωστά τον κωδικό που σας ήρθε με το e-mail Υπενθύμισης Κωδικού Πρόσβασης.";
            }
        }
    }
}
