using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;
using Imis.Domain;

namespace Eudoxus.Portal.DataSources
{
    public class Academics
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IEnumerable<Academic> GetAll()
        {
            return CacheManager.Academics.GetItems();
        }
    }
}
