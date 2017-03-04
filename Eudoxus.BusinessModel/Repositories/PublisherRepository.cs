using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class PublisherRepository : DomainRepository<HelpDeskEntities, Publisher, int>
    {
        #region [ Base .ctors ]

        public PublisherRepository() : base() { }

        public PublisherRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public Publisher FindByUsername(string username)
        {
            Criteria<Publisher> criteria = new Criteria<Publisher>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int publisherCount;
            IList<Publisher> publishers = FindPublishersWithCriteria(criteria, out publisherCount);

            if (publisherCount == 1)
            {
                publishers[0].PublisherDetailsReference.Load();
                return publishers[0];
            }

            if (publisherCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} publishers found for user {1}", publisherCount, username));

            return null;
        }


        public bool IsAfmVerified(int currentReporterID, string afm)
        {
            return FindPublishersByVerificationStatus(afm, enVerificationStatus.Verified).Any(x => x.ID != currentReporterID);
        }

        public IList<Publisher> FindPublishersByVerificationStatus(string afm, enVerificationStatus verificationStatus)
        {
            Criteria<Publisher> criteria = new Criteria<Publisher>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.PublisherAFM, afm);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int publisherCount;
            List<Publisher> publishers = FindPublishersWithCriteria(criteria, out publisherCount);

            return publishers;
        }

        public List<Publisher> FindPublishersWithCriteria(Criteria<Publisher> criteria, out int totalRecordCount)
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