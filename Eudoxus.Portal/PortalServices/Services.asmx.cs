using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.ComponentModel;
using AjaxControlToolkit;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.PortalServices
{
    /// <summary>
    /// Summary description for Services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Services : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] GetPrefectures()
        {
            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            foreach (Prefecture p in CacheManager.Prefectures.GetItems())
            {
                values.Add(new CascadingDropDownNameValue(p.Name, p.ID.ToString()));
            }

            return values.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCities(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            int prefectureID;
            if (int.TryParse(knownCategoryValues.Split(':')[1].Replace(";", ""), out prefectureID) && prefectureID >= 0)
            {
                foreach (City c in CacheManager.Prefectures.Get(int.Parse(knownCategoryValues.Split(':')[1].Replace(";", ""))).Cities)
                {
                    values.Add(new CascadingDropDownNameValue(c.Name, c.ID.ToString()));
                }
            }

            return values.OrderBy(x => x.name).ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetIncidentTypes(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            int reporterTypeID;
            if (int.TryParse(knownCategoryValues.Split(':')[1].Replace(";", ""), out reporterTypeID) && reporterTypeID >= 0)
            {
                foreach (IncidentType it in CacheManager.ReporterIncidentTypes.GetItems().
                                                                  Where(x => x.ReporterType == (enReporterType)reporterTypeID).
                                                                  Select(x => x.IncidentType))
                {
                    values.Add(new CascadingDropDownNameValue(it.Name, it.ID.ToString()));
                }
            }

            return values.ToArray();
        }
    }
}