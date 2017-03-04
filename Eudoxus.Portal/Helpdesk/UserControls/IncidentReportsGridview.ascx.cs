using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;


namespace Eudoxus.Portal.Helpdesk.UserControls
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Themeable(true)]
    public partial class IncidentReportsGridview : System.Web.UI.UserControl
    {
        #region [ Events ]

        public event EventHandler<DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs> RowCommand;

        #endregion

        #region [ Properties ]

        public string DataSourceID
        {
            get
            {
                return gvIncidentReports.DataSourceID;
            }
            set { gvIncidentReports.DataSourceID = value; }
        }

        public int PageIndex
        {
            get { return gvIncidentReports.PageIndex; }
            set { gvIncidentReports.PageIndex = value; }
        }

        public string SearchControlID { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Browsable(false)]
        [DefaultValue("")]
        [TemplateContainer(typeof(GridViewDataItemTemplateContainer))]
        public ITemplate CustomTemplate { get; set; }

        #endregion

        #region [ Overrides ]

        protected override void OnLoad(EventArgs e)
        {
            var clm = gvIncidentReports.Columns["Commands"] as GridViewDataTextColumn;
            if (clm != null)
            {
                clm.DataItemTemplate = CustomTemplate;
            }
            base.OnLoad(e);
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            SetUItoDefault();
            HideHiddenColumns();
            base.OnPreRender(e);
        }

        protected void gvIncidentReports_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
        {
            if (RowCommand != null)
                RowCommand(gvIncidentReports, e);
        }

        public override void DataBind()
        {
            gvIncidentReports.DataBind();
        }

        #region [ Gridview Methods ]

        protected string GetIncidentTypeDetails(IncidentReport ir)
        {
            if (ir == null)
                return string.Empty;
            string incidentTypeDetails = string.Empty;
            IncidentType it = CacheManager.IncidentTypes.Get(ir.IncidentTypeID);
            incidentTypeDetails = string.Format("ID: <b>{0}</b><br/>{1}<br/>{2}<br/>{3}", ir.ID, ir.CallType.GetLabel(), ir.Reporter.ReporterType.GetLabel(), it.Name);
            return incidentTypeDetails;
        }

        protected string GetHandlerDetails(IncidentReport ir)
        {
            if (ir == null)
                return string.Empty;

            string handlerDetails = string.Empty;

            if (ir.HandlerType == enHandlerType.Helpdesk)
            {
                handlerDetails = string.Format("{0}", enHandlerType.Helpdesk.GetLabel());
            }
            else if (ir.HandlerType == enHandlerType.Supervisor)
            {
                if (ir.HandlerStatus == enHandlerStatus.Pending)
                {
                    handlerDetails = string.Format("{0}<br/><span style=\"color:Red; font-weight:bold\">{1}</span>", ir.HandlerType.GetLabel(), ir.HandlerStatus.GetLabel());
                }
                else if (ir.HandlerStatus == enHandlerStatus.Closed)
                {
                    handlerDetails = string.Format("{0}<br/><span style=\"color:Green; font-weight:bold\">{1}</span>", ir.HandlerType.GetLabel(), ir.HandlerStatus.GetLabel());
                }
            }

            return handlerDetails;
        }

        protected string GetReporterDetails(object reporter)
        {
            if (reporter == null)
                return string.Empty;

            string reporterDetails = string.Empty;

            if (reporter is Online)
            {
                Online online = (Online)reporter;

                if (online.AcademicID.HasValue)
                {
                    var academic = CacheManager.Academics.Get(online.AcademicID.Value);
                    reporterDetails = string.Format("Είδος Χρήστη: {0}<br/>Ίδρυμα: {1}<br/>Σχολή: {2}<br/>Τμήμα: {3}", online.OnlineReporterType.GetLabel(), academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));

                }
                else
                {
                    reporterDetails = string.Format("Είδος Χρήστη: {0}", online.OnlineReporterType.GetLabel());
                }
            }
            else if (reporter is Unknown)
            {
                Unknown unknown = (Unknown)reporter;

                if (!string.IsNullOrEmpty(unknown.IdentificationNumber))
                {
                    reporterDetails = string.Format("Είδος Χρήστη: {0}<br/>Α.Δ.Τ.: {1}<br/>Λοιπά Στοιχεία: {2}", unknown.UnknownReporterType.GetLabel(), unknown.IdentificationNumber, unknown.Description);
                }
                else
                {
                    reporterDetails = string.Format("Είδος Χρήστη: {0}<br/>Λοιπά Στοιχεία: {1}", unknown.UnknownReporterType.GetLabel(), unknown.Description);
                }
            }
            else if (reporter is Publisher)
            {
                Publisher publisher = (Publisher)reporter;
                reporterDetails = string.Format("Επωνυμία: {0}<br/>Α.Φ.Μ.: {1}", publisher.PublisherName, publisher.PublisherAFM);
            }
            else if (reporter is Secretary)
            {
                Secretary secretary = (Secretary)reporter;
                var academic = CacheManager.Academics.Get(secretary.AcademicID);
                reporterDetails = string.Format("Ίδρυμα: {0}<br/>Σχολή: {1}<br/>Τμήμα: {2}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));
            }
            else if (reporter is DistributionPoint)
            {
                DistributionPoint distributionPoint = (DistributionPoint)reporter;
                reporterDetails = string.Format("Τίτλος: {0}", distributionPoint.DistributionPointName);
            }
            else if (reporter is PublicationsOffice)
            {
                PublicationsOffice publicationsOffice = (PublicationsOffice)reporter;
                var institution = CacheManager.Institutions.Get(publicationsOffice.InstitutionID);
                reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }
            else if (reporter is DataCenter)
            {
                DataCenter dataCenter = (DataCenter)reporter;
                var institution = CacheManager.Institutions.Get(dataCenter.InstitutionID);
                reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }
            else if (reporter is Library)
            {
                Library library = (Library)reporter;
                var institution = CacheManager.Institutions.Get(library.InstitutionID);
                reporterDetails = string.Format("Ίδρυμα: {0}<br/>Τίτλος: {1}", institution.Name, library.LibraryName);
            }
            else if (reporter is BookSupplier)
            {
                BookSupplier bookSupplier = (BookSupplier)reporter;
                var institution = CacheManager.Institutions.Get(bookSupplier.InstitutionID);
                reporterDetails = string.Format("Ίδρυμα: {0}", institution.Name);
            }
            else if (reporter is PricingCommittee)
            {
                PricingCommittee pricingCommittee = (PricingCommittee)reporter;                
                reporterDetails = string.Format("Ιδιότητα: {0}", pricingCommittee.PricingCommitteeType);
            }
            else if (reporter is Student)
            {
                Student student = (Student)reporter;

                var academic = CacheManager.Academics.Get(student.AcademicID.GetValueOrDefault());

                if (academic != null)
                {
                    reporterDetails = string.Format("Αρ. Μητρώου: {0}<br/>Ίδρυμα: {1}<br/>Σχολή: {2}<br/>Τμήμα: {3}", student.AcademicIdentifier, academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));
                }
            }
            else if (reporter is Professor)
            {
                Professor professor = (Professor)reporter;
                var academic = CacheManager.Academics.Get(professor.AcademicID.GetValueOrDefault());
                if (academic != null)
                    reporterDetails = string.Format("Ίδρυμα: {0} <br/>Σχολή: {1}<br/>Τμήμα: {2}", academic.Institution, (academic.School != null ? academic.School : "-"), (academic.Department != null ? academic.Department : "-"));

            }

            return reporterDetails;
        }

        #endregion

        private Control GetSearchControl()
        {
            if (string.IsNullOrEmpty(SearchControlID))
                return null;
            var ctrlToFind = Page.FindControlRecursive(SearchControlID);
            if (ctrlToFind == null)
                throw new ArgumentException(string.Format("Control with ID \"{0}\" was not found in the page.", SearchControlID), "SearchControlID");
            return ctrlToFind;
        }
    }
}