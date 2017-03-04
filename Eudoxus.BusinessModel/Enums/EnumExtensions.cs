using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.BusinessModel.Resources;

namespace Eudoxus.BusinessModel
{
    public static class EnumExtensions
    {
        public static string GetLabel(this Enum enumeration)
        {
            string resourceKey = enumeration.GetType().Name + "_" + enumeration.ToString();
            string label = Labels.ResourceManager.GetString(resourceKey);
            return string.IsNullOrEmpty(label) ? enumeration.ToString() : label;
        }

        public static string GetAcronym(this Enum enumeration)
        {
            string resourceKey = enumeration.GetType().Name + "_" + enumeration.ToString() + "_Acronym";
            string label = Labels.ResourceManager.GetString(resourceKey);
            return string.IsNullOrEmpty(label) ? enumeration.ToString() : label;
        }

        public static string GetIcon(this Enum enumeration)
        {
            string resourceKey = enumeration.GetType().Name + "_" + enumeration.ToString() + "_Icon";
            string label = Labels.ResourceManager.GetString(resourceKey);
            return string.IsNullOrEmpty(label) ? enumeration.ToString() : label;
        }
    }
}
