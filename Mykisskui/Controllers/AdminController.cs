using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult login() {

            return View();
        }
        [HttpPost]
        public void login(MyAdmin admin) {

            admin = Configs.loginVerifiCation(admin);
            if (admin.Id == 0)
            {
                Response.Redirect("/Admin/login");
                Response.End();
            }
            else {
                Response.Redirect("/Admin/Index");
                Response.End();
                 //  Response.Write(string.Format("你登录成功了{0}密码{1}",(HttpContext.Session["yujx"] as MyAdmin).UserName,admin.pwd));
            }
            //判断用户名密码是否正确

        }
        /// <summary>
        /// 后台默认页面
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult defaultAction(){

            return View();
        }
        /// <summary>
        /// 评论管理列表
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult commentList() {
            return View();
        }
        /// <summary>
        /// 评论管理编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult commentEdit() {

            return View();
        }
        /// <summary>
        /// 导航管理
        /// </summary>
        /// <returns></returns>
       [CustomAuthorize]
        public ActionResult NaviList() {
            ViewBag.data = Configs.AdminNaviData();
            return View();
        }
        /// <summary>
        /// 导航添加(编辑)
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
       public ActionResult NaviAdd() {
           return View();
       }
        /// <summary>
        /// 隐藏
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public int enable(string id1,int id = 1) {
            int result = 0;
            try {
                  result = Configs.EnableUpdate(id1,id);
                }
            catch { }
            return result;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public int remove(string id1,int id= 1) { 
            int result = 0;
            try {
                switch (id1) { 
                    case "article":
                        result = Configs.DataRemove(id);
                        break;
                    case "navi":

                        break;
                }
            }
            catch {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 文章管理列表
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult articleList(int id = 1) {
            ViewBag.data = Configs.articleListData(0,id,0,false);
            return View();
        }
        /// <summary>
        /// 文章添加(编辑)
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult articleAdd(int id = 1) {
            article article = Configs.AdminArticleData(id);
            ViewBag.data = article;
            return View();
        }
        /// <summary>
        /// 文章添加(编辑)[post]
        /// </summary>
        /// <param name="article"></param>
        [CustomAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public void articleAdd(article article) {
            article = Configs.articleAddAndEdit(article);
            Response.Write(string.Format("操作成功,返回码{0}",article.Id));
        }
        
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="Filedata"></param>
        /// <param name="filetype"></param>
        /// <param name="Id"></param>
        [CustomAuthorize]
        public void upload(HttpPostedFileWrapper Filedata, string filetype, string Id)
        {
                Response.ContentType = "text/plain";
                string result = Base.UpLoadFile(filetype, Filedata, "44", Id, "/blog/images/");
                Response.Write(result);
        } 
    }
}
