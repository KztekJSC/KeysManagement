using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #region service
        private ICDKeyService _CDKeyService;
        private IAppService _AppService;
        private IProjectService _ProjectService;
        private ICustomerService _CustomerService;
        private IUserService _UserService;
        private IActiveKeyService _ActiveKeyService;
        private ItblSystemConfigService _tblSystemConfigService;
        public CDKeyController(ICDKeyService _CDKeyService, IAppService _AppService, IUserService _UserService, IActiveKeyService _ActiveKeyService, IProjectService _ProjectService, ICustomerService _CustomerService, ItblSystemConfigService _tblSystemConfigService)
        {
            this._CDKeyService = _CDKeyService;
            this._AppService = _AppService;
            this._UserService = _UserService;
            this._ActiveKeyService = _ActiveKeyService;
            this._ProjectService = _ProjectService;
            this._CustomerService = _CustomerService;
            this._tblSystemConfigService = _tblSystemConfigService;
        }
        #endregion

        #region Danh sách

        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Index(string key = "", int page = 1, string AreaCode = "", string selectedId = "")
        {
            var gridmodel = await _CDKeyService.GetPagingByFirst(key, page, 20);

            if(gridmodel.Data != null && gridmodel.Data.Count > 0)
            {
                var users = await _UserService.GetAll();
                var apps = await _AppService.GetAll();
                var projects = await GetProject();
                var customers = await _CustomerService.GetAll();

                foreach (var item in gridmodel.Data)
                {
                    var objUser = users.FirstOrDefault(n => n.Id == item.UserCreated);

                    item.UserCreated = objUser != null ? objUser.Username : "";

                    var objApp = apps.FirstOrDefault(n => n.Id == item.AppId);

                    item.AppId = objApp != null ? objApp.Name + " - " + objApp.Code : "";

                    var objProject = projects.Data.FirstOrDefault(n => n.ItemValue == item.ProjectId);

                    item.ProjectId = objProject != null ? objProject.ItemText : "";

                    var objCus = customers.FirstOrDefault(n => n.Id == item.CustomerId);

                    item.CustomerId = objCus != null ? string.Format("<span>{0} - {1}</span><p>{2}</p>", objCus.Name, objCus.Phone, objCus.Address) : "";
                }
            }

            ViewBag.keyValue = key;
            ViewBag.AuthValue = await AuthHelper.CheckAuthAction("CDKey", this.HttpContext);
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.SelectedIdValue = selectedId;


            return View(gridmodel);
        }
        #endregion

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

        public async Task<SelectListModel_Chosen> GetProject(string selecteds = "")
        {
            var list = new List<SelectListModel> { new SelectListModel { ItemValue = "", ItemText = "- Lựa chọn -" } };
            var lst = await _ProjectService.GetAll();
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    list.Add(new SelectListModel { ItemValue = item.Id, ItemText = item.Name });
                }
            }

            var a = new SelectListModel_Chosen
            {
                Placeholder = await LanguageHelper.GetLanguageText("STATICLIST:DEFAULT"),
                IdSelectList = "ProjectId",
                isMultiSelect = false,
                Selecteds = !string.IsNullOrEmpty(selecteds) ? selecteds : "",
                Data = list
            };

            return a;
        }

        public async Task<SelectListModel_Chosen> GetCustomer(string selecteds = "")
        {
            var list = new List<SelectListModel> { new SelectListModel {ItemValue = "", ItemText = "- Lựa chọn -" } };
            var lst = await _CustomerService.GetAll();
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    list.Add(new SelectListModel { ItemValue = item.Id, ItemText = item.Name + " - " + item.Phone });
                }
            }

            var a = new SelectListModel_Chosen
            {
                Placeholder = await LanguageHelper.GetLanguageText("STATICLIST:DEFAULT"),
                IdSelectList = "CustomerId",
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
            ViewBag.Project = await GetProject(model.ProjectId);
            ViewBag.Customer = await GetCustomer(model.CustomerId);

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
        public async Task<IActionResult> Update(CDKey model, string AreaCode = "", int page = 1, string key = "",string appId = "",string pId = "",string cId = "", string txtDate = "")
        {
            ViewBag.App = await GetApp(appId);
            ViewBag.Project = await GetProject(pId);
            ViewBag.Customer = await GetCustomer(cId);
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
            oldObj.ProjectId = pId;
            oldObj.CustomerId = cId;

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

            ViewBag.Project = await GetProject("");

            ViewBag.Customer = await GetCustomer("");

            return PartialView();
        }

        public async Task<IActionResult> Save(int Quantity,string App,string ProjectId,string CustomerId)
        {
            var mes = new MessageReport(false, "Có lỗi xảy ra!");

            if (Quantity == 0)
            {
                mes = new MessageReport(false, "Vui lòng chọn số lượng!");

                return Json(mes);
            }

            var user = await SessionCookieHelper.CurrentUser(this.HttpContext);

            List<ActiveKey> activeKeys = new List<ActiveKey>();

            try
            {
                for (int i = 0; i < Quantity; i++)
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
                        ExpireDate = DateTime.Now.AddDays(7),
                        IsExpire = false,
                        CustomerId = CustomerId,
                        ProjectId = ProjectId
                    };

                    mes = await CreateKey(model);

                    if (mes.isSuccess)
                    {
                        var activeModel = new ActiveKey()
                        {
                            AppId = App,
                            CDKey = model.Code,
                            CustomerId = model.CustomerId,
                            DateCreated = DateTime.Now,
                            Id = Guid.NewGuid().ToString(),
                            IsDeleted = false,
                            KeyActive = "",
                            ProjectId = model.ProjectId,
                            UserCode = "",
                            UserCreated = user != null ? user.UserId : ""
                        };

                        await CreateActiveKey(activeModel);

                        //danh sách key active thành công
                        activeKeys.Add(activeModel);
                    }
                }

                mes = new MessageReport(true, "Thành công");

                //gửi mail
                var objSystem = await _tblSystemConfigService.GetDefault();

                if (!string.IsNullOrEmpty(objSystem.EmailTo))
                {
                    var body = await Body(activeKeys, objSystem.EmailSystem, user);

                    await MailHelper.SendMail("Kích hoạt key phần mềm", body, objSystem);
                }
            }
            catch (Exception ex)
            {
                mes = new MessageReport(false, ex.Message);
            } 

            return Json(mes);
        }

        async Task<string> Body(List<ActiveKey> activeKeys, string emailSystem,SessionModel user)
        {
            var _bodyHtml = new StringBuilder();

            if (activeKeys != null && activeKeys.Count > 0 && !string.IsNullOrEmpty(emailSystem))
            {
                #region header
                _bodyHtml.AppendLine("<h1> Danh sách key active </h1>");

                _bodyHtml.AppendLine(string.Format("<h4> Người tạo: <strong>{0}</strong> </h4>", user != null ? user.Name : ""));

                _bodyHtml.AppendLine("<div>");

                _bodyHtml.AppendLine("<table>");

                _bodyHtml.AppendLine("</table style='border: 1px solid #ccc;width:auto' cellspacing='0' cellpadding='3' border='0' align='left'>");

                _bodyHtml.AppendLine("<tr style='font-weight: bold; background-color: #d9d6d6;'>");

                _bodyHtml.AppendLine("<td style='color:#333333;border-right:1px solid #ccc;text-transform:none;font-family:Arial;font-size:12px;padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;border-top:1px solid #ccc;border-left:1px solid #ccc;border-bottom:1px solid #ccc' align='left'>");

                _bodyHtml.AppendLine("Phần mềm</td>");

                _bodyHtml.AppendLine("<td style='color:#333333;border-right:1px solid #ccc;text-transform:none;font-family:Arial;font-size:12px;padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;border-top:1px solid #ccc;border-bottom:1px solid #ccc' align='left'>");

                _bodyHtml.AppendLine("Dự án</td>");

                _bodyHtml.AppendLine("<td style='color:#333333;border-right:1px solid #ccc;text-transform:none;font-family:Arial;font-size:12px;padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;border-top:1px solid #ccc;border-bottom:1px solid #ccc' align='left'>");

                _bodyHtml.AppendLine("Khách hàng</td>");

                _bodyHtml.AppendLine("<td style='color:#333333;border-right:1px solid #ccc;text-transform:none;font-family:Arial;font-size:12px;padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;border-top:1px solid #ccc;border-bottom:1px solid #ccc' align='left'>");

                _bodyHtml.AppendLine("CDKey</td>");

                _bodyHtml.AppendLine("</tr>");
                #endregion

                var apps = await _AppService.GetAll();

                var pros = await _ProjectService.GetAll();

                var cuss = await _CustomerService.GetAll();

                foreach (var item in activeKeys)
                {
                    var objApp = apps.FirstOrDefault(n => n.Id == item.AppId);

                    item.AppId = objApp != null ? objApp.Name + " - " + objApp.Code : "";

                    var objPro = pros.FirstOrDefault(n => n.Id == item.ProjectId);

                    item.ProjectId = objPro != null ? objPro.Name : "";

                    var objCus = cuss.FirstOrDefault(n => n.Id == item.CustomerId);

                    item.CustomerId = objCus != null ? string.Format("<span>{0} - {1}</span><p>{2}</p>", objCus.Name, objCus.Phone, objCus.Address) : "";

                    _bodyHtml.AppendLine("<tr>");

                    _bodyHtml.AppendLine("<td style='border-right:1px solid #ccc;text-transform:uppercase;font-family:Arial;color:#333333;font-size:12px;padding-left:10px;padding-right:10px;border-bottom:1px solid #ccc;direction:ltr;padding-top:5px;padding-bottom:5px;border-left:1px solid #ccc'>");

                    _bodyHtml.AppendLine(string.Format("{0}", item.AppId));

                    _bodyHtml.AppendLine("</td>");

                    _bodyHtml.AppendLine("<td style='border-right:1px solid #ccc;text-transform:uppercase;font-family:Arial;color:#333333;font-size:12px;padding-left:10px;padding-right:10px;border-bottom:1px solid #ccc;direction:ltr;padding-top:5px;padding-bottom:5px'>");

                    _bodyHtml.AppendLine(string.Format("{0}", item.ProjectId));

                    _bodyHtml.AppendLine("</td>");

                    _bodyHtml.AppendLine("<td style='border-right:1px solid #ccc;text-transform:uppercase;font-family:Arial;color:#333333;font-size:12px;padding-left:10px;padding-right:10px;border-bottom:1px solid #ccc;direction:ltr;padding-top:5px;padding-bottom:5px'>");

                    _bodyHtml.AppendLine(string.Format("{0}", item.CustomerId));

                    _bodyHtml.AppendLine("</td>");

                    _bodyHtml.AppendLine("<td style='border-right:1px solid #ccc;text-transform:uppercase;font-family:Arial;color:#333333;font-size:12px;padding-left:10px;padding-right:10px;border-bottom:1px solid #ccc;direction:ltr;padding-top:5px;padding-bottom:5px'>");

                    _bodyHtml.AppendLine(string.Format("{0}", item.CDKey));

                    _bodyHtml.AppendLine("</td>");

                    _bodyHtml.AppendLine("</tr>");
                }



                _bodyHtml.AppendLine("</div>");
            }



            return _bodyHtml.ToString();
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

        public async Task<MessageReport> CreateActiveKey(ActiveKey model)
        {
            var mes = new MessageReport(false, "");

            mes = await _ActiveKeyService.Create(model);

            return mes;
        }
      
    }
}