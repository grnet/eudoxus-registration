using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class RegionCacheManager : DomainCacheManager<HelpDeskEntities, Region, int>
    {
        public RegionCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<Region> oqEntities = s_Repository.LoadAll();
            List<Region> Regions = oqEntities.ToList();

            foreach (Region Region in Regions)
            {
                s_CacheStorage.Add(GetKey(Region), Region);
            }
        }
    }
}
