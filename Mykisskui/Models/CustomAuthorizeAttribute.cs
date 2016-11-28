using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 重写系统权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (null == filterContext) {
                throw new Exception("filter null");
            }
            var user = filterContext.HttpContext.Session["yujx"];

            if (null == user || string.Empty == user.ToString()) {
                //filterContext.HttpContext.Response.Redirect("/Admin/login",true);
                filterContext.HttpContext.Response.Write("<script>parent.location ='/Admin/login';</script>");
                filterContext.HttpContext.Response.End();
                return;
            }
        }
    }
}