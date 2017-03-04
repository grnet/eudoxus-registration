using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.Reports
{
    public partial class BookSupplierDetailsPopup : System.Web.UI.Page
    {
        public enum enRegisteredBookSupplierDetails
        {
            None = 0,
            RegistrationsByDate = 10,
            VerificationsByDate = 11
        }

        private enRegisteredBookSupplierDetails _currentMode = enRegisteredBookSupplierDetails.None;
        protected enRegisteredBookSupplierDetails CurrentMode
        {
            get
            {
                if (_currentMode != enRegisteredBookSupplierDetails.None)
                    return _currentMode;

                _currentMode = (enRegisteredBookSupplierDetails)Convert.ToInt32(Request.QueryString["t"]);

                return _currentMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsBookSuppliers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<BookSupplierDetailsView> criteria = new Criteria<BookSupplierDetailsView>();

            DateTime date = DateTime.Parse(Request.QueryString["d"]).Date;

            switch (CurrentMode)
            {
                case enRegisteredBookSupplierDetails.RegistrationsByDate:
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date, Imis.Domain.EF.Search.enCriteriaOperator.GreaterThanEquals);
                    criteria.Expression = criteria.Expression.Where(x => x.RegistrationDate, date.AddDays(1), Imis.Domain.EF.Search.enCriteriaOperator.LessThanEquals);
                    break;
                case enRegisteredBookSupplierDetails.VerificationsByDate:
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