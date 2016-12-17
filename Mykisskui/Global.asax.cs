using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mykisskui
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            if (lastError != null)
            {
                var httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //ASP.NET的400与404错误不记录日志，并都以自定义404页面响应
                    var httpCode = httpError.GetHttpCode();
                    timeStamp.writelog(httpError.Message);
                    if (httpCode == 400 || httpCode == 404)
                    {
                        Response.StatusCode = 404;//在IIS中配置自定义404页面
                        Server.ClearError();
                        //Response.Redirect("/404.html");
                        return;
                    }
                }

                //对于路径错误不记录日志，并都以自定义404页面响应
                if (lastError.TargetSite.ReflectedType == typeof(System.IO.Path))
                {
                    Response.StatusCode = 404;
                    Server.ClearError();
                    return;
                }
                Response.StatusCode = 500;
                Server.ClearError();
            }
        }







    }
}