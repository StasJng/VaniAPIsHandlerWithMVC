using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Funq;
using MP.Lib.LibUtility;
using VaniAPIsHandler.App_Start;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.Redis;
using WebMarkupMin.AspNet4.Common;

namespace VaniAPIsHandler
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //only for web
            //AreaRegistration.RegisterAllAreas();

            ModelBinderConfig.Register(ModelBinders.Binders);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // register Redis log
            if (ConfigUtils.GetAppSetting("log", false)) { LogManager.LogFactory = new ConsoleLogFactory(); }

            WebMarkupMinConfig.Configure(WebMarkupMinConfiguration.Instance);

            MvcHandler.DisableMvcResponseHeader = true;
        }

        private Container InitializeContainer()
        {
            Container container = new Container();
            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            return container;
        }

        protected void Session_Start()
        {
            //VaniAPIsHandler.ClientVal.ClientConfig.CurrentPortalId = 144;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //ApplyCookie();
        }
        private void ApplyCookie()
        {
            var _domain = ".dealtoday.vn";
            string _cookieUtmKey = "__utm_affilate";
            string value = Request.QueryString["utm-affilate"];
            if (!string.IsNullOrEmpty(value))
            {
                var _affCookie = new HttpCookie(_cookieUtmKey, value) { Path = "/", Expires = DateTime.Now.AddMonths(1) };
                Response.AppendCookie(_affCookie);
            }
        }
    }
}