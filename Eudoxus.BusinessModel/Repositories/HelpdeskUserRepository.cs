using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using Imis.Domain;

namespace Eudoxus.BusinessModel
{
    public class HelpdeskUserRepository : DomainRepository<HelpDeskEntities, HelpdeskUser, int>
    {
        #region [ Base .ctors ]

        public HelpdeskUserRepository() : base() { }

        public HelpdeskUserRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public HelpdeskUser FindByUsername(string username, IEnumerable<string> includes = null)
        {
            Criteria<HelpdeskUser> criteria = new Criteria<HelpdeskUser>();

            if (includes != null)
            {
                criteria.Includes = new List<string>();

                foreach (var include in includes)
                {
                    criteria.Includes.Add(include);
                }
            }

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username);

            int helpdeskUserCount;
            IList<HelpdeskUser> helpdeskUsers = FindHelpdeskUsersWithCriteria(criteria, out helpdeskUserCount);

            if (helpdeskUserCount == 1)
            {
                return helpdeskUsers[0];
            }

            if (helpdeskUserCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} helpdeskUsers found for user {1}", helpdeskUserCount, username));

            return null;
        }

        public List<HelpdeskUser> FindHelpdeskUsersWithCriteria(Criteria<HelpdeskUser> criteria, out int totalRecordCount)
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


            if (criteria.UsePaging)
            {
                totalRecordCount = query.Count();
                return query.OrderBy(criteria.SortExpression).Skip(criteria.StartRowIndex).Take(criteria.MaximumRows).ToList();
            }
            var retValue = query.ToList();
            totalRecordCount = retValue.Count;
            return retValue;
        }
    }
}