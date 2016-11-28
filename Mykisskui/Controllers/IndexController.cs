using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class IndexController : Controller
    {

        yujingxueEntities db = new yujingxueEntities();
        //
        // GET: /Index/

        /// <summary>
        /// 默认首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try {
            }
            catch (Exception e)
            {
            }
            return View();
        }
        /// <summary>
        /// 个人
        /// </summary>
        /// <returns></returns>
        public ActionResult Indexs()
        {  
            return View();
        }  
        /// <summary>  
        /// 列表展示
        /// </summary>   
        /// <returns></returns>
        public ActionResult YJX(string id)
        {
            int i = 0;
            try {
                i = int.Parse(id);
            }
            catch { 
                
            }
            ViewBag.id = i;
            return View();
        }

      

    }
}
