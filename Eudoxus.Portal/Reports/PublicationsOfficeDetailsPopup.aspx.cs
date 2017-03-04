using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class PublicationsOfficeDetailsPopup : System.Web.UI.Page
    {
        public enum enRegisteredPublicationsOfficeDetails
        {
            None = 0,
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        private enRegisteredPublicationsOfficeDetails _currentMode = enRegisteredPublicationsOfficeDetails.None;
        protected enRegisteredPublicationsOfficeDetails CurrentMode
        {
            get
            {
                if (_currentMode != enRegisteredPublicationsOfficeDetails.None)
                    return _currentMode;

                _currentMode = (enRegisteredPublicationsOfficeDetails)Convert.ToInt32(Request.QueryString["t"]);

                return _currentMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsPublicationsOffices_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<PublicationsOfficeDetailsView> criteria = new Criteria<PublicationsOfficeDetailsView>();

            DateTime date = DateTime.Parse(Request.QueryString["d"]).Date;

            switch (CurrentMode)
            {
                case enRegisteredPublicationsOfficeDetails.RegistrationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                case enRegisteredPublicationsOfficeDetails.VerificationsByDate:
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