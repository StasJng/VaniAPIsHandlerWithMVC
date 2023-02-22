using VaniAPIsHandler.CacheController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace VaniAPIsHandler.App_Start
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString InlineScripts(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "script");
        }

        public static IHtmlString InlineStyles(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "style");
        }

        private static IHtmlString InlineBundle(this HtmlHelper htmlHelper, string bundleVirtualPath, string htmlTagName)
        {
            string htmlTag = string.Empty;
            try
            {
                htmlTag = StackExRedisUtil.GetCacheByKey<string>(CacheDefinition.OtherCache, "GetBundleCache", htmlTagName, bundleVirtualPath.Replace("~", string.Empty).Replace("/", string.Empty));
            }
            catch (Exception ex) {
            }

            if (string.IsNullOrEmpty(htmlTag))
            {
                string bundleContent = LoadBundleContent(htmlHelper.ViewContext.HttpContext, bundleVirtualPath);
                htmlTag = string.Format("<{0}>{1}</{0}>", htmlTagName, bundleContent);
                StackExRedisUtil.SetCacheByKeypermanent<string>(CacheDefinition.OtherCache,htmlTag, "GetBundleCache", htmlTagName, bundleVirtualPath.Replace("~", string.Empty).Replace("/", string.Empty));

            }
            return new HtmlString(htmlTag);
        }

        private static string LoadBundleContent(HttpContextBase httpContext, string bundleVirtualPath)
        {
            var bundleContext = new BundleContext(httpContext, BundleTable.Bundles, bundleVirtualPath);
            var bundle = BundleTable.Bundles.Single(b => b.Path == bundleVirtualPath);
            var bundleResponse = bundle.GenerateBundleResponse(bundleContext);

            return bundleResponse.Content;
        }
        
    }
}