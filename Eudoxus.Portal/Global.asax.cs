using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Eudoxus.BusinessModel;
using System.Text.RegularExpressions;
using Eudoxus.Utils;

namespace Eudoxus.Portal
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            log4net.Config.XmlConfigurator.Configure();

            if (Config.UseEService)
                QueueWorker.Inititalize();

        }

        //protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        //{
        //    if (!Request.IsSecureConnection && !Request.Url.ToString().ToLower().Contains("localhost"))
        //    {                
        //        Response.Redirect(Request.Url.ToString().Replace("http://","https://"), true);
        //    }
        //}

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
                LogHelper.LogError(e.ExceptionObject as Exception, "Global");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (HttpContext.Current == null)
            {
                LogHelper.LogError(new Exception("Unknown Error"), "Global");
            }
            else
            {
                if (HttpContext.Current.Error != null)
                    LogHelper.LogError(HttpContext.Current.Error, "Global");
                else
                    LogHelper.LogError(new Exception("Unknown error"), "Global");
            }
        }
    }
}