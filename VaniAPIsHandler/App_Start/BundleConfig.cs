using System;
using System.Web;
using System.Web.Optimization;

namespace VaniAPIsHandler
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/mobilev2detailjs")
            //                .Include("~/Areas/MobileV2/asset/plugins/noUiSlider.9.2.0/nouislider.min.js")
            //                .Include("~/asset/plugins/handlebarjs/handlebars.min.js")
            //                .Include("~/asset/plugins/fancybox/source/jquery.fancybox.pack.js")
            //                .Include("~/asset/plugins/fancybox/source/helpers/jquery.fancybox-thumbs.js")
            //                .Include("~/asset/js/dealtoday.review.v2.js")
            //                );

            //BundleTable.EnableOptimizations = true;
        }

        public class CssRewriteUrlTransformWrapper : IItemTransform
        {
            public string Process(string includedVirtualPath, string input)
            {
                return new CssRewriteUrlTransform().Process(includedVirtualPath, input);
            }
        }
    }
}
