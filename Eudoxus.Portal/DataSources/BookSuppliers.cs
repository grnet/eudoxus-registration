using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class BookSuppliers
    {
        private int _RecordCount = 0;

        public int CountBookSuppliersWithCriteria(Criteria<BookSupplier> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<BookSupplier> FindBookSuppliersWithCriteria(Criteria<BookSupplier> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var bookSuppliers = new BookSupplierRepository(uow).FindBookSuppliersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return bookSuppliers;
            }
        }
    }
}
