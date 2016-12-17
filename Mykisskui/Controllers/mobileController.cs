using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class mobileController : Controller
    {
        //
        // GET: /mobile/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// PWA를 사용 하여 개발 test
        /// </summary>
        /// <returns></returns>
        public ActionResult socket() {

            return View();
        }

    }
}
