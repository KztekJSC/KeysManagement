using Kztek_Library.Helpers;
using Kztek_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kztek_Web.Components.ImageFPT
{
    public class ImageFPTViewComponent : ViewComponent
    {
        private IHttpContextAccessor HttpContextAccessor;

        public ImageFPTViewComponent(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(ImageFPTModel model)
        {

            if (model.Filename.Contains("bienso"))
            {
                model.Type = "HOAPHAT";
            }

            model.Image = await FunctionHelper.FtpImage(model.Filename);

            return View(await Task.FromResult(model));
        }
    }
}
