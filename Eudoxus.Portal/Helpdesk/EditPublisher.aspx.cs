using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Eudoxus.eService;
using Eudoxus.Utils;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class EditPublisher : BaseEntityPortalPage<Publisher>
    {
        protected override void Fill()
        {
            Entity = new PublisherRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.PublisherDetailsReference.Load();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucPublisherInput.SetPublisher(Entity);
            }
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Χρειάζεται για το Validation σχετικά με το αν υπάρχει άλλος πιστοποιημένος            
            ucPublisherInput.PublisherType = Entity.PublisherType;
            ucPublisherInput.PublisherID = Entity.ID;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string oldPublisherAFM = Entity.PublisherAFM;

            ucPublisherInput.FillPublisher(Entity);
            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);
            string newPublisherAFM = Entity.PublisherAFM;

            //if (oldPublisherAFM != newPublisherAFM)
            //{
            //    IList<Publisher> notVerifiedPublishers = new PublisherRepository(UnitOfWork).FindPublishersByVerificationStatus(newPublisherAFM, enVerificationStatus.NotVerified);
            //    IList<Publisher> cannotBeVerifiedPublishers = new PublisherRepository(UnitOfWork).FindPublishersByVerificationStatus(oldPublisherAFM, enVerificationStatus.CannotBeVerified);

            //    foreach (Publisher publisher in notVerifiedPublishers)
            //    {
            //        if (publisher.ID != Entity.ID)
            //        {
            //            publisher.VerificationStatus = enVerificationStatus.CannotBeVerified;
            //            updatedIDs.Add(publisher.ID);
            //        }
            //    }

            //    foreach (Publisher publisher in cannotBeVerifiedPublishers)
            //    {
            //        if (publisher.ID != Entity.ID)
            //        {
            //            publisher.VerificationStatus = enVerificationStatus.NotVerified;
            //            updatedIDs.Add(publisher.ID);
            //        }
            //    }
            //}

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
            {
                ServiceWorker.SendPublisherUpdate(id);
            }

            if (Config.UsePaymentEService)
            {
                foreach (int id in updatedIDs.Distinct())
                {
                    try
                    {
                        var publisherDto = new HelpDeskViewsEntities().vPublisher.Single(x => x.ID == id).ToJsonDto();

                        if (publisherDto.PublisherType != (int)enPublisherType.EbookPublisher)
                        {
                            //ServiceClientForPayment.Update(publisherDto);
                            EudoxusOsyClient.Update(publisherDto);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(ex, this, string.Format("Update failed for publisher with id {0}", id));
                        continue;
                    }
                }
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
