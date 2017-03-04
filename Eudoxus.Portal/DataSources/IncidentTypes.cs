using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class IncidentTypes
    {
        private int _RecordCount = 0;

        public int CountIncidentTypes(Criteria<IncidentType> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<IncidentType> FindIncidentTypes(Criteria<IncidentType> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            var IncidentTypes = new IncidentTypeRepository().FindIncidentTypes(criteria, out recordCount);

            _RecordCount = recordCount;

            return IncidentTypes;
        }
    }
}
