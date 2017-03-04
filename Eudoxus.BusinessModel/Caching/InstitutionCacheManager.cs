using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain.EF;
using System.Data.Objects;

namespace Eudoxus.BusinessModel
{
    public class InstitutionCacheManager : DomainCacheManager<HelpDeskEntities, Institution, int>
    {
        public InstitutionCacheManager()
        {
            if (s_CacheStorage.Values.Count == 0)
                Fill();
        }

        protected override void Fill()
        {
            ObjectQuery<Institution> oqEntities = s_Repository.LoadAll();
            List<Institution> Institutions = oqEntities.ToList();

            foreach (Institution Institution in Institutions)
            {
                s_CacheStorage.Add(GetKey(Institution), Institution);
            }
        }

        public List<Institution> GetInstitutions()
        {
            ObjectQuery<Institution> oqEntities = s_Repository.LoadAll();
            return oqEntities.ToList();
        }
    }
}
