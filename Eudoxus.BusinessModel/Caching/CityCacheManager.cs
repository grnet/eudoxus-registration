using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class CityCacheManager : DomainCacheManager<HelpDeskEntities, City, int>
    {
        public CityCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<City> oqEntities = s_Repository.LoadAll();
            List<City> Citys = oqEntities.ToList();

            foreach (City City in Citys)
            {
                s_CacheStorage.Add(GetKey(City), City);
            }
        }
    }
}
