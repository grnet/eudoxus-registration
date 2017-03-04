using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class SubSystems
    {
        private int _RecordCount = 0;

        public int CountSubSystems(Criteria<SubSystem> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SubSystem> FindSubSystems(Criteria<SubSystem> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            var subSystems = new SubSystemRepository().FindSubSystems(criteria, out recordCount);

            _RecordCount = recordCount;

            return subSystems;
        }
    }
}
