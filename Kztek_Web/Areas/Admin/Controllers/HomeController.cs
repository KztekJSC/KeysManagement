using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kztek_Web.Models;
using Kztek_Service.Admin;
using Kztek_Library.Models;
using Kztek_Web.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Kztek_Library.Configs;
using Kztek_Library.Helpers;
using Microsoft.AspNetCore.SignalR;
using Kztek_Web.Hubs;
using System.Data.SqlClient;
using Kztek_Model.Models;
using Newtonsoft.Json;
using System.Data;

namespace Kztek_Web.Areas.Admin.Controllers
{
    [Area(AreaConfig.Admin)]
    public class HomeController : Controller
    {

        public HomeController()
        {
         
        }

        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Index()
        {
            //kiểm tra session xem có lưu ngôn ngữ không nếu có thì lấy không mặc định là "vi"
            string sessionValue = HttpContext.Session.GetString(SessionConfig.Kz_Language);
            if (string.IsNullOrWhiteSpace(sessionValue))
                sessionValue = HttpContext.Request.Cookies[CookieConfig.Kz_LanguageCookie];
            sessionValue = String.IsNullOrEmpty(sessionValue) ? "vi" : sessionValue;
            LanguageHelper.GetLang(sessionValue);



            return View();
        }

        public async Task<IActionResult> DashboardPartial(SelectListModel_Date model)
        {
          

            return PartialView();
        }

     
    }
}
