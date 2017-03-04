using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Eudoxus.Utils;
using System.Threading;

namespace Eudoxus.Portal.Admin
{
    public partial class SendUpdatesToService : System.Web.UI.Page
    {
        protected void SendPublisherUpdates(object sender, EventArgs e)
        {
            int take = -1;
            int.TryParse(txtTake.Text, out take);
            HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
            IEnumerable<int> ids = null;
            if (take > 0)
            {
                ids = ctx.vPublisher.Select(x => x.ID).Take(take);
            }
            else
            {
                ids = ctx.vPublisher.Select(x => x.ID).ToList();
            }

            foreach (int id in ids)
            {
                ServiceWorker.SendPublisherUpdate(ctx.vPublisher.Single(x => x.ID == id).ID);
            }
        }

        protected void SendSecretaryUpdates(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(p =>
               {
                   try
                   {
                       int take = -1;
                       int.TryParse(txtTake.Text, out take);
                       HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
                       IEnumerable<int> ids = null;
                       if (take > 0)
                       {
                           ids = ctx.vSecretary.Select(x => x.ID).Take(take);
                       }
                       else
                       {
                           ids = ctx.vSecretary.Select(x => x.ID).ToList();
                       }

                       foreach (int id in ids)
                       {
                           ServiceWorker.SendSecretaryUpdate(ctx.vSecretary.Single(x => x.ID == id).ID, false);
                       }
                   }
                   catch (Exception ex)
                   {
                       LogHelper.LogError<SendUpdatesToService>(ex);
                   }
               });
        }

        protected void SendPublicationsOfficeUpdates(object sender, EventArgs e)
        {
            int take = -1;
            int.TryParse(txtTake.Text, out take);
            HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
            IEnumerable<int> ids = null;
            if (take > 0)
            {
                ids = ctx.vPublicationsOffice.Select(x => x.ID).Take(take);
            }
            else
            {
                ids = ctx.vPublicationsOffice.Select(x => x.ID).ToList();
            }

            foreach (int id in ids)
            {
                ServiceWorker.SendPublicationsOfficeUpdate(ctx.vPublicationsOffice.Single(x => x.ID == id).ID);
            }
        }

        protected void SendDataCenterUpdates(object sender, EventArgs e)
        {
            int take = -1;
            int.TryParse(txtTake.Text, out take);
            HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
            IEnumerable<int> ids = null;
            if (take > 0)
            {
                ids = ctx.vDataCenter.Select(x => x.ID).Take(take);
            }
            else
            {
                ids = ctx.vDataCenter.Select(x => x.ID).ToList();
            }

            foreach (int id in ids)
            {
                ServiceWorker.SendDataCenterUpdate(ctx.vDataCenter.Single(x => x.ID == id).ID);
            }
        }

        protected void SendDistributionPointUpdates(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(p =>
            {
                try
                {
                    int take = -1;
                    int.TryParse(txtTake.Text, out take);
                    HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
                    IEnumerable<int> ids = null;
                    if (take > 0)
                    {
                        ids = ctx.vDistributionPoint.Select(x => x.ID).Take(take);
                    }
                    else
                    {
                        ids = ctx.vDistributionPoint.Select(x => x.ID).ToList();
                    }
                    foreach (int id in ids)
                    {
                        ServiceWorker.SendDistributionPointUpdate(ctx.vDistributionPoint.Single(x => x.ID == id).ID, false);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError<SendUpdatesToService>(ex);
                }
            }
            );
        }

        protected void SendLibraryUpdates(object sender, EventArgs e)
        {
            int take = -1;
            int.TryParse(txtTake.Text, out take);
            HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
            IEnumerable<int> ids = null;
            if (take > 0)
            {
                ids = ctx.vLibrary.Select(x => x.ID).Take(take);
            }
            else
            {
                ids = ctx.vLibrary.Select(x => x.ID).ToList();
            }

            foreach (int id in ids)
            {
                ServiceWorker.SendLibraryUpdate(ctx.vLibrary.Single(x => x.ID == id).ID);
            }
        }

        protected void SendPricingCommitteeUpdates(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(p =>
            {
                try
                {
                    int take = -1;
                    int.TryParse(txtTake.Text, out take);
                    HelpDeskViewsEntities ctx = new HelpDeskViewsEntities();
                    IEnumerable<int> ids = null;
                    if (take > 0)
                    {
                        ids = ctx.vPricingCommittee.Select(x => x.ID).Take(take);
                    }
                    else
                    {
                        ids = ctx.vPricingCommittee.Select(x => x.ID).ToList();
                    }
                    foreach (int id in ids)
                    {
                        ServiceWorker.SendPricingCommitteeUpdate(ctx.vPricingCommittee.Single(x => x.ID == id).ID, false);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError<SendUpdatesToService>(ex);
                }
            }
            );
        }
    }
}