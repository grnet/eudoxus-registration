using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;

namespace Eudoxus.BusinessModel
{
    public class LibraryRepository : DomainRepository<HelpDeskEntities, Library, int>
    {
        #region [ Base .ctors ]

        public LibraryRepository() : base() { }

        public LibraryRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public Library FindByUsername(string username)
        {
            Criteria<Library> criteria = new Criteria<Library>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.UserName, username.Trim());

            int libraryCount;
            IList<Library> libraries = FindLibrariesWithCriteria(criteria, out libraryCount);

            if (libraryCount == 1)
            {
                libraries[0].LibraryDetailsReference.Load();
                return libraries[0];
            }

            if (libraryCount > 1)
                throw new Exception(
                    string.Format("The DB is inconsistent : {0} libraries found for user {1}", libraryCount, username));

            return null;
        }

        public List<Library> FindLibrariesByVerificationStatus(int academicID, enVerificationStatus verificationStatus)
        {
            Criteria<Library> criteria = new Criteria<Library>();

            criteria.UsePaging = false;
            criteria.Expression = criteria.Expression.Where(x => x.Institution.ID, academicID);
            criteria.Expression = criteria.Expression.Where(x => x.VerificationStatus, verificationStatus);
            int libraryCount;
            List<Library> libraries = FindLibrariesWithCriteria(criteria, out libraryCount);
            return libraries;
        }

        public List<Library> FindLibrariesWithCriteria(Criteria<Library> criteria, out int totalRecordCount)
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