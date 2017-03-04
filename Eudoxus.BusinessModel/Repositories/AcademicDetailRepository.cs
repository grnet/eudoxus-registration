using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class AcademicDetailRepository : DomainRepository<HelpDeskEntities, AcademicDetail, int>
    {
        #region [ Base .ctors ]

        public AcademicDetailRepository() : base() { }

        public AcademicDetailRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion


        public List<AcademicDetail> FindAcademicDetailsWithCriteria(AcademicDetailCriteria criteria, out int totalRecordCount)
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

            if (!criteria.SecretaryExists.IsEmpty)
            {
                if (criteria.SecretaryExists.FieldValue == true)
                {
                    query = query.Where("AnyElement(it.Secretaries) IS NOT NULL");
                }
                else
                {
                    query = query.Where("AnyElement(it.Secretaries) IS NULL");
                }
            }

            if (string.IsNullOrEmpty(criteria.SortExpression))
                criteria.SortExpression = "it.Institution ASC";
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