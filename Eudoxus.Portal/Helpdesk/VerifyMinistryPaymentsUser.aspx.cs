using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;
using Eudoxus.Mails;
using System.Web.Security;
using Eudoxus.eService;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class VerifyMinistryPaymentsUser : BaseEntityPortalPage<MinistryPaymentsUser>
    {
        protected void ddlMinistryAuthorization_Init(object sender, EventArgs e)
        {
            ddlMinistryAuthorization.Items.Add(new ListItem("-- επιλέξτε --", ""));

            ddlMinistryAuthorization.Items.Add(new ListItem(enMinistryAuthorization.ReadOnly.GetLabel(), ((int)enMinistryAuthorization.ReadOnly).ToString()));
            ddlMinistryAuthorization.Items.Add(new ListItem(enMinistryAuthorization.Admin.GetLabel(), ((int)enMinistryAuthorization.Admin).ToString()));
        }

        protected override void Fill()
        {
            Entity = new MinistryPaymentsUserRepository(UnitOfWork).Load(Convert.ToInt32(Request.QueryString["pID"]));
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucMinistryPaymentsUserInput.Entity = Entity;

                if (Entity.MinistryAuthorization == enMinistryAuthorization.None)
                {
                    ddlMinistryAuthorization.Items.FindByValue("").Selected = true;
                }
                else
                {
                    ddlMinistryAuthorization.Items.FindByValue(Entity.MinistryAuthorizationInt.ToString()).Selected = true;
                }
            }

            if (Entity.IsActivated)
            {
                if (Entity.VerificationStatus == enVerificationStatus.NotVerified)
                {
                    btnUnVerify.Visible = false;
                    btnVerify.Visible = true;
                    btnReject.Visible = true;
                    btnRestore.Visible = false;
                }
                else if (Entity.VerificationStatus == enVerificationStatus.Verified)
                {
                    btnUnVerify.Visible = true;
                    btnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = false;

                    ddlMinistryAuthorization.Enabled = false;
                }
                else
                {
                    btnVerify.Visible = false;
                    btnUnVerify.Visible = false;
                    btnReject.Visible = false;
                    btnRestore.Visible = true;

                    ddlMinistryAuthorization.Enabled = false;
                }
            }
            else
            {
                btnVerify.Visible = false;
                btnUnVerify.Visible = false;

                ddlMinistryAuthorization.Enabled = false;

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

            if (!Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SuperHelpdesk)
                && !Roles.IsUserInRole(Page.User.Identity.Name, RoleNames.SystemAdministrator))
            {
                if (Entity.MinistryAuthorization == enMinistryAuthorization.None)
                {
                    phMinistryAuthorization.Visible = false;
                }
                else
                {
                    ddlMinistryAuthorization.Enabled = false;
                }
                btnVerify.Visible = false;
                btnUnVerify.Visible = false;
                btnReject.Visible = false;
                btnRestore.Visible = false;
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucMinistryPaymentsUserInput.ReadOnly = true;
                ucMinistryPaymentsUserInput.Bind();
            }
            base.OnPreRender(e);
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int ministryAuthorization;
                if (int.TryParse(ddlMinistryAuthorization.SelectedValue, out ministryAuthorization) && ministryAuthorization > 0)
                {
                    if (Entity.MinistryAuthorizationInt != ministryAuthorization)
                        Entity.MinistryAuthorizationInt = ministryAuthorization;
                }

                Entity.VerificationStatus = enVerificationStatus.Verified;

                IncidentReport ir = new IncidentReport();

                ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
                ir.IncidentTypeID = AutomaticIncidentTypes.MinistryPaymentsUserVerification;
                ir.Reporter = Entity;
                ir.CallType = enCallType.Outgoing;
                ir.ReportStatus = enReportStatus.Closed;
                ir.ReporterName = Entity.ContactName;
                ir.ReporterPhone = Entity.ContactPhone;
                ir.ReporterEmail = Entity.ContactEmail;
                ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserVerified, User.Identity.Name);
                ir.CreatedBy = User.Identity.Name;

                UnitOfWork.Commit();

                var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
                EudoxusOsyClient.Update(ministryPaymentsUserDto);

                HashSet<string> emails = new HashSet<string>();
                emails.Add(Entity.ContactEmail);

                foreach (string email in emails)
                {
                    MailSender.SendMinistryPaymentsUserVerification(email, Entity.UserName);
                }
            }
            catch (Exception ex)
            {   
                Entity.VerificationStatus = enVerificationStatus.NotVerified;
                UnitOfWork.Commit();
            }

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnUnVerify_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.NotVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.MinistryPaymentsUserVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserUnVerified, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
            EudoxusOsyClient.Update(ministryPaymentsUserDto);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.CannotBeVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.MinistryPaymentsUserVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRejected, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
            EudoxusOsyClient.Update(ministryPaymentsUserDto);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            Entity.VerificationStatus = enVerificationStatus.NotVerified;

            IncidentReport ir = new IncidentReport();

            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = AutomaticIncidentTypes.MinistryPaymentsUserVerification;
            ir.Reporter = Entity;
            ir.CallType = enCallType.Outgoing;
            ir.ReportStatus = enReportStatus.Closed;
            ir.ReporterName = Entity.ContactName;
            ir.ReporterPhone = Entity.ContactPhone;
            ir.ReporterEmail = Entity.ContactEmail;
            ir.ReportText = string.Format(AutomaticIncidentReportMessages.UserRestored, User.Identity.Name);
            ir.CreatedBy = User.Identity.Name;

            UnitOfWork.Commit();

            var ministryPaymentsUserDto = new HelpDeskViewsEntities().vMinistryPaymentsUser.Single(x => x.ID == Entity.ID).ToJsonDto();
            EudoxusOsyClient.Update(ministryPaymentsUserDto);

            ClientScript.RegisterStartupScript(GetType(), "refreshParent", "window.parent.popUp.hide();", true);
        }
    }
}
