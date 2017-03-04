using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class PrefectureCacheManager : DomainCacheManager<HelpDeskEntities, Prefecture, int>
    {
        public PrefectureCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<Prefecture> oqEntities = s_Repository.LoadAll();
            List<Prefecture> Prefectures = oqEntities.ToList();

            foreach (Prefecture Prefecture in Prefectures)
            {
                s_CacheStorage.Add(GetKey(Prefecture), Prefecture);
            }
        }
    }
}
