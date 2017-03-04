using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class DataCenterRepository : DomainRepository<HelpDeskEntities, DataCenter, int>
    {
        #region [ Base .ctors ]

        public DataCenterRepository() : base() { }

        public DataCenterRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public DataCenter FindByUsername(string username)
        {
            Criteria<DataCenter> criteria = new Criteria<DataCenter>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int dataCenterCount;
            IList<DataCenter> dataCenters = FindDataCentersWithCriteria(criteria, out dataCenterCount);

            if (dataCenterCount == 1)
            {
                dataCenters[0].DataCenterDetailsReference.Load();
                return dataCenters[0];
            }

            if (dataCenterCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} data centers found for user {1}", dataCenterCount, username));

            return null;
        }

        public List<DataCenter> FindDataCentersByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<DataCenter> criteria = new Criteria<DataCenter>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.Institution.ID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int dataCenterCount;
            List<DataCenter> dataCenters = FindDataCentersWithCriteria(criteria, out dataCenterCount);
            return dataCenters;
        }

        public List<DataCenter> FindDataCentersWithCriteria(Criteria<DataCenter> criteria, out int totalRecordCount)
        {
            var query = BaseQuery;

            if (criteria.Includes != null)
            {
                criteria.Includes.ForEach(x => query = query.Include(x));
            }

            if (!string.IsNullOrEmpty(criteria.Expression.CommandText))
            {
                query = query.Where(criteria.Expression.CommandText, criteria.Expression.Parameters);
            }

            if (string.IsNullOrEmpty(criteria.SortExpression))
                criteria.SortExpression = "it.CreatedAt DESC";
            else
                criteria.SortExpression = "it." + criteria.SortExpression.Replace(",", ",it.");

            totalRecordCount = query.Count();

            if (criteria.UsePaging)
            {
                return query.OrderBy(criteria.SortExpression).Skip(criteria.StartRowIndex).Take(criteria.MaximumRows).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public bool IsInstitutionVerified(int dataCenterID, int institutionID)
        {
            return BaseQuery.Any(x => x.ID != dataCenterID && x.Institution.ID == institutionID && x.VerificationStatusInt == (int)enVerificationStatus.Verified);
        }

        public bool IsInstitutionVerified(int academicID)
        {
            return IsInstitutionVerified(0, academicID);
        }
    }
}