using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kztek_Library.Configs;
using Kztek_Library.Helpers;
using Kztek_Model.Models;
using Kztek_Service.Admin;
using Kztek_Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Kztek_Web.Areas.Admin.Controllers
{
    [Area(AreaConfig.Admin)]
    public class CustomerController : Controller
    {
        private ICustomerService _CustomerService;


        public CustomerController(ICustomerService _CustomerService)
        {
            this._CustomerService = _CustomerService;

        }

        [CheckSessionCookie(AreaConfig.Admin)]
        public async Task<IActionResult> Index(string key = "", int page = 1, string AreaCode = "", string selectedId = "")
        {
            var gridmodel = await _CustomerService.GetPagingByFirst(key, page, 20);

            ViewBag.keyValue = key;
            ViewBag.AuthValue = await AuthHelper.CheckAuthAction("Customer", this.HttpContext);
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.SelectedIdValue = selectedId;


            return View(gridmodel);
        }

        #region Thêm mới
        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpGet]
        public async Task<IActionResult> Create(Customer model, string AreaCode = "", string key = "", string gate = "")
        {

            model = model == null ? new Customer() : model;
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.gateValue = gate;
            return await Task.FromResult(View(model));
        }

        [CheckSessionCookie(AreaConfig.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(Customer model, string key = "", bool SaveAndCountinue = false, string AreaCode = "", string gate = "")
        {

            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.gateValue = gate;
            //
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Vui lòng nhập thông tin");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                ModelState.AddModelError("Phone", "Vui lòng nhập SĐT");
                return View(model);
            }

            var existed = await _CustomerService.GetByKey(model.Phone);
            if (existed != null)
            {
                ModelState.AddModelError("Phone", "SĐT đã được sử dụng");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                var valiMail = await MailHelper.ValidateMail(model.Email);

                if (valiMail.isSuccess)
                {
                    var existedMail = await _CustomerService.GetByKey("", model.Email);
                    if (existedMail != null)
                    {
                        ModelState.AddModelError("Email", "Email đã được sử dụng!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", valiMail.Message);
                    return View(model);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = Guid.NewGuid().ToString();
            model.DateCreated = DateTime.Now;
            model.IsDeleted = false;
            model.Phone = StringUtilHelper.RemoveSpecialAndSpace(model.Phone);
            model.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : "";


            //Thực hiện thêm mới
            var result = await _CustomerService.Create(model);
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

            var model = await _CustomerService.GetById(id);
            ViewBag.PN = page;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.keyValue = key;

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
        public async Task<IActionResult> Update(Customer model, string AreaCode = "", int page = 1, string key = "")
        {

            //
            ViewBag.keyValue = key;
            ViewBag.AreaCodeValue = AreaCode;
            ViewBag.PN = page;

            //Kiểm tra

            var oldObj = await _CustomerService.GetById(model.Id.ToString());
            if (oldObj == null)
            {
                ViewBag.Error = "Bản ghi không tồn tại";
                return View(model);
            }

            //
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Vui lòng nhập thông tin");
                return View(oldObj);
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                ModelState.AddModelError("Phone", "Vui lòng nhập SĐT");
                return View(model);
            }

            var existed = await _CustomerService.GetByKey(model.Phone);
            if (existed != null && existed.Id != oldObj.Id)
            {
                ModelState.AddModelError("Phone", "SĐT đã được sử dụng");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                var valiMail = await MailHelper.ValidateMail(model.Email);

                if (valiMail.isSuccess)
                {
                    var existedMail = await _CustomerService.GetByKey("", model.Email);
                    if (existedMail != null)
                    {
                        ModelState.AddModelError("Email", "Email đã được sử dụng!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", valiMail.Message);
                    return View(model);
                }
            }

            //
            if (!ModelState.IsValid)
            {
                return View(oldObj);
            }

            //Gán giá trị
            oldObj.Name = model.Name;
            oldObj.Address = model.Address;
            oldObj.Phone = StringUtilHelper.RemoveSpecialAndSpace(model.Phone);
            oldObj.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : "";

            var result = await _CustomerService.Update(oldObj);
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
            var result = await _CustomerService.DeleteById(id);

            return Json(result);
        }

        #endregion Xóa
    }
}