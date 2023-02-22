using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace MP.Lib.LibUtility
{
    public class UserAgentConstraint : IRouteConstraint, IReadOnlySessionState
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return DeviceUtil.DetectMobileBrowser(); //DetectDesktopBrowser();
        }
    }
}