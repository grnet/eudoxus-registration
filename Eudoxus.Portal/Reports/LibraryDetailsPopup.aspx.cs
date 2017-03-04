using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class LibraryDetailsPopup : System.Web.UI.Page
    {
        public enum enRegisteredLibraryDetails
        {
            None = 0,
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        private enRegisteredLibraryDetails _currentMode = enRegisteredLibraryDetails.None;
        protected enRegisteredLibraryDetails CurrentMode
        {
            get
            {
                if (_currentMode != enRegisteredLibraryDetails.None)
                    return _currentMode;

                _currentMode = (enRegisteredLibraryDetails)Convert.ToInt32(Request.QueryString["t"]);

                return _currentMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsLibraries_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<LibraryDetailsView> criteria = new Criteria<LibraryDetailsView>();

            DateTime date = DateTime.Parse(Request.QueryString["d"]).Date;

            switch (CurrentMode)
            {
                case enRegisteredLibraryDetails.RegistrationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                case enRegisteredLibraryDetails.VerificationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.VerificationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.VerificationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                default:
                    break;
            }

            e.InputParameters["criteria"] = criteria;
        }
    }
}