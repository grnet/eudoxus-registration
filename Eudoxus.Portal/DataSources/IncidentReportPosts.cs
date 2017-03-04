using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.DataSources
{
    public class IncidentReportPosts
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<IncidentReportPost> FindByIncidentReportID(int incidentReportID)
        {
            var incidentReportPosts = new IncidentReportPostRepository().FindByIncidentReportID(incidentReportID);

            return incidentReportPosts;
        }
    }
}