using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.eService;
using Eudoxus.Utils;
using System.Threading;
using System.Web.SessionState;
using System.Web.Hosting;

namespace Eudoxus.Portal.Admin
{
    public partial class SendPublishersForPayment : System.Web.UI.Page
    {
        protected void btnSendPublishers_Click(object sender, EventArgs e)
        {
            int take = -1;
            int.TryParse(txtPublisherCount.Text, out take);
            Application["Message"] = "Batch process started!";
            ThreadPool.QueueUserWorkItem(p =>
               {
                   SendUpdates(take, Application);
               });
        }

        protected string ShowMessage()
        {
            string message = Application["Message"] as string;
            if (!string.IsNullOrEmpty(message))
                Application.Remove("Message");
            return message;
        }

        static void SendUpdates(int take, HttpApplicationState state)
        {
            try
            {
                HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
                IEnumerable<int> ids = null;

                if (take > 0)
                {
                    ids = ctx.vPublisher.Where(x => x.PublisherType != (int)enPublisherType.EbookPublisher && x.VerificationStatus == (int)enVerificationStatus.Verified).OrderByDescending(x => x.UpdatedAt).Select(x => x.ID).Take(take);
                }
                else
                {
                    ids = ctx.vPublisher.Where(x => x.PublisherType != (int)enPublisherType.EbookPublisher && x.VerificationStatus == (int)enVerificationStatus.Verified).OrderByDescending(x => x.UpdatedAt).Select(x => x.ID).ToList();
                }

                state["Message"] = string.Format("Finished! {0} users were sent.", ids.Count());

                if (Config.UsePaymentEService)
                {
                    foreach (int id in ids)
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
                            LogHelper.LogError<SendPublishersForPayment>(ex, string.Format("Update failed for publisher with id {0}", id));
                            continue;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                state["Message"] = string.Format("Error: {0}", ex.Message);
                LogHelper.LogError<SendUpdatesToService>(ex);
            }
        }
    }
}