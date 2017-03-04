using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class DistributionPointRepository : DomainRepository<HelpDeskEntities, DistributionPoint, int>
    {
        #region [ Base .ctors ]

        public DistributionPointRepository() : base() { }

        public DistributionPointRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public DistributionPoint FindByUsername(string username)
        {
            Criteria<DistributionPoint> criteria = new Criteria<DistributionPoint>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int distributionPointCount;
            IList<DistributionPoint> distributionPoints = FindDistributionPointsWithCriteria(criteria, out distributionPointCount);

            if (distributionPointCount == 1)
            {
                distributionPoints[0].DistributionPointDetailsReference.Load();
                return distributionPoints[0];
            }

            if (distributionPointCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} distibution points found for user {1}", distributionPointCount, username));

            return null;
        }

        public DistributionPoint FindByCreator(string creatorUsername)
        {
            Criteria<DistributionPoint> criteria = new Criteria<DistributionPoint>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.DistributionPointCreator, creatorUsername);

            int distributionPointCount;
            IList<DistributionPoint> distributionPoints = FindDistributionPointsWithCriteria(criteria, out distributionPointCount);

            if (distributionPointCount == 1)
            {
                distributionPoints[0].DistributionPointDetailsReference.Load();
                return distributionPoints[0];
            }

            if (distributionPointCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} distibution points found for creator {1}", distributionPointCount, creatorUsername));

            return null;
        }

        public List<DistributionPoint> FindDistributionPointsWithCriteria(Criteria<DistributionPoint> criteria, out int totalRecordCount)
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
    }
}