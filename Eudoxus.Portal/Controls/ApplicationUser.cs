using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using System.Web.Security;

namespace Eudoxus.Portal.Controls
{
    public class ApplicationUser
    {
        public ApplicationUser(string username)
        {
            Username = username;

            var roles = Roles.GetRolesForUser(username).ToList();

            SubSystem = CacheManager.SubSystems.GetItems().Where(x => x.Role == roles[0]).FirstOrDefault();
        }

        #region [ Public Properties ]

        public string Username { get; set; }

        public SubSystem SubSystem { get; set; }

        public List<enReporterType> ReporterTypes
        {
            get
            {
                return CacheManager.SubSystemReporterTypes.GetItems().Where(x => x.SubSystem == SubSystem).Select(x => x.ReporterType).ToList();
            }
        }

        public List<IncidentType> IncidentTypes
        {
            get
            {
                return CacheManager.IncidentTypes.GetItems().Where(x => x.SubSystem == SubSystem).ToList();
            }
        }

        #endregion
    }
}