using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class DataCenterDetailsPopup : System.Web.UI.Page
    {
        public enum enRegisteredDataCenterDetails
        {
            None = 0,
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        private enRegisteredDataCenterDetails _currentMode = enRegisteredDataCenterDetails.None;
        protected enRegisteredDataCenterDetails CurrentMode
        {
            get
            {
                if (_currentMode != enRegisteredDataCenterDetails.None)
                    return _currentMode;

                _currentMode = (enRegisteredDataCenterDetails)Convert.ToInt32(Request.QueryString["t"]);

                return _currentMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsDataCenters_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<DataCenterDetailsView> criteria = new Criteria<DataCenterDetailsView>();

            DateTime date = DateTime.Parse(Request.QueryString["d"]).Date;

            switch (CurrentMode)
            {
                case enRegisteredDataCenterDetails.RegistrationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                case enRegisteredDataCenterDetails.VerificationsByDate:
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