using System.Collections.Generic;

using WebMarkupMin.AspNet.Common;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNet.Common.UrlMatchers;
using WebMarkupMin.AspNet4.Common;
using WebMarkupMin.Core;

namespace VaniAPIsHandler.App_Start
{
    public class WebMarkupMinConfig
    {
        public static void Configure(WebMarkupMinConfiguration configuration)
        {
            try
            {
                configuration.AllowMinificationInDebugMode = true;
                configuration.AllowCompressionInDebugMode = true;

                IHtmlMinificationManager htmlMinificationManager =
                    HtmlMinificationManager.Current;
                htmlMinificationManager.ExcludedPages = new List<IUrlMatcher>
                {
                    new WildcardUrlMatcher("/minifiers/x*ml-minifier"),
                    new ExactUrlMatcher("/contact")
                };
                HtmlMinificationSettings htmlMinificationSettings =
                    htmlMinificationManager.MinificationSettings;
                htmlMinificationSettings.RemoveRedundantAttributes = false;
                htmlMinificationSettings.RemoveHttpProtocolFromAttributes = false;
                htmlMinificationSettings.RemoveHttpsProtocolFromAttributes = false;
                htmlMinificationSettings.MinifyInlineJsCode = false;
                htmlMinificationSettings.RemoveCdataSectionsFromScriptsAndStyles = false;
                //htmlMinificationManager.CssMinifierFactory =
                //    new MsAjaxCssMinifierFactory();
                //htmlMinificationManager.JsMinifierFactory =
                //    new MsAjaxJsMinifierFactory();

                IXhtmlMinificationManager xhtmlMinificationManager =
                    XhtmlMinificationManager.Current;
                xhtmlMinificationManager.IncludedPages = new List<IUrlMatcher>
                {
                    new WildcardUrlMatcher("/minifiers/x*ml-minifier"),
                    new ExactUrlMatcher("/contact")
                };
                XhtmlMinificationSettings xhtmlMinificationSettings =
                    xhtmlMinificationManager.MinificationSettings;
                xhtmlMinificationSettings.RemoveRedundantAttributes = false;
                xhtmlMinificationSettings.RemoveHttpProtocolFromAttributes = true;
                xhtmlMinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                //xhtmlMinificationManager.CssMinifierFactory =
                //    new YuiCssMinifierFactory();
                //xhtmlMinificationManager.JsMinifierFactory =
                //    new YuiJsMinifierFactory();

                IXmlMinificationManager xmlMinificationManager =
                    XmlMinificationManager.Current;
                XmlMinificationSettings xmlMinificationSettings =
                    xmlMinificationManager.MinificationSettings;
                xmlMinificationSettings.CollapseTagsWithoutContent = true;

                IHttpCompressionManager httpCompressionManager =
                    HttpCompressionManager.Current;
                httpCompressionManager.CompressorFactories = new List<ICompressorFactory>
                {
                    new DeflateCompressorFactory(),
                    new GZipCompressorFactory()
                };
            }
            catch (System.Exception)
            {
                return;
            }
        }
    }
}