using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;
using Imis.Domain;
using Dynamite.Extensions;

namespace Eudoxus.BusinessModel
{
    public class LessonReportRepository : DomainRepository<HelpDeskEntities, LessonReport, int>
    {
        #region [ Base .ctors ]

        public LessonReportRepository() : base() { }

        public LessonReportRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<LessonReport> FindLessonReportsWithCriteria(LessonReportCriteria criteria, out int totalRecordCount)
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

            if (!criteria.AcademicID.IsEmpty)
            {
                query = query
                    .Where("it.Reporter is of(Eudoxus.BusinessModel.Secretary) and TREAT(it.Reporter as Eudoxus.BusinessModel.Secretary).AcademicID = @typ", new ObjectParameter("typ", (int)criteria.AcademicID.FieldValue));
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