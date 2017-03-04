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
    public class IncidentReportRepository : DomainRepository<HelpDeskEntities, IncidentReport, int>
    {
        #region [ Base .ctors ]

        public IncidentReportRepository() : base() { }

        public IncidentReportRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<IncidentReport> FindIncidentReportsWithCriteria(IncidentReportCriteria criteria, out int totalRecordCount)
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

            if (!criteria.OnlineReporterType.IsEmpty)
            {
                query = query
                    .Where("(it.Reporter is of(Eudoxus.BusinessModel.Online) and TREAT(it.Reporter as Eudoxus.BusinessModel.Online).OnlineReporterTypeInt = @onlineReporterType) or it.Reporter.ReporterTypeInt = @onlineReporterType", new ObjectParameter("onlineReporterType", (int)criteria.OnlineReporterType.FieldValue));
            }

            if (!criteria.AcademicID.IsEmpty)
            {
                query = query
                    .Where("it.Reporter is of(Eudoxus.BusinessModel.Secretary) and TREAT(it.Reporter as Eudoxus.BusinessModel.Secretary).AcademicID = @academicID", new ObjectParameter("academicID", (int)criteria.AcademicID.FieldValue));
            }

            if (!criteria.OnlineAcademicID.IsEmpty)
            {
                query = query
                    .Where("(it.Reporter is of(Eudoxus.BusinessModel.Secretary) and TREAT(it.Reporter as Eudoxus.BusinessModel.Secretary).AcademicID = @onlineAcademicID) or (it.Reporter is of(Eudoxus.BusinessModel.Online) and TREAT(it.Reporter as Eudoxus.BusinessModel.Online).AcademicID = @onlineAcademicID)", new ObjectParameter("onlineAcademicID", (int)criteria.OnlineAcademicID.FieldValue));
            }

            if (!criteria.ReportText.IsEmpty)
            {
                query = query.Where("it.ReportText LIKE \"%" + criteria.ReportText.FieldValue + "%\"");
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