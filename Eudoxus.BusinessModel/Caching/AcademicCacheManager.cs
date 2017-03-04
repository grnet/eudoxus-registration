using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class AcademicCacheManager : DomainCacheManager<HelpDeskEntities, Academic, int>
    {
        public AcademicCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<Academic> oqEntities = s_Repository.LoadAll();
            List<Academic> Academics = oqEntities.ToList();

            foreach (Academic Academic in Academics)
            {
                s_CacheStorage.Add(GetKey(Academic), Academic);
            }
        }
    }
}
