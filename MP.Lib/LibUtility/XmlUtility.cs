using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace MP.Lib.LibUtility
{
    public class LayoutInfo
    {
        public string style { get; set; }
        public string view { get; set; }
    }
    public class ListItemInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public int selected { get; set; }
        public string provinceCode { get; set; }
    }
    public class PriceRangeInfo
    {
        public int price { get; set; }
        public int step { get; set; }
    }
    public class XmlUtility
    {
        public static XmlUtility instance()
        {
            return new XmlUtility();
        }
        public static T GetLayOutSetting<T>(string configFileName)
        {
            if (string.IsNullOrWhiteSpace(configFileName))
                configFileName = "/LayoutConfig.xml";
            string path = HttpContext.Current.Server.MapPath(configFileName);
            using (StreamReader reader = new StreamReader(path))
            {
                XDocument docx = XDocument.Load(reader);
                //var ret1 = docx.Descendants("homeDealCategory").FirstOrDefault();
                //var ret2 = docx.Descendants("homeDealCategory").FirstOrDefault().Descendants("item");
                var ret = docx.Descendants("homeDealCategory").FirstOrDefault().Descendants("item").Select(p => new LayoutInfo
                {
                    style = p.Element("style").Value,
                    view = p.Element("view").Value
                });
                return ret.ToList().ToJson().ToObject<T>();
            }
        }
        public List<PriceRangeInfo> GetPriceRange(int catId)
        {
            string path = "dealCat/filter/priceRange";
            XElement elm = GetXElementByPath(path);
            List<XElement> elms = elm.Descendants("item").Where(t => (int)t.Attribute("catId") == catId).ToList();
            elms = elms.FirstOrDefault().Descendants("range").ToList();
            List<PriceRangeInfo> rets = elms.Select(p => new PriceRangeInfo { price = ConvertUtility.ToInt32(p.Value), step = (int)p.Attribute("step") }).ToList();
            return rets;
        }

        public static string GetDropDownItemJson(string nodeName)
        {
            //string configFileName = "/LayoutConfig.xml";
            //string path = HttpContext.Current.Server.MapPath(configFileName);
            //using (StreamReader reader = new StreamReader(path))
            //{
            //    XDocument docx = XDocument.Load(reader);
            //    var ret = docx.Descendants(nodeName).FirstOrDefault().Descendants("item").Select(p => new ListItemInfo
            //    {
            //        id = ConvertUtility.ToInt32(p.Element("id").Value),
            //        name = p.Element("name").Value,
            //        selected = ConvertUtility.ToInt32(p.Element("selected").Value),
            //        provinceCode = p.Element("provinceCode").Value
            //    });

            //    return ret.ToList().ToJson();
            //}

            List<ListItemInfo> lst = new List<ListItemInfo>();
            lst.Add(new ListItemInfo()
            {
                id = 24,
                name = "Hà Nội",
                selected = 1,
                provinceCode = "ha-noi"
            });
            lst.Add(new ListItemInfo()
            {
                id = 25,
                name = "TP.HCM",
                selected = 0,
                provinceCode = "ho-chi-minh"
            });
            lst.Add(new ListItemInfo()
            {
                id = 61,
                name = "Đà Nẵng",
                selected = 0,
                provinceCode = "da-nang"
            });
            return lst.ToList().ToJson();

        }

        public static string GetCategoryLayout(string nodeName, int catlevel)
        {

            string configFileName = "/LayoutConfig.xml";
            string path = HttpContext.Current.Server.MapPath(configFileName);
            using (StreamReader reader = new StreamReader(path))
            {
                XDocument docx = XDocument.Load(reader);
                var ret = docx.Descendants(nodeName).FirstOrDefault().Descendants("item").Where(x => x.Descendants("catid").FirstOrDefault().Value == catlevel.ToString()).FirstOrDefault();
                if (ret != null)
                {
                    return ret.Descendants("layout").FirstOrDefault().Value;
                }
                else
                {
                    return "category_default";
                }
            }
        }

        public XElement GetXElementByPath(string nodePath)
        {
            string configFileName = "/LayoutConfig.xml";
            string path = HttpContext.Current.Server.MapPath(configFileName);
            string[] paths = nodePath.Trim('/').Split('/');
            using (StreamReader reader = new StreamReader(path))
            {
                XDocument docx = XDocument.Load(reader);
                XElement elm = null;
                foreach (string str in paths)
                {
                    if (elm == null)
                    {
                        elm = docx.Descendants(str).FirstOrDefault();
                    }
                    else
                    {
                        elm = elm.Descendants(str).FirstOrDefault();
                    }
                }
                return elm;
            }
        }
        public static string GetSetting(string nodePath)
        {

            string configFileName = "/LayoutConfig.xml";
            string path = HttpContext.Current.Server.MapPath(configFileName);
            string[] paths = nodePath.Trim('/').Split('/');
            using (StreamReader reader = new StreamReader(path))
            {
                XDocument docx = XDocument.Load(reader);
                XElement elm = null;
                foreach (string str in paths)
                {
                    if (elm == null)
                    {
                        elm = docx.Descendants(str).FirstOrDefault();
                    }
                    else
                    {
                        elm = elm.Descendants(str).FirstOrDefault();
                    }
                }
                if (elm == null)
                    return "";
                else
                    return elm.Value;
            }
        }
        public static List<object> getCategoryConfig()
        {
            string configFileName = "/LayoutConfig.xml";
            string nodeName = "CategoryHomeBooking";
            string path = HttpContext.Current.Server.MapPath(configFileName);
            using (StreamReader reader = new StreamReader(path))
            {
                XDocument docx = XDocument.Load(reader);
                var ret = docx.Descendants(nodeName).FirstOrDefault().Descendants("item").Select(p => new 
                {
                    categoryName = p.Element("categoryName").Value,
                    categoryId = ConvertUtility.ToInt32(p.Element("categoryId").Value),
                    eventId = ConvertUtility.ToInt32(p.Element("eventId").Value)
                });

                return ret.ToList<object>();
            }
        }
    }
}
