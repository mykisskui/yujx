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
            //http://fanyi.baidu.com/transpage?query=http%3A%2F%2Fwww.yujx.org&from=zh&to=kor&source=url&render=1
            string result = PostAndGet.GetResponseString("http://fanyi.baidu.com/transpage?query=http%3A%2F%2Fwww.yujx.org&from=zh&to=kor&source=url&render=1");
            ViewBag.result = result;
            return View();
        }
        public ActionResult game() {

            return View();
        }

    }
}
