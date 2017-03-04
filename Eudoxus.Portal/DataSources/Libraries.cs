using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Libraries
    {
        private int _RecordCount = 0;

        public int CountLibrariesWithCriteria(Criteria<Library> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Library> FindLibrariesWithCriteria(Criteria<Library> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var libraries = new LibraryRepository(uow).FindLibrariesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return libraries;
            }
        }
    }
}
