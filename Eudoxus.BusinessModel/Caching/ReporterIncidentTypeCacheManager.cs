using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class ReporterIncidentTypeCacheManager : DomainCacheManager<HelpDeskEntities, ReporterIncidentType, int>
    {
        public ReporterIncidentTypeCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<ReporterIncidentType> oqEntities = s_Repository.LoadAll();
            List<ReporterIncidentType> reporterIncidentTypes = oqEntities.ToList();

            foreach (ReporterIncidentType reporterIncidentType in reporterIncidentTypes)
            {
                s_CacheStorage.Add(GetKey(reporterIncidentType), reporterIncidentType);
            }
        }
    }
}
