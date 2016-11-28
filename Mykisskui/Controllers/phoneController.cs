using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class phoneController : Controller
    {

        private Mykisskui.Models.Configs config = new Models.Configs();
        private yujingxueEntities db = new yujingxueEntities();

        //
        // GET: /phone/

        public ActionResult Index()
        {
            string result = config.ListData();
            ViewBag.result = result;
            ViewBag.navi = config.NaviList();//获取导航数据
            return View();
        }

    }
}
