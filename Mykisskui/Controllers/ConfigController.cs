using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class ConfigController : Controller
    {


      public  yujingxueEntities db = new yujingxueEntities();
        //
        // GET: /Config/
        /// <summary>
        /// 配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult game() {

            return View();
        }
    }
}
