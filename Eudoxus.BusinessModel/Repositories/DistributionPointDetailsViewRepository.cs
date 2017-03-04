using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class DistributionPointDetailsViewRepository : DomainRepository<HelpDeskViewsEntities, DistributionPointDetailsView, int>
    {
        #region [ Base .ctors ]

        public DistributionPointDetailsViewRepository() : base() { }

        public DistributionPointDetailsViewRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<DistributionPointDetailsView> FindDistributionPointsWithCriteria(Criteria<DistributionPointDetailsView> criteria, out int totalRecordCount)
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
                criteria.SortExpression = "it.RegistrationDate DESC";
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