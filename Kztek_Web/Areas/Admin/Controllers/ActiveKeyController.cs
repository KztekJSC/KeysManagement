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
using Kztek_Security;
using Kztek_Service.Admin;
using Kztek_Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Kztek_Web.Areas.Admin.Controllers
{
    [Area(AreaConfig.Admin)]
    public class ActiveKeyController : Controller
    {
        private IActiveKeyService _ActiveKeyService;
        private ICDKeyService _CDKeyService;
        private IAppService _AppService;
        private IUserService _UserService;
        private IProjectService _ProjectService;
        private ICustomerService _CustomerService;
        public ActiveKeyController(IActiveKeyService _ActiveKeyService, IAppService _AppService, IUserService _UserService, IProjectService _ProjectService, ICustomerService _CustomerService, ICDKeyService _CDKeyService)
        {
            this._ActiveKeyService = _ActiveKeyService;
            this._AppService = _AppService;
            this._UserService = _UserService;
            this._ProjectService = _ProjectService;
            this._CustomerService = _CustomerService;
            this._CDKeyService = _CDKeyService;
        }

        #region DDL   
        public async Task<SelectListModel_Multi> GetApp(string selecteds = "")  //bind ServicePackageId to dropdownlist
        {
            var list = new List<SelectListModel> { };
            var lst = await _AppService.GetAll();
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    list.Add(new SelectListModel { ItemValue = item.Id, ItemText = item.Name + " - " + item.Code });
                }
            }

            var model = new SelectListModel_Multi
            {
                Placeholder = await LanguageHelper.GetLanguageText("STATICLIST:DEFAULT"),
                IdSelectList = "ddlApp",
                Selecteds = !string.IsNullOrEmpty(selecteds) ? selecteds : "",
                Data = list
            };

            return model;
        }
        public async Task<SelectListModel_Chosen> GetProject(string selecteds = "")
        {
            var list = new List<SelectListModel> { };
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
            var list = new List<SelectListModel> { };
            var lst = await _CustomerService.GetAll();
            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    list.Add(new SelectListModel { ItemValue = item.Id, ItemText = string.Format("{0}{1}{2}", item.Name, !string.IsNullOrEmpty(item.Phone) ? " - " + item.Phone : "", !string.IsNullOrEmpty(item.Address) ? " - " + item.Address : "") });
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

        #region Danh sách
        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Index(string key = "", int page = 1, string AreaCode = "", string selectedId = "")
        {
            var gridmodel = await _ActiveKeyService.GetPagingByFirst(key, page, 20);

            if (gridmodel.Data != null && gridmodel.Data.Count > 0)
            {
                var users = await _UserService.GetAll();
                var apps = await _AppService.GetAll();
                var pros = await _ProjectService.GetAll();
                var cuss = await _CustomerService.GetAll();

                foreach (var item in gridmodel.Data)
                {
                    var objUser = users.FirstOrDefault(n => n.Id == item.UserCreated);

                    item.UserCreated = objUser != null ? objUser.Username : "";

                    var objApp = apps.FirstOrDefault(n => n.Id == item.AppId);

                    item.AppId = objApp != null ? objApp.Name + " - " + objApp.Code : "";

                    var objPro = pros.FirstOrDefault(n => n.Id == item.ProjectId);

                    item.ProjectId = objPro != null ? objPro.Name : "";

                    var objCus = cuss.FirstOrDefault(n => n.Id == item.CustomerId);

                    item.CustomerId = objCus != null ? string.Format("<p>{0}</p><p>{1}</p><p>{2}</p>", objCus.Name, objCus.Phone, objCus.Address) : "";
                }
            }

            ViewBag.keyValue = key;
            ViewBag.AuthValue = await AuthHelper.CheckAuthAction("ActiveKey", this.HttpContext);
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.SelectedIdValue = selectedId;


            return View(gridmodel);
        }
        #endregion

        #region Active
        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Active()
        {
            ViewBag.App = await GetApp();
            ViewBag.Project = await GetProject();
            ViewBag.Customer = await GetCustomer();
            return View();
        }


        public async Task<IActionResult> Partial_CDKey(string app, string codes)
        {
            var list = await _CDKeyService.GetByApp(app);

            if (list != null && list.Count > 0)
            {
                var apps = await _AppService.GetAll();

                foreach (var item in list)
                {
                    var objApp = apps.FirstOrDefault(n => n.Id == item.AppId);

                    item.AppId = objApp != null ? objApp.Name + " - " + objApp.Code : "";
                }
            }

            ViewBag.Codes = codes;

            return PartialView(list);
        }

        public async Task<IActionResult> Partial_ActiveKey(string codes)
        {
            var list = await _ActiveKeyService.GetByApp(codes);

            ViewBag.Codes = codes;

            return PartialView(list);
        }

        public async Task<IActionResult> RemoveChoose(string strCode, string code)
        {
            if (!string.IsNullOrEmpty(strCode) && !string.IsNullOrEmpty(code))
            {
                var arr = strCode.Split(',');
                arr = arr.Where(n => n != code).ToArray();
                strCode = string.Join(",", arr);
            }

            return await Task.FromResult(Json(strCode));
        }

        public async Task<IActionResult> Save(ActiveKey model)
        {
            var mes = new MessageReport(false, "Có lỗi xảy ra!");

            if (string.IsNullOrEmpty(model.CDKey))
            {
                mes = new MessageReport(false, "Vui lòng chọn key cần active!");
                return await Task.FromResult(Json(mes));
            }

            if (string.IsNullOrEmpty(model.UserCode))
            {
                mes = new MessageReport(false, "Vui lòng chọn nhập UserCode!");
                return await Task.FromResult(Json(mes));
            }

            try
            {
                //lấy danh sách cd key
                var cdkeys = await _CDKeyService.GetByKeys(model.CDKey);

                if (cdkeys != null && cdkeys.Count() > 0)
                {
                    var user = await SessionCookieHelper.CurrentUser(this.HttpContext);

                    foreach (var item in cdkeys)
                    {
                        //tạo sự kiện active
                        var modelActive = new ActiveKey
                        {
                            Id = Guid.NewGuid().ToString(),
                            AppId = item.AppId,
                            CDKey = item.Code,
                            CustomerId = model.CustomerId,
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            ProjectId = model.ProjectId,
                            UserCode = model.UserCode,
                            UserCreated = user != null ? user.UserId : "",
                            KeyActive = "1234"
                        };

                        mes = await _ActiveKeyService.Create(modelActive);

                        if (mes.isSuccess)
                        {
                            //update trạng thái cd key đã kích hoạt
                            item.Active = true;
                            await _CDKeyService.Update(item);
                        }
                    }

                    mes = new MessageReport(true, "Active thành công!");
                }
            }
            catch (Exception ex)
            {
                mes = new MessageReport(false, ex.Message);
                return await Task.FromResult(Json(mes));
            }

            return await Task.FromResult(Json(mes));
        }
        #endregion

        #region Nhập UserCode
        public async Task<IActionResult> Modal_UserCode(string id)
        {
            ViewBag.Id = id;
            return await Task.FromResult(PartialView());
        }

        public async Task<IActionResult> SaveUserCode(ActiveKey model)
        {
            var mes = await SaveCode(model);

            return await Task.FromResult(Json(mes));
        }

        async Task<MessageReport> SaveCode(ActiveKey model)
        {
            var mes = new MessageReport(false, "Có lỗi xảy ra!");

            if (string.IsNullOrEmpty(model.UserCode))
            {
                mes = new MessageReport(false, "Vui lòng chọn nhập UserCode!");
                return mes;
            }

            try
            {
                //lấy danh sách cd key
                var obj = await _ActiveKeyService.GetById(model.Id);

                if (obj != null)
                {
                    //Lấy thông tin CDKEY
                    var objCdKey = await _CDKeyService.GetByCode(obj.CDKey);

                    if (objCdKey != null)
                    {
                        //Check usercode hợp lệ
                        var activeCode = await GetActiveCode(model.UserCode, objCdKey);

                        if (!string.IsNullOrWhiteSpace(activeCode))
                        {
                            obj.KeyActive = activeCode;
                            obj.UserCode = model.UserCode;
                            mes = await _ActiveKeyService.Update(obj);
                        }
                        else
                        {
                            mes = new MessageReport(false, "Usercode không hợp lệ");
                        }
                    }
                }
                else
                {
                    mes = new MessageReport(false, "Bản ghi ActiveKey không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                mes = new MessageReport(false, ex.Message);
                return mes;
            }

            return mes;
        }

        public async Task<IActionResult> SaveUserCodeAndDownload(ActiveKey model)
        {
            var mes = await SaveCode(model);

            //nếu lưu usercode  thành công thì download
            if (mes.isSuccess)
            {

            }

            return View();
        }
        public async Task<string> GetActiveCode(string reqStr, CDKey cdkey)
        {
            string responseStr = string.Empty;
            try
            {
                LicenseRequest decryptedReq = LicenseGenerator.ReadUserCode(reqStr);

                var info = new LicenseInfo()
                {
                    CD_KEY = decryptedReq.CD_KEY,
                    ExpireDate = cdkey.ExpireDate,
                    IsExpire = cdkey.IsExpire,
                    ProjectName = ""
                };

                responseStr = LicenseGenerator.CreateActiveKey(decryptedReq, info);
            }
            catch { }

            return await Task.FromResult(responseStr);
        }

        public async Task<IActionResult> Download(string id)
        {
            var activeKey = await _ActiveKeyService.GetById(id);

            if (activeKey != null)
            {
                byte[] fileBytes = Encoding.UTF8.GetBytes(activeKey.KeyActive);
                string fileName = "license.dat";
                return await Task.FromResult(File(fileBytes, System.Net.Mime.MediaTypeNames.Text.Plain, fileName));
            }
            else
            {
                return await Task.FromResult(new EmptyResult());
            }
        }
        #endregion

    }
}