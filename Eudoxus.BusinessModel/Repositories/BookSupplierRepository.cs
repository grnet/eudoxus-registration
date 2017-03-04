using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class BookSupplierRepository : DomainRepository<HelpDeskEntities, BookSupplier, int>
    {
        #region [ Base .ctors ]

        public BookSupplierRepository() : base() { }

        public BookSupplierRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public BookSupplier FindByUsername(string username)
        {
            Criteria<BookSupplier> criteria = new Criteria<BookSupplier>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int bookSupplierCount;
            IList<BookSupplier> bookSuppliers = FindBookSuppliersWithCriteria(criteria, out bookSupplierCount);

            if (bookSupplierCount == 1)
            {
                bookSuppliers[0].BookSupplierDetailsReference.Load();
                return bookSuppliers[0];
            }

            if (bookSupplierCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} data centers found for user {1}", bookSupplierCount, username));

            return null;
        }

        public List<BookSupplier> FindBookSuppliersByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<BookSupplier> criteria = new Criteria<BookSupplier>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.Institution.ID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int bookSupplierCount;
            List<BookSupplier> bookSuppliers = FindBookSuppliersWithCriteria(criteria, out bookSupplierCount);
            return bookSuppliers;
        }

        public List<BookSupplier> FindBookSuppliersWithCriteria(Criteria<BookSupplier> criteria, out int totalRecordCount)
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

        public bool IsInstitutionVerified(int bookSupplierID, int institutionID)
        {
            return BaseQuery.Any(x => x.ID != bookSupplierID && x.Institution.ID == institutionID && x.VerificationStatusInt == (int)enVerificationStatus.Verified);
        }

        public bool IsInstitutionVerified(int academicID)
        {
            return IsInstitutionVerified(0, academicID);
        }
    }
}