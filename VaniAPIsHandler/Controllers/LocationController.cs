using MP.Lib.LibUtility;
using MP.Lib.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VaniAPIsHandler.Models.Location;
using VaniAPIsHandler.Services.LocationRepo;

namespace VaniAPIsHandler.Controllers
{
    public class LocationController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        [HttpPost]
        public int EgiftSendMail(string purpose, string name, string phone, string content)
        {
            SendMailAPI.SendSMTPMailWSSoapClient senderAPI = new SendMailAPI.SendSMTPMailWSSoapClient();
            try
            {
                var mail = "Yêu cầu:" + purpose + "<br/>" + "Tên khách hàng:" + name + "<br/>" + "Số điện thoại:" + phone + "<br/>" + "Nội dung:" + content;
                senderAPI.MultiSendSmtpMail("", "", string.Empty, "hotro@dealtoday.vn", "THƯ KHÁCH HÀNG DOANH NGHIỆP GỬI - " + DateTime.Now.ToString("dd/MM/yyyy HH:MM"), mail);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return -1;
            }
            return 0;
        }

        [HttpGet]
        public string GetListLocation()
        {
            List<Location> lstLocs = new List<Location>();
            LocationRepoController locsRepo = new LocationRepoController();
            lstLocs = locsRepo.getListLocation(1, 10);
            var jsonStr = new JavaScriptSerializer().Serialize(lstLocs);
            return jsonStr;
        }
    }
}
