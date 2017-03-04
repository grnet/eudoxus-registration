using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.ComponentModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Views
    {
        private int _RecordCount = 0;

        public int CountSecretariesWithCriteria(Criteria<SecretaryDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountPublicationsOfficesWithCriteria(Criteria<PublicationsOfficeDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountDataCentersWithCriteria(Criteria<DataCenterDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountLibrariesWithCriteria(Criteria<LibraryDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountBookSuppliersWithCriteria(Criteria<BookSupplierDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountDistributionPointsWithCriteria(Criteria<DistributionPointDetailsView> criteria)
        {
            return _RecordCount;
        }

        public int CountIncidentReportsWithCriteria(Criteria<IncidentReportDetailsView> criteria)
        {
            return _RecordCount;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SecretaryDetailsView> FindSecretariesWithCriteria(Criteria<SecretaryDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var secretaries = new SecretaryDetailsViewRepository(uow).FindSecretariesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return secretaries;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<PublicationsOfficeDetailsView> FindPublicationsOfficesWithCriteria(Criteria<PublicationsOfficeDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var publicationsOffices = new PublicationsOfficeDetailsViewRepository(uow).FindPublicationsOfficesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return publicationsOffices;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<DataCenterDetailsView> FindDataCentersWithCriteria(Criteria<DataCenterDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var dataCenters = new DataCenterDetailsViewRepository(uow).FindDataCentersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return dataCenters;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<LibraryDetailsView> FindLibrariesWithCriteria(Criteria<LibraryDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var libraries = new LibraryDetailsViewRepository(uow).FindLibrariesWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return libraries;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<BookSupplierDetailsView> FindBookSuppliersWithCriteria(Criteria<BookSupplierDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var bookSuppliers = new BookSupplierDetailsViewRepository(uow).FindBookSuppliersWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return bookSuppliers;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<DistributionPointDetailsView> FindDistributionPointsWithCriteria(Criteria<DistributionPointDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var dataCenters = new DistributionPointDetailsViewRepository(uow).FindDistributionPointsWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return dataCenters;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<IncidentReportDetailsView> FindIncidentReportsWithCriteria(Criteria<IncidentReportDetailsView> criteria, int startRowIndex, int maximumRows, string sortExpression)
        {
            int recordCount;

            criteria.StartRowIndex = startRowIndex;
            criteria.MaximumRows = maximumRows;
            criteria.SortExpression = sortExpression;

            using (IUnitOfWork uow = UnitOfWorkFactory.Create())
            {
                var dataCenters = new IncidentReportDetailsViewRepository(uow).FindIncidentReportsWithCriteria(criteria, out recordCount);
                _RecordCount = recordCount;
                return dataCenters;
            }
        }
    }
}
