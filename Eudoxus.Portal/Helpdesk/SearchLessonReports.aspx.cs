using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using System.Xml.Linq;
using Eudoxus.Portal.DataSources;
using System.Web.Security;
using System.IO;
using System.Reflection;
using System.Xml;
using Imis.Domain;
using Eudoxus.Portal.Controls;
using Microsoft.Data.Extensions;
using Eudoxus.Portal.Helpdesk.UserControls;
using System.Web.Services;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class SearchLessonReports : BasePortalPage
    {
        private IUnitOfWork unitOfWork = null;
        private Reporter _currentReporter = null;
        //private bool _bindDataWhenSearchIsClicked = false;


        protected Reporter CurrentReporter
        {
            get
            {
                if (_currentReporter != null)
                    return _currentReporter;
                try
                {
                    int reporterID = Convert.ToInt32(Request.QueryString["rID"]);
                    if (reporterID > 0)
                        _currentReporter = new ReporterRepository(unitOfWork).Load(reporterID);
                }
                catch (FormatException)
                {
                }

                return _currentReporter;
            }
        }

        protected bool HideFilters
        {
            get
            {
                bool hideFilters;

                bool.TryParse(Request.QueryString["hideFilters"], out hideFilters);

                return hideFilters;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "jsInit", string.Format("hd.init('{0}','{1}','{2}','{3}');", hfSchoolCode.ClientID, txtInstitutionName.ClientID, txtSchoolName.ClientID, txtDepartmentName.ClientID), true);

            txtInstitutionName.Attributes.Add("readonly", "readonly");
            txtSchoolName.Attributes.Add("readonly", "readonly");
            txtDepartmentName.Attributes.Add("readonly", "readonly");

            txtInstitutionName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtInstitutionName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtSchoolName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtSchoolName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            txtDepartmentName.Attributes.Add("onclick", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");
            txtDepartmentName.Attributes.Add("onfocus", "popUp.show('../Common/SchoolSelectPopup.aspx', 'Επιλογή Σχολής');");

            unitOfWork = UnitOfWorkFactory.Create();
            ScriptManager.GetCurrent(this).EnablePageMethods = true;
            if (HideFilters)
            {
                tbFilters.Visible = false;
                btnSearch.Visible = false;
            }

            gvIncidentReports.DefaultColumns = new List<UserControls.enIncidentReportsGridviewColumns>()
                {
                     enIncidentReportsGridviewColumns.CreatedAt,
                     enIncidentReportsGridviewColumns.LastPost_PostText,
                     enIncidentReportsGridviewColumns.Reporter_ReporterType,
                     enIncidentReportsGridviewColumns.ReporterName,
                     enIncidentReportsGridviewColumns.ReportStatus,
                     enIncidentReportsGridviewColumns.SpecialDetailsOfReporter,
                     enIncidentReportsGridviewColumns.ReportText,
                     enIncidentReportsGridviewColumns.Commands,
                      enIncidentReportsGridviewColumns.CallType,
                      enIncidentReportsGridviewColumns.HandlerType
                };
        }

        protected void ddlReportStatus_Init(object sender, EventArgs e)
        {
            ddlReportStatus.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enReportStatus item in Enum.GetValues(typeof(enReportStatus)))
            {
                ddlReportStatus.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlHandlerType_Init(object sender, EventArgs e)
        {
            ddlHandlerType.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (enHandlerType item in Enum.GetValues(typeof(enHandlerType)))
            {
                ddlHandlerType.Items.Add(new ListItem(item.GetLabel(), ((int)item).ToString()));
            }
        }

        protected void ddlHandlerStatus_Init(object sender, EventArgs e)
        {
            ddlHandlerStatus.Items.Add(new ListItem("-- αδιάφορο --", ""));

            ddlHandlerStatus.Items.Add(new ListItem(enHandlerStatus.Pending.GetLabel(), ((int)enHandlerStatus.Pending).ToString()));
            ddlHandlerStatus.Items.Add(new ListItem(enHandlerStatus.Closed.GetLabel(), ((int)enHandlerStatus.Closed).ToString()));
        }

        protected void ddlAcademicYear_Init(object sender, EventArgs e)
        {
            ddlAcademicYear.Items.Add(new ListItem("-- αδιάφορο --", ""));

            for (int i = 2010; i < 2020; i++)
            {
                string year = i.ToString() + " - " + (i + 1).ToString();

                ddlAcademicYear.Items.Add(new ListItem(year, i.ToString()));
            }
        }

        protected void ddlInstitution_Init(object sender, EventArgs e)
        {
            ddlInstitution.Items.Add(new ListItem("-- αδιάφορο --", ""));

            foreach (var item in CacheManager.Institutions.GetItems())
            {
                ddlInstitution.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvIncidentReports.DataBind();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            gvIncidentReports.PageIndex = 0;
            //_bindDataWhenSearchIsClicked = true;
            gvIncidentReports.DataBind();
        }

        protected void odsIncidentReports_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //var ctrl = btnSearch;
            //if (ctrl != null && Request.Form["__EVENTTARGET"] == ctrl.UniqueID)
            //{
            //    if (!_bindDataWhenSearchIsClicked)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //}
            LessonReportCriteria criteria = new LessonReportCriteria();

            criteria.Includes = new List<string>();
            criteria.Includes.Add("Reporter");
            criteria.Includes.Add("LastPost.LastDispatch");

            //criteria.Expression = criteria.Expression.Where(x => x.SubSystem.ID, HelpDeskConstants.DEFAULT_SUBSYSTEM_ID);
            criteria.Expression = criteria.Expression.Where(x => x.SubmissionType, enReportSubmissionType.InternalReporter);
            criteria.Expression = criteria.Expression.Where(x => x.IncidentTypeID, AutomaticIncidentTypes.SecretaryLessonSubmission);

            if (CurrentReporter != null)
            {
                criteria.Expression = criteria.Expression.Where(x => x.Reporter.ID, CurrentReporter.ID);
            }

            int reportStatus;
            if (int.TryParse(ddlReportStatus.SelectedItem.Value, out reportStatus) && reportStatus > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.ReportStatus, (enReportStatus)reportStatus);
            }

            int handlerType;
            if (int.TryParse(ddlHandlerType.SelectedItem.Value, out handlerType) && handlerType > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.HandlerType, (enHandlerType)handlerType);
            }

            int handlerStatus;
            if (int.TryParse(ddlHandlerStatus.SelectedItem.Value, out handlerStatus) && handlerStatus > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.HandlerStatus, (enHandlerStatus)handlerStatus);
            }

            int academicYear;
            if (int.TryParse(ddlAcademicYear.SelectedItem.Value, out academicYear) && academicYear > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.AcademicYear, academicYear);
            }

            int institutionID;
            if (int.TryParse(ddlInstitution.SelectedItem.Value, out institutionID) && institutionID > 0)
            {
                criteria.Expression = criteria.Expression.Where(x => x.InstitutionID, institutionID);
            }

            int schoolCode;
            if (int.TryParse(hfSchoolCode.Value, out schoolCode))
            {
                criteria.AcademicID.FieldValue = schoolCode;
            }

            e.InputParameters["criteria"] = criteria;
        }

        [WebMethod]
        public static string ChangeStatus(int incidentReportID, enReportStatus newStatus)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var ic = new IncidentReportRepository(uow).Load(incidentReportID);
                if (ic == null)
                    return null;
                ic.ReportStatus = newStatus;
                uow.Commit();
                return newStatus.GetIcon();
            }
        }

    }
}
