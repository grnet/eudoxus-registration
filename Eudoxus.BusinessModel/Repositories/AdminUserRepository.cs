using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using Imis.Domain;

namespace Eudoxus.BusinessModel
{
    public class AdminUserRepository : DomainRepository<HelpDeskEntities, AdminUser, int>
    {
        #region [ Base .ctors ]

        public AdminUserRepository() : base() { }

        public AdminUserRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public AdminUser FindByUsername(string username, IEnumerable<string> includes = null)
        {
            Criteria<AdminUser> criteria = new Criteria<AdminUser>();

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

            int adminUserCount;
            IList<AdminUser> adminUsers = FindAdminUsersWithCriteria(criteria, out adminUserCount);

            if (adminUserCount == 1)
            {
                return adminUsers[0];
            }

            if (adminUserCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} adminUsers found for user {1}", adminUserCount, username));

            return null;
        }

        public List<AdminUser> FindAdminUsersWithCriteria(Criteria<AdminUser> criteria, out int totalRecordCount)
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