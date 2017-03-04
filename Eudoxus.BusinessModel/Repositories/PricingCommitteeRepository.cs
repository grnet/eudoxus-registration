using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class PricingCommitteeRepository : DomainRepository<HelpDeskEntities, PricingCommittee, int>
    {
        #region [ Base .ctors ]

        public PricingCommitteeRepository() : base() { }

        public PricingCommitteeRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public PricingCommittee FindByUsername(string username)
        {
            Criteria<PricingCommittee> criteria = new Criteria<PricingCommittee>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int pricingCommitteeCount;
            IList<PricingCommittee> pricingCommittees = FindPricingCommitteesWithCriteria(criteria, out pricingCommitteeCount);

            if (pricingCommitteeCount == 1)
            {   
                return pricingCommittees[0];
            }

            if (pricingCommitteeCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} pricingCommittees found for user {1}", pricingCommitteeCount, username));

            return null;
        }

        public List<PricingCommittee> FindPricingCommitteesByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<PricingCommittee> criteria = new Criteria<PricingCommittee>();

            criteria.UsePaging = false;            
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int pricingCommitteeCount;
            List<PricingCommittee> users = FindPricingCommitteesWithCriteria(criteria, out pricingCommitteeCount);
            return users;
        }

        public List<PricingCommittee> FindPricingCommitteesWithCriteria(Criteria<PricingCommittee> criteria, out int totalRecordCount)
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