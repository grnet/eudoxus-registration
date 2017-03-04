using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class SubSystemCacheManager : DomainCacheManager<HelpDeskEntities, SubSystem, int>
    {
        public SubSystemCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<SubSystem> oqEntities = s_Repository.LoadAll();
            List<SubSystem> subSystems = oqEntities.ToList();

            foreach (SubSystem subSystem in subSystems)
            {
                s_CacheStorage.Add(GetKey(subSystem), subSystem);
            }
        }
    }
}
