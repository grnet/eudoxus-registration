using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Eudoxus.Portal
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replace \r,\n with &lt;br /&gt;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CrLfToBr(this string s)
        {
            return s != null ? s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "<br />") : "";
        }

        public static string[] SplitLines(this string s)
        {
            return s.Replace("\r", "\n").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            return s.Trim();
        }
    }
}
