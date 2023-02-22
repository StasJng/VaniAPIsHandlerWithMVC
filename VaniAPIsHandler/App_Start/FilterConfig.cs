using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;

namespace VaniAPIsHandler.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CompressContentAttribute());
            filters.Add(new MinifyHtmlAttribute());
            filters.Add(new MinifyXmlAttribute());
        }
    }
}