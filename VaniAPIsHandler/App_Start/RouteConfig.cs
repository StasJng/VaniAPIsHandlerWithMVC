using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace VaniAPIsHandler.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region Ignore route
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("scripts/{*file}");
            #endregion
            //
            #region APIs Route
            routes.MapRoute("GetLocation", "api/v1/getLocation", new { controller = "Location", action = "GetListLocation" });
            #endregion
        }
    }
}