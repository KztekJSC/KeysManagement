using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kztek_Core.Models;
using Kztek_Library.Configs;
using Kztek_Library.Helpers;
using Kztek_Library.Models;
using Kztek_Model.Models;
using Kztek_Service.Admin;
using Kztek_Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Kztek_Web.Areas.Admin.Controllers
{
    [Area(AreaConfig.Admin)]
    public class CDKeyController : Controller
    {
        private ICDKeyService _CDKeyService;
        private IAppService _AppService;
        private IUserService _UserService;
        public CDKeyController(ICDKeyService _CDKeyService, IAppService _AppService, IUserService _UserService)
        {
            this._CDKeyService = _CDKeyService;
            this._AppService = _AppService;
            this._UserService = _UserService;
        }

        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Index(string key = "", int page = 1, string AreaCode = "", string selectedId = "")
        {
            var gridmodel = await _CDKeyService.GetPagingByFirst(key, page, 20);

            if(gridmodel.Data != null && gridmodel.Data.Count > 0)
            {
                var users = await _UserService.GetAll();
                var apps = await _AppService.GetAll();

                foreach(var item in gridmodel.Data)
                {
                    var objUser = users.FirstOrDefault(n => n.Id == item.UserCreated);

                    item.UserCreated = objUser != null ? objUser.Username : "";

                    var objApp = apps.FirstOrDefault(n => n.Id == item.AppId);

                    item.AppId = objApp != null ? objApp.Name + " - " + objApp.Code : "";
                }
            }

            ViewBag.keyValue = key;
            ViewBag.AuthValue = await AuthHelper.CheckAuthAction("CDKey", this.HttpContext);
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.SelectedIdValue = selectedId;


            return View(gridmodel);
        }

        #region DDL   

        public async Task<SelectListModel_Chosen> GetApp(string selecteds = "")
        {
            var list = new List<SelectListModel> { };
            var lst = await _AppService.GetAll();
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    list.Add(new SelectListModel { ItemValue = item.Id, ItemText = item.Name + " - " + item.Code});
                }
            }

            var a = new SelectListModel_Chosen
            {
                Placeholder = await LanguageHelper.GetLanguageText("STATICLIST:DEFAULT"),
                IdSelectList = "AppId",
                isMultiSelect = false,
                Selecteds = !string.IsNullOrEmpty(selecteds) ? selecteds : "",
                Data = list
            };

            return a;
        }
        #endregion

        #region Thêm mới
        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpGet]
        public async Task<IActionResult> Create(CDKey model, string AreaCode = "", string key = "")
        {

            model = model == null ? new CDKey() : model;
            model.ExpireDate = DateTime.Now;
            model.IsExpire = false;
            model.Active = true;
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.App = await GetApp(model.AppId);

            return await Task.FromResult(View(model));
        }

        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(CDKey model, string key = "", bool SaveAndCountinue = false, string AreaCode = "", string txtDate = "")
        {
            model.Active = false;
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.App = await GetApp(model.AppId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await SessionCookieHelper.CurrentUser(this.HttpContext);

            model.Id = Guid.NewGuid().ToString();
            model.DateCreated = DateTime.Now;
            model.IsDeleted = false;
            model.UserCreated = user != null ? user.UserId : "";
            model.ExpireDate = !string.IsNullOrEmpty(txtDate) ? Convert.ToDateTime(txtDate) : DateTime.Now;
            //Thực hiện thêm mới
            var result = await _CDKeyService.Create(model);
            if (result.isSuccess)
            {
                if (SaveAndCountinue)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToAction("Create", new { AreaCode = AreaCode, key = key, selectedId = model.Id });
                }

                return RedirectToAction("Index", new { AreaCode = AreaCode, key = key, selectedId = model.Id });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }
        #endregion

        #region Cập nhật

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <modified>
        /// Author              Date            Comments
        /// TrungNQ             01/09/2017      Tạo mới
        /// </modified>
        /// <param name="id">Id bản ghi</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <returns></returns>
        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpGet]
        public async Task<IActionResult> Update(string id, string AreaCode = "", int page = 1, string key = "")
        {

            var model = await _CDKeyService.GetById(id);
            ViewBag.PN = page;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.keyValue = key;
            ViewBag.App = await GetApp(model.AppId);

            return View(model);
        }
        /// <summary>
        /// Thực hiện cập nhật
        /// </summary>
        /// <modified>
        /// Author              Date            Comments
        /// TrungNQ             01/09/2017      Tạo mới
        /// </modified>
        /// <param name="obj">Đối tượng</param>
        /// <param name="objId">Id bản ghi</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <returns></returns>
        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpPost]
        public async Task<IActionResult> Update(CDKey model, string AreaCode = "", int page = 1, string key = "",string appId = "", string txtDate = "")
        {
            ViewBag.App = await GetApp(appId);
            //
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.PN = page;

            //Kiểm tra

            var oldObj = await _CDKeyService.GetById(model.Id);
            if (oldObj == null)
            {
                ViewBag.Error = "Bản ghi không tồn tại";
                return View(model);
            }


            //
            if (!ModelState.IsValid)
            {
                return View(oldObj);
            }

            //Gán giá trị
            if (!oldObj.Active)
            {
                oldObj.AppId = appId;
            }

            oldObj.ExpireDate = !string.IsNullOrEmpty(txtDate) ? Convert.ToDateTime(txtDate) : DateTime.Now;
            oldObj.Active = model.Active;
            oldObj.IsExpire = model.IsExpire;

            var result = await _CDKeyService.Update(oldObj);
            if (result.isSuccess)
            {

                return RedirectToAction("Index", new { AreaCode = AreaCode, key = key, page = page, selectedId = model.Id });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }
        #endregion Cập nhật

        #region Xóa

        /// <summary>
        /// Xóa
        /// </summary>
        /// <modified>
        /// Author              Date            Comments
        /// TrungNQ             01/09/2017      Tạo mới
        /// </modified>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>

        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _CDKeyService.DeleteById(id);

            return Json(result);
        }

        #endregion Xóa

        #region Modal Create Key
        public async Task<IActionResult> Modal_CreateKey()
        {
            ViewBag.App = await GetApp("");
            return PartialView();
        }

        public async Task<IActionResult> Save(int Quantity,string App)
        {
            var mes = new MessageReport(false, "Có lỗi xảy ra!");

            var user = await SessionCookieHelper.CurrentUser(this.HttpContext);

            for (int i = 0; i< Quantity; i++)
            {
                var model = new CDKey
                {
                    Id = Guid.NewGuid().ToString(),
                    Active = true,
                    AppId = App,
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    UserCreated = user != null ? user.UserId : "",
                    Code = Guid.NewGuid().ToString(),
                    ExpireDate = DateTime.Now,
                    IsExpire = false
                };

                mes = await CreateKey(model);
            }

            return Json(mes);
        }

        public async Task<MessageReport> CreateKey(CDKey model)
        {
            var mes = new MessageReport(false, "");

            //kiểm tra code này đã có trong database chưa
            var obj = await _CDKeyService.GetByCode(model.Code);

            //chưa có thì tạo
            if (obj == null)
            {
                mes = await _CDKeyService.Create(model);

                return mes;
            }
            else
            {
                //đã có thì đổi code khác rồi kiểm tra lại
                model.Code = Guid.NewGuid().ToString();

                await CreateKey(model);
            }

            return mes;
        }
        #endregion

        public async Task<IActionResult> Download(string id)
        {
           

            return View();
        }
    }
}