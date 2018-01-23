using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using NLog;
using Blog.Common;

namespace Blog.Controllers
{
    public class BaseController:Controller
    {
        protected readonly IOptions<AppSetting> _appSetting;
        protected static Logger _baseLogger = LogManager.GetCurrentClassLogger();

        public BaseController(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Exception != null)
            //    return;
            base.OnActionExecuted(context);
        }
    }
}
