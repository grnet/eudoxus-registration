using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.Portal.Controls;
using Eudoxus.BusinessModel;


namespace Eudoxus.Portal.Secure.UserControls
{
    public partial class ContactFormInput : BaseUserControl<BaseEntityPortalPage<Reporter>>
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IList<IncidentType> incidentTypes = CacheManager.IncidentTypes.GetItems();

            Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, string.Empty);

            foreach (IncidentType incidentType in incidentTypes)
            {
                Page.ClientScript.RegisterForEventValidation(ddlIncidentType.UniqueID, incidentType.ID.ToString());
            }

            base.Render(writer);
        }

        protected void ddlReporterType_Init(object sender, EventArgs e)
        {
            ddlReporterType.Items.Add(new ListItem(enReporterType.Publisher.GetLabel(), ((int)enReporterType.Publisher).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Secretary.GetLabel(), ((int)enReporterType.Secretary).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.PublicationsOffice.GetLabel(), ((int)enReporterType.PublicationsOffice).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DataCenter.GetLabel(), ((int)enReporterType.DataCenter).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.DistributionPoint.GetLabel(), ((int)enReporterType.DistributionPoint).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.Library.GetLabel(), ((int)enReporterType.Library).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.BookSupplier.GetLabel(), ((int)enReporterType.BookSupplier).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.PricingCommittee.GetLabel(), ((int)enReporterType.PricingCommittee).ToString()));
            ddlReporterType.Items.Add(new ListItem(enReporterType.MinistryPayments.GetLabel(), ((int)enReporterType.MinistryPayments).ToString()));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillContactForm(IncidentReport ir)
        {
            ir.SubSystemID = HelpDeskConstants.DEFAULT_SUBSYSTEM_ID;
            ir.IncidentTypeID = Convert.ToInt32(ddlIncidentType.SelectedValue);
            ir.SubmissionType = enReportSubmissionType.LoggedInUser;

            ir.ReporterName = txtReporterName.Text.ToNull();
            ir.ReporterPhone = txtReporterPhone.Text.ToNull();
            ir.ReporterEmail = txtReporterEmail.Text.ToNull();

            ir.ReportText = txtReportText.Text;
            ir.CallType = enCallType.Incoming;
            ir.ReportStatus = enReportStatus.Pending;
        }

        public void SetContactForm(Reporter reporter)
        {
            ddlReporterType.SelectedValue = ((int)reporter.ReporterType).ToString();
            ddlReporterType.Enabled = false;

            txtReporterName.Text = reporter.ContactName;
            txtReporterPhone.Text = reporter.ContactPhone;
            txtReporterEmail.Text = reporter.ContactEmail;
        }

        public string ValidationGroup
        {
            get
            {
                return rfvReporterName.ValidationGroup;
            }
            set
            {
                foreach (var validator in this.RecursiveOfType<BaseValidator>())
                    validator.ValidationGroup = value;
            }
        }
    }
}