using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    //继承过滤器控制器
    public class BlogController : ErrorController
    {

        /// <summary>
        ///  首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.data = Configs.articleListData(1);
            return View();
        }
        /// <summary>
        /// 新闻列表
        ///        
        /// </summary>
        /// <returns></returns>
        public ActionResult NewList(int id = 1) {
            ViewBag.data = Configs.articleListData(0, id);
            return View();
        }
        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <returns></returns>
        public ActionResult New(int id = 1) {
            
            ViewBag.data = Configs.articleListData(2, 0,id).FirstOrDefault();
            return View();
        }
        /// <summary>
        /// 音乐模版
        /// </summary>
        /// <returns></returns>
        public ActionResult music() {

            return View();
        }
        /// <summary>
        /// 菜单-分布布局
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu() {
            return PartialView();
        }
        /// <summary>
        /// 右侧-分布布局
        /// </summary>
        /// <returns></returns>
        public ActionResult Right() {
            return PartialView();
        }
        public ActionResult message() {

            return View();
        }
        public ActionResult pg() {
            return View();
        }

    }
}
