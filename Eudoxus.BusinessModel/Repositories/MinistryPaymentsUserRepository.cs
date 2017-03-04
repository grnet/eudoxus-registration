using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class MinistryPaymentsUserRepository : DomainRepository<HelpDeskEntities, MinistryPaymentsUser, int>
    {
        #region [ Base .ctors ]

        public MinistryPaymentsUserRepository() : base() { }

        public MinistryPaymentsUserRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public MinistryPaymentsUser FindByUsername(string username)
        {
            Criteria<MinistryPaymentsUser> criteria = new Criteria<MinistryPaymentsUser>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int ministryPaymentsUserCount;
            IList<MinistryPaymentsUser> MinistryPaymentsUsers = FindMinistryPaymentsUsersWithCriteria(criteria, out ministryPaymentsUserCount);

            if (ministryPaymentsUserCount == 1)
            {
                return MinistryPaymentsUsers[0];
            }

            if (ministryPaymentsUserCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} MinistryPaymentsUsers found for user {1}", ministryPaymentsUserCount, username));

            return null;
        }

        public List<MinistryPaymentsUser> FindMinistryPaymentsUsersByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<MinistryPaymentsUser> criteria = new Criteria<MinistryPaymentsUser>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int ministryPaymentsUserCount;
            List<MinistryPaymentsUser> users = FindMinistryPaymentsUsersWithCriteria(criteria, out ministryPaymentsUserCount);
            return users;
        }

        public List<MinistryPaymentsUser> FindMinistryPaymentsUsersWithCriteria(Criteria<MinistryPaymentsUser> criteria, out int totalRecordCount)
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