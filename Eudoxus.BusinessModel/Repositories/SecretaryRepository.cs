using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class SecretaryRepository : DomainRepository<HelpDeskEntities, Secretary, int>
    {
        #region [ Base .ctors ]

        public SecretaryRepository() : base() { }

        public SecretaryRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public Secretary FindByUsername(string username)
        {
            Criteria<Secretary> criteria = new Criteria<Secretary>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int secretaryCount;
            IList<Secretary> secretaries = FindSecretariesWithCriteria(criteria, out secretaryCount);

            if (secretaryCount == 1)
            {
                secretaries[0].SecretaryDetailsReference.Load();
                return secretaries[0];
            }

            if (secretaryCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} secretaries found for user {1}", secretaryCount, username));

            return null;
        }

        public Secretary FindByAcademicID(int academicID)
        {
            Criteria<Secretary> criteria = new Criteria<Secretary>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.AcademicID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, enVerificationStatus.Verified);

            int secretaryCount;
            IList<Secretary> secretaries = FindSecretariesWithCriteria(criteria, out secretaryCount);

            if (secretaryCount == 1)
            {
                secretaries[0].SecretaryDetailsReference.Load();
                return secretaries[0];
            }

            if (secretaryCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} secretaries found for academic {1}", secretaryCount, academicID.ToString()));

            return null;
        }

        public List<Secretary> FindSecretariesByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<Secretary> criteria = new Criteria<Secretary>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.Academic.ID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int secretaryCount;
            List<Secretary> secretaries = FindSecretariesWithCriteria(criteria, out secretaryCount);
            return secretaries;
        }

        public List<Secretary> FindSecretariesWithCriteria(Criteria<Secretary> criteria, out int totalRecordCount)
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

        public bool IsAcademicVerified(int secretaryID, int academicID)
        {
            return BaseQuery.Any(x => x.ID != secretaryID && x.Academic.ID == academicID && x.VerificationStatusInt == (int)enVerificationStatus.Verified);
        }

        public bool IsAcademicVerified(int academicID)
        {
            return IsAcademicVerified(0, academicID);
        }

        public Secretary FindVerifiedSecretaryByAcademic(int academicID)
        {
            return BaseQuery.Where(x => x.AcademicID == academicID && x.VerificationStatusInt == (int)enVerificationStatus.Verified).FirstOrDefault();
        }
    }
}