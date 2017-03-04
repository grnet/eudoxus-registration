using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal
{
    public class CacheManager
    {
        private static object s_Lock = new object();
        private static SubSystemCacheManager s_SubSystemCacheManager;
        private static IncidentTypeCacheManager s_IncidentTypeCacheManager;
        private static SubSystemReporterTypeCacheManager s_SubSystemReporterTypeCacheManager;
        private static ReporterIncidentTypeCacheManager s_ReporterIncidentTypeCacheManager;
        private static RegionCacheManager s_RegionCacheManager;
        private static PrefectureCacheManager s_PrefectureCacheManager;
        private static CityCacheManager s_CityCacheManager;
        private static AcademicCacheManager s_AcademicCacheManager;
        private static InstitutionCacheManager s_InstitutionCacheManager;

        static CacheManager()
        {
            SubSystems.GetItems();
            IncidentTypes.GetItems();
            SubSystemReporterTypes.GetItems();
            ReporterIncidentTypes.GetItems();
            Regions.GetItems();
            Prefectures.GetItems();
            Cities.GetItems();
            Academics.GetItems();
            Institutions.GetItems();
        }

        public static SubSystemCacheManager SubSystems
        {
            get
            {
                if (s_SubSystemCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_SubSystemCacheManager == null)
                            s_SubSystemCacheManager = new SubSystemCacheManager();
                    }
                return s_SubSystemCacheManager;
            }
        }

        public static IncidentTypeCacheManager IncidentTypes
        {
            get
            {
                if (s_IncidentTypeCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_IncidentTypeCacheManager == null)
                            s_IncidentTypeCacheManager = new IncidentTypeCacheManager();
                    }
                return s_IncidentTypeCacheManager;
            }
        }

        public static SubSystemReporterTypeCacheManager SubSystemReporterTypes
        {
            get
            {
                if (s_SubSystemReporterTypeCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_SubSystemReporterTypeCacheManager == null)
                            s_SubSystemReporterTypeCacheManager = new SubSystemReporterTypeCacheManager();
                    }
                return s_SubSystemReporterTypeCacheManager;
            }
        }

        public static ReporterIncidentTypeCacheManager ReporterIncidentTypes
        {
            get
            {
                if (s_ReporterIncidentTypeCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_ReporterIncidentTypeCacheManager == null)
                            s_ReporterIncidentTypeCacheManager = new ReporterIncidentTypeCacheManager();
                    }
                return s_ReporterIncidentTypeCacheManager;
            }
        }

        public static RegionCacheManager Regions
        {
            get
            {
                if (s_RegionCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_RegionCacheManager == null)
                            s_RegionCacheManager = new RegionCacheManager();
                    }
                return s_RegionCacheManager;
            }
        }

        public static PrefectureCacheManager Prefectures
        {
            get
            {
                if (s_PrefectureCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_PrefectureCacheManager == null)
                            s_PrefectureCacheManager = new PrefectureCacheManager();
                    }
                return s_PrefectureCacheManager;
            }
        }

        public static CityCacheManager Cities
        {
            get
            {
                if (s_CityCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_CityCacheManager == null)
                            s_CityCacheManager = new CityCacheManager();
                    }
                return s_CityCacheManager;
            }
        }

        public static AcademicCacheManager Academics
        {
            get
            {
                if (s_AcademicCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_AcademicCacheManager == null)
                            s_AcademicCacheManager = new AcademicCacheManager();
                    }
                return s_AcademicCacheManager;
            }
        }

        public static InstitutionCacheManager Institutions
        {
            get
            {
                if (s_InstitutionCacheManager == null)
                    lock (s_Lock)
                    {
                        if (s_InstitutionCacheManager == null)
                            s_InstitutionCacheManager = new InstitutionCacheManager();
                    }
                return s_InstitutionCacheManager;
            }
        }

        public static void Refresh()
        {
            s_SubSystemCacheManager.Refresh();
            s_IncidentTypeCacheManager.Refresh();
            s_SubSystemReporterTypeCacheManager.Refresh();
            s_ReporterIncidentTypeCacheManager.Refresh();
            s_RegionCacheManager.Refresh();
            s_PrefectureCacheManager.Refresh();
            s_CityCacheManager.Refresh();
            s_AcademicCacheManager.Refresh();
            s_InstitutionCacheManager.Refresh();
        }
    }
}
