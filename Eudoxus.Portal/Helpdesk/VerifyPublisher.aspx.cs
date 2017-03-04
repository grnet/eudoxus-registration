using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;
using Eudoxus.eService;
using Eudoxus.Utils;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class VerifyPublisher : BaseEntityPortalPage<Publisher>
    {
        protected override void Fill()
        {
            Entity = new PublisherRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.PublisherDetailsReference.Load();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack)
            {
                ucPublisherInput.SetPublisher(Entity);
                ucPublisherInput.ReadOnly = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity.IsActivated && Entity.CertificationNumber != null)
            {
                if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
                {
                    btnUnVerify.Visible = false;
                    btnVerify.Visible = true;
                    btnReject.Visible = true;
                    btnRestore.Visible = false;
                    btnDeactivate.Visible = false;
                    btnActivate.Visible = false;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.Verified)
                {
                    btnUnVerify.Visible = true;
                    btnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = false;

                    if (Entity.IsActive)
                    {
                        btnDeactivate.Visible = true;
                        btnActivate.Visible = false;
                    }
                    else
                    {
                        btnDeactivate.Visible = false;
                        btnActivate.Visible = true;
                    }
                }
                else
                {
                    btnVerify.Visible = false;
                    btnUnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = true;
                    btnDeactivate.Visible = false;
                    btnActivate.Visible = false;
                }
            }
            else
            {
                btnVerify.Visible = false;
                btnUnVerify.Visible = false;
                btnDeactivate.Visible = false;
                btnActivate.Visible = false;

                if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
                {
                    btnReject.Visible = true;
                    btnRestore.Visible = false;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified)
                {
                    btnReject.Visible = false;
                    btnRestore.Visible = true;
                }
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            try
            {
                List<int> updatedIDs = new List<int>();
                updatedIDs.Add(Entity.ID);
                Entity.VerificationStatus = enVerificationStatus.Verified;
                Entity.IsActive = true;

                //if (Entity.PublisherType != enPublisherType.EbookPublisher)
                //{
                //    IList<Publisher> publishers = new PublisherRepository(UnitOfWork).FindPublishersByVerificationStatus(Entity.PublisherAFM, enVerificationStatus.NotVerified);

                //    foreach (Publisher publisher in publishers)
                //    {
                //        if (publisher.ID != Entity.ID)
                //        {
                //            publisher.VerificationStatus = enVerificationStatus.CannotBeVerified;
                //            updatedIDs.Add(publisher.ID);
                //        }
                //    }
                //}

                IncidentReport ir = new IncidentReport();

                ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                ir.IncidentTypeID = AutomaticIncidentTypes.PublisherVerification;
                ir.Reporter = Entity;
                ir.CallType = enCallType.Outgoing;
                ir.ReportStatus = enReportStatus.Closed;
                ir.ReporterName = Entity.ContactName;
                ir.ReporterPhone = Entity.ContactPhone;
                ir.ReporterEmail = Entity.ContactEmail;
                ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserVerified, User.Identity.Name);
                ir.CreatedBy = User.Identity.Name;

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

                HashSet<string> emails = new HashSet<string>();
                PublisherDetails publisherDetails = Entity.PublisherDetails;

                if (Entity.PublisherType == enPublisherType.LegalPerson)
                {
                    emails.Add(publisherDetails.LegalPersonEmail);
                    emails.Add(Entity.ContactEmail);

                    if (!string.IsNullOrEmpty(publisherDetails.AlternateContactEmail))
                    {
                        emails.Add(publisherDetails.AlternateContactEmail);
                    }
                }
                else
                {
                    emails.Add(publisherDetails.PublisherEmail);
                    emails.Add(Entity.ContactEmail);
                }

                foreach (string email in emails)
                {
                    if (Entity.PublisherType == enPublisherType.EbookPublisher)
                    {
                        MailSender.SendEbookPublisherVerification(email, Entity.UserName);
                    }
                    else
                    {
                        MailSender.SendPublisherVerification(email, Entity.UserName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, this, string.Format("Προέκυψε σφάλμα στην πιστοποίηση του Εκδότη με Α.Φ.Μ. {0}", Entity.PublisherAFM));
                Entity.VerificationStatus = enVerificationStatus.NotVerified;
                UnitOfWork.Commit();
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnUnVerify_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.NotVerified;
            List<int> updatedIDs = new List<int>();
            updatedIDs.Add(Entity.ID);

            //if (Entity.PublisherType != enPublisherType.EbookPublisher)
            //{
            //    IList<Publisher> publishers = new PublisherRepository(UnitOfWork).FindPublishersByVerificationStatus(Entity.PublisherAFM, enVerificationStatus.CannotBeVerified);

            //    foreach (Publisher publisher in publishers)
            //    {
            //        if (publisher.ID != Entity.ID)
            //        {
            //            publisher.VerificationStatus = enVerificationStatus.NotVerified;
            //            updatedIDs.Add(publisher.ID);
            //        }
            //    }
            //}

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublisherVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserUnVerified, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            foreach (int id in updatedIDs.Distinct())
                ServiceWorker.SendPublisherUpdate(id);

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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.CannotBeVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublisherVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRejected, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            ServiceWorker.SendPublisherUpdate(Entity.ID);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.NotVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublisherVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRestored, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            ServiceWorker.SendPublisherUpdate(Entity.ID);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            Entity.IsActive = false;

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            Entity.IsActive = true;

            UnitOfWork.Commit();

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
