using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class IncidentTypeCacheManager : DomainCacheManager<HelpDeskEntities, IncidentType, int>
    {
        public IncidentTypeCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<IncidentType> oqEntities = s_Repository.LoadAll();
            List<IncidentType> incidentTypes = oqEntities.ToList();

            foreach (IncidentType incidentType in incidentTypes)
            {
                s_CacheStorage.Add(GetKey(incidentType), incidentType);
            }
        }
    }
}
