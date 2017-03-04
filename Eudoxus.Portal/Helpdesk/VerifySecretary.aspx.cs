using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class VerifySecretary : BaseEntityPortalPage<Secretary>
    {
        protected override void Fill()
        {
            Entity = new SecretaryRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["sID"]));
            if (Entity != null)
                Entity.SecretaryDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucSecretaryInput.Entity = Entity;

            if (Entity.IsActivated && Entity.CertificationNumber != null)
            {
                if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
                {
                    btnUnVerify.Visible = false;
                    btnVerify.Visible = true;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.Verified)
                {
                    btnUnVerify.Visible = true;
                    btnVerify.Visible = false;
                }
                else
                {
                    btnVerify.Visible = false;
                    btnUnVerify.Visible = false;
                }
            }
            else
            {
                btnVerify.Visible = false;
                btnUnVerify.Visible = false;
            }

            //Μόνιμα απενεργοποιημένο λόγω του ότι δεν θέλουμε μια γραμματεία να αποπιστοποιηθεί και να φτιάξει νέο account οπότε και να αλλάξει ID (βλέπε ΚΠΣ)
            btnUnVerify.Visible = false;

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucSecretaryInput.ReadOnly = true;
                ucSecretaryInput.Bind();
            }
            base.OnPreRender(e);
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

                List<Secretary> secretaries = new SecretaryRepository(UnitOfWork).FindSecretariesByVerificationStatus(Entity.AcademicID, enVerificationStatus.NotVerified);

                foreach (Secretary secretary in secretaries)
                {
                    if (secretary.ID != Entity.ID)
                    {
                        secretary.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(secretary.ID);
                    }
                }

                IncidentReport ir = new IncidentReport();

                ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                ir.IncidentTypeID = AutomaticIncidentTypes.SecretaryVerification;
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
                    ServiceWorker.SendSecretaryUpdate(id);

                HashSet<string> emails = new HashSet<string>();
                SecretaryDetails publisherDetails = Entity.SecretaryDetails;


                emails.Add(publisherDetails.SecretaryEmail);
                emails.Add(Entity.ContactEmail);

                if (!string.IsNullOrEmpty(publisherDetails.AlternateContactEmail))
                {
                    emails.Add(publisherDetails.AlternateContactEmail);
                }


                foreach (string email in emails)
                {
                    MailSender.SendSecretaryVerification(email, Entity.UserName);
                }
            }
            catch (Exception ex)
            {
                //ex.Message = string.Format("Προέκυψε σφάλμα στην πιστοποίηση του Εκδότη με Α.Φ.Μ. {0}", Entity.PublisherAFM);
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

            List<Secretary> secretaries = new SecretaryRepository(UnitOfWork).FindSecretariesByVerificationStatus(Entity.AcademicID, enVerificationStatus.CannotBeVerified);

            foreach (Secretary secretary in secretaries)
            {
                if (secretary.ID != Entity.ID)
                {
                    secretary.VerificationStatus = enVerificationStatus.NotVerified;
                    updatedIDs.Add(secretary.ID);
                }
            }

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.SecretaryVerification;
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
                ServiceWorker.SendSecretaryUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
