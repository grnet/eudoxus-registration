using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class PublicationsOfficeRepository : DomainRepository<HelpDeskEntities, PublicationsOffice, int>
    {
        #region [ Base .ctors ]

        public PublicationsOfficeRepository() : base() { }

        public PublicationsOfficeRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public PublicationsOffice FindByUsername(string username)
        {
            Criteria<PublicationsOffice> criteria = new Criteria<PublicationsOffice>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int publicationsOfficeCount;
            IList<PublicationsOffice> publicationsOffices = FindPublicationsOfficesWithCriteria(criteria, out publicationsOfficeCount);

            if (publicationsOfficeCount == 1)
            {
                publicationsOffices[0].PublicationsOfficeDetailsReference.Load();
                return publicationsOffices[0];
            }

            if (publicationsOfficeCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} publications offices found for user {1}", publicationsOfficeCount, username));

            return null;
        }

        public List<PublicationsOffice> FindPublicationsOfficesByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<PublicationsOffice> criteria = new Criteria<PublicationsOffice>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.Institution.ID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int publicationsOfficeCount;
            List<PublicationsOffice> publicationsOffices = FindPublicationsOfficesWithCriteria(criteria, out publicationsOfficeCount);
            return publicationsOffices;
        }

        public List<PublicationsOffice> FindPublicationsOfficesWithCriteria(Criteria<PublicationsOffice> criteria, out int totalRecordCount)
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

        public bool IsInstitutionVerified(int publicationsOfficeID, int institutionID)
        {
            return BaseQuery.Any(x => x.ID != publicationsOfficeID && x.Institution.ID == institutionID && x.VerificationStatusInt == (int)enVerificationStatus.Verified);
        }

        public bool IsInstitutionVerified(int academicID)
        {
            return IsInstitutionVerified(0, academicID);
        }
    }
}