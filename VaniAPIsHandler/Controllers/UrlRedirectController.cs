using MP.Lib.LibUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VaniAPIsHandler.Controllers
{
    public class UrlRedirectController : Controller
    {
        //
        // GET: /UrlRedirect/

        public ActionResult Index()
        {
            throw new HttpException(404, "PageNotFound");//RedirectTo NoFoundPage

        }
        public ActionResult Notfound()
        {
            return View();
        }
        public ActionResult Maintenance()
        {
            return View();
        }

        public ActionResult RedirectUrl()
        {
            string url = ConvertUtility.ToString(Request.QueryString["url"]);
            if (string.IsNullOrEmpty(url)) return View("Notfound");
            return Redirect(url);
        }
    }
}
