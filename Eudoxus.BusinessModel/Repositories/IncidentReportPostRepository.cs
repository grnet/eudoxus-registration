using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;
using Dynamite.Extensions;
using Microsoft.Data.Extensions;

namespace Eudoxus.BusinessModel
{
    public class IncidentReportPostRepository : DomainRepository<HelpDeskEntities, IncidentReportPost, int>
    {
        #region [ Base .ctors ]

        public IncidentReportPostRepository() : base() { }

        public IncidentReportPostRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<IncidentReportPost> FindByIncidentReportID(int incidentReportID)
        {
            var query = BaseQuery.Where(x => x.IncidentReport.ID == incidentReportID);

            return query.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public int GetAnsweredIncidentReportCount(string helpdeskUserName)
        {
            var query = BaseQuery;

            query = query.Where("it.CreatedBy = @createdBy", new ObjectParameter("createdBy", helpdeskUserName));

            return query.Count();
        }
    }
}