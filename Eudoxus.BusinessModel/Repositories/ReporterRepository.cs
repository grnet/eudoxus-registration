using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class ReporterRepository : DomainRepository<HelpDeskEntities, Reporter, int>
    {
        #region [ Base .ctors ]

        public ReporterRepository() : base() { }

        public ReporterRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public List<Reporter> FindReportersWithCriteria(ReporterCriteria criteria, out int totalRecordCount)
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
            if (!criteria.ReporterType.IsEmpty)
            {
                switch (criteria.ReporterType.FieldValue)
                {
                    case enReporterType.Unknown:
                        if (!criteria.UnknownReporterType.IsEmpty)
                        {
                            query = query.OfType<Unknown>().Where("it.UnknownReporterTypeInt = @unknownReporterTypeInt", new ObjectParameter("unknownReporterTypeInt", (int)criteria.UnknownReporterType.FieldValue)).OfType<Reporter>();
                        }

                        if (!criteria.IdentificationNumber.IsEmpty)
                        {
                            query = query.OfType<Unknown>().Where("it.IdentificationNumber = @identificationNumber", new ObjectParameter("identificationNumber", criteria.IdentificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        if (!criteria.Description.IsEmpty)
                        {
                            query = query.OfType<Unknown>().Where("it.Description LIKE \"%" + criteria.Description.FieldValue + "%\"").OfType<Reporter>();
                        }

                        break;
                    case enReporterType.Publisher:
                        if (!criteria.PublisherAFM.IsEmpty)
                        {
                            query = query.OfType<Publisher>().Where("it.PublisherAFM = @publisherAFM", new ObjectParameter("publisherAFM", criteria.PublisherAFM.FieldValue)).OfType<Reporter>();
                        }

                        if (!criteria.PublisherName.IsEmpty)
                        {
                            query = query.OfType<Publisher>().Where("it.PublisherName LIKE \"%" + criteria.PublisherName.FieldValue + "%\" OR it.PublisherTradeName LIKE \"%" + criteria.PublisherName.FieldValue + "%\"").OfType<Reporter>();
                        }

                        if (!criteria.PublisherTradeName.IsEmpty)
                        {
                            query = query.OfType<Publisher>().Where("it.PublisherTradeName LIKE \"%" + criteria.PublisherTradeName.FieldValue + "%\"").OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<Publisher>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }
                        break;
                    case enReporterType.Secretary:
                        if (!criteria.AcademicID.IsEmpty)
                        {
                            query = query.OfType<Secretary>().Where("it.Academic.ID = " + criteria.AcademicID.FieldValue).OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<Secretary>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.DistributionPoint:
                        if (!criteria.DistributionPointName.IsEmpty)
                        {
                            query = query.OfType<DistributionPoint>().Where("it.DistributionPointName LIKE \"%" + criteria.DistributionPointName.FieldValue + "%\"").OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<DistributionPoint>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }
                        break;
                    case enReporterType.PublicationsOffice:
                        if (!criteria.InstitutionID.IsEmpty)
                        {
                            query = query.OfType<PublicationsOffice>().Where("it.Institution.ID = " + criteria.InstitutionID.FieldValue).OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<PublicationsOffice>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.DataCenter:
                        if (!criteria.InstitutionID.IsEmpty)
                        {
                            query = query.OfType<DataCenter>().Where("it.Institution.ID = " + criteria.InstitutionID.FieldValue).OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<DataCenter>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.Library:
                        if (!criteria.InstitutionID.IsEmpty)
                        {
                            query = query.OfType<DataCenter>().Where("it.Institution.ID = " + criteria.InstitutionID.FieldValue).OfType<Reporter>();
                        }

                        if (!criteria.LibraryName.IsEmpty)
                        {
                            query = query.OfType<Library>().Where("it.LibraryName = @libraryName", new ObjectParameter("libraryName", criteria.LibraryName.FieldValue)).OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<DataCenter>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.BookSupplier:
                        if (!criteria.InstitutionID.IsEmpty)
                        {
                            query = query.OfType<BookSupplier>().Where("it.Institution.ID = " + criteria.InstitutionID.FieldValue).OfType<Reporter>();
                        }

                        if (!criteria.CertificationNumber.IsEmpty)
                        {
                            query = query.OfType<BookSupplier>().Where("it.CertificationNumber = @certificationNumber", new ObjectParameter("certificationNumber", criteria.CertificationNumber.FieldValue)).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.Student:
                        if (!criteria.AcademicIdentifier.IsEmpty)
                        {
                            query = query.OfType<Student>().Where("it.AcademicIdentifier = @academicIdentifier", new ObjectParameter("academicIdentifier", criteria.AcademicIdentifier.FieldValue)).OfType<Reporter>();
                        }

                        if (!criteria.AcademicID.IsEmpty)
                        {
                            query = query.OfType<Student>().Where("it.Academic.ID = " + criteria.AcademicID.FieldValue).OfType<Reporter>();
                        }

                        break;
                    case enReporterType.Professor:
                        if (!criteria.AcademicID.IsEmpty)
                        {
                            query = query.OfType<Professor>().Where("it.Academic.ID = " + criteria.AcademicID.FieldValue).OfType<Reporter>();
                        }

                        break;
                }
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


        public Reporter FindByID<T>(int id)
            where T : Reporter
        {
            return BaseQuery.OfType<T>().FirstOrDefault(x => x.ID == id);
        }

        public Reporter FindByUsername(string username)
        {
            return BaseQuery.FirstOrDefault(x => x.UserName == username);
        }

        public enReporterType FindReporterTypeByUsername(string username)
        {
            int type = BaseQuery.Where(x => x.CreatedBy == username).Select(x => x.ReporterTypeInt).FirstOrDefault();
            return (enReporterType)type;
        }
    }
}