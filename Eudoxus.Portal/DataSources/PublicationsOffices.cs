using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class PublicationsOffices
    {
        private int _RecordCount = 0;

        public int CountPublicationsOfficesWithCriteria(Criteria<PublicationsOffice> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<PublicationsOffice> FindPublicationsOfficesWithCriteria(Criteria<PublicationsOffice> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var publicationsOffices = new PublicationsOfficeRepository(uow).FindPublicationsOfficesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return publicationsOffices;
            }
        }
    }
}
