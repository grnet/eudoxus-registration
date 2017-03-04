﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class DataCenters
    {
        private int _RecordCount = 0;

        public int CountDataCentersWithCriteria(Criteria<DataCenter> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<DataCenter> FindDataCentersWithCriteria(Criteria<DataCenter> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var dataCenters = new DataCenterRepository(uow).FindDataCentersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return dataCenters;
            }
        }
    }
}
