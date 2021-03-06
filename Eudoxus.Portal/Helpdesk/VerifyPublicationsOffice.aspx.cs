﻿using System;
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
    public partial class VerifyPublicationsOffice : BaseEntityPortalPage<PublicationsOffice>
    {
        protected override void Fill()
        {
            Entity = new PublicationsOfficeRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
            if (Entity != null)
                Entity.PublicationsOfficeDetailsReference.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            ucPublicationsOfficeInput.Entity = Entity;

            if (Entity.IsActivated && Entity.CertificationNumber != null) {
                if (Entity.VerificationStatus == enVerificationStatus.NotVerified) {
                    btnUnVerify.Visible = false;
                    btnVerify.Visible = true;
                    btnReject.Visible = true;
                    btnRestore.Visible = false;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.Verified) {
                    btnUnVerify.Visible = true;
                    btnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = false;
                }
                else {
                    btnVerify.Visible = false;
                    btnUnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = true;
                }
            }
            else {
                btnVerify.Visible = false;
                btnUnVerify.Visible = false;

                if (Entity.VerificationStatus == enVerificationStatus.NotVerified) {
                    btnReject.Visible = true;
                    btnRestore.Visible = false;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.CannotBeVerified) {
                    btnReject.Visible = false;
                    btnRestore.Visible = true;
                }
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucPublicationsOfficeInput.ReadOnly = true;
                ucPublicationsOfficeInput.Bind();
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

                List<PublicationsOffice> institutions = new PublicationsOfficeRepository(UnitOfWork).FindPublicationsOfficesByVerificationStatus(Entity.InstitutionID, enVerificationStatus.NotVerified);

                foreach (PublicationsOffice publicationsOffice in institutions)
                {
                    if (publicationsOffice.ID != Entity.ID)
                    {
                        publicationsOffice.VerificationStatus = enVerificationStatus.CannotBeVerified;
                        updatedIDs.Add(publicationsOffice.ID);
                    }
                }

                IncidentReport ir = new IncidentReport();

                ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                ir.IncidentTypeID = AutomaticIncidentTypes.PublicationsOfficeVerification;
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
                    ServiceWorker.SendPublicationsOfficeUpdate(id);

                HashSet<string> emails = new HashSet<string>();
                PublicationsOfficeDetails publisherDetails = Entity.PublicationsOfficeDetails;


                emails.Add(publisherDetails.PublicationsOfficeEmail);
                emails.Add(Entity.ContactEmail);

                if (!string.IsNullOrEmpty(publisherDetails.AlternateContactEmail))
                {
                    emails.Add(publisherDetails.AlternateContactEmail);
                }


                foreach (string email in emails)
                {
                    MailSender.SendPublicationsOfficeVerification(email, Entity.UserName);
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

            List<PublicationsOffice> institutions = new PublicationsOfficeRepository(UnitOfWork).FindPublicationsOfficesByVerificationStatus(Entity.InstitutionID, enVerificationStatus.CannotBeVerified);

            foreach (PublicationsOffice publicationsOffice in institutions)
            {
                if (publicationsOffice.ID != Entity.ID)
                {
                    publicationsOffice.VerificationStatus = enVerificationStatus.NotVerified;
                    updatedIDs.Add(publicationsOffice.ID);
                }
            }

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublicationsOfficeVerification;
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
                ServiceWorker.SendPublicationsOfficeUpdate(id);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnReject_Click(object sender, EventArgs e) {
            Entity.VerificationStatus = enVerificationStatus.CannotBeVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublicationsOfficeVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRejected, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            ServiceWorker.SendPublicationsOfficeUpdate(Entity.ID);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnRestore_Click(object sender, EventArgs e) {
            Entity.VerificationStatus = enVerificationStatus.NotVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.PublicationsOfficeVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRestored, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            ServiceWorker.SendPublicationsOfficeUpdate(Entity.ID);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
