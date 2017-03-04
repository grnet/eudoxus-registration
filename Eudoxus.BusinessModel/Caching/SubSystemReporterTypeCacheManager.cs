using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class SubSystemReporterTypeCacheManager : DomainCacheManager<HelpDeskEntities, SubSystemReporterType, int>
    {
        public SubSystemReporterTypeCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<SubSystemReporterType> oqEntities = s_Repository.LoadAll();
            List<SubSystemReporterType> subSystemReporterTypes = oqEntities.ToList();

            foreach (SubSystemReporterType subSystemReporterType in subSystemReporterTypes)
            {
                s_CacheStorage.Add(GetKey(subSystemReporterType), subSystemReporterType);
            }
        }
    }
}
