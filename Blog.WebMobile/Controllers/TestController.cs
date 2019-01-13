using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Blog.Mvc;

namespace Blog.Controllers
{
    public class TestController:BaseController
    {
        [HttpGet]
        public IActionResult GetEncryptKey()
        {
            String str =
                "2f2c21e5f94c9797513b1a1b4c92dde48eaf7f858cf4abb8996e1c4a9a6931db5ae444c8efa0acfd2cac3c6b3634cf56fd90ed93049f1167555d0f48ce8caed9e3d569779025fa3ba63793fd22e1d8bff86353d1e898803758d211bce45fd052e33f622e75b5b8254dd1be6295eee4360c9de675bd473b9de54392283d237793";
            var dd= RSAHelper.DecryptFromJsEncrypt(str, GlobalConfig.rsaPrivateKey);
            
            return Content(dd);
        }

        [HttpGet]
        public IActionResult Encrypt()
        {
            return View();
        }

        public TestController(IOptions<AppSetting> appSetting) : base(appSetting)
        {
        }
    
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode([FromServices]VierificationCodeServices _vierificationCodeServices)
        {
            string code = "";
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}
