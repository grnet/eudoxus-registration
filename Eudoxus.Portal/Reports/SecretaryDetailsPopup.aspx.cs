using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class SecretaryDetailsPopup : System.Web.UI.Page
    {
        public enum enRegisteredSecretaryDetails
        {
            None = 0,
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        private enRegisteredSecretaryDetails _currentMode = enRegisteredSecretaryDetails.None;
        protected enRegisteredSecretaryDetails CurrentMode
        {
            get
            {
                if (_currentMode != enRegisteredSecretaryDetails.None)
                    return _currentMode;

                _currentMode = (enRegisteredSecretaryDetails)Convert.ToInt32(Request.QueryString["t"]);

                return _currentMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsSecretaries_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<SecretaryDetailsView> criteria = new Criteria<SecretaryDetailsView>();

            DateTime date = DateTime.Parse(Request.QueryString["d"]).Date;

            switch (CurrentMode)
            {
                case enRegisteredSecretaryDetails.RegistrationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                case enRegisteredSecretaryDetails.VerificationsByDate:
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