using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class ErrorController : Controller
    {
        //自定义错误处理器(过滤器)
        protected override void OnException(ExceptionContext filterContext)
        {

            if (!filterContext.ExceptionHandled)
            {
                filterContext.Result = new RedirectResult("~/Error.htm");
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }
    }
}
