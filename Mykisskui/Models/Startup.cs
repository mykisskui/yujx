using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Mykisskui.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartupAttribute(typeof(Mykisskui.Models.Startup), "Configuration")]
namespace Mykisskui.Models
{
    public class Startup
    {
        public static void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }

        public static void Configuration(IAppBuilder app)
        {
          //  var UserID = new IUserId();
        //    GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => UserID);
            Models.Startup.ConfigureSignalR(app);
        }
    }
}