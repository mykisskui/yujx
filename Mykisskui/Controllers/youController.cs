using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mykisskui.Controllers
{
    public class youController : Controller
    {

        private Mykisskui.Models.Configs config = new Models.Configs();
        private yujingxueEntities db = new yujingxueEntities();

        //
        // GET: /you/

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            //if (!Configs.userAgent())
            //{
            //    return RedirectToAction("Index","phone");
            //}
            //else
            //{
            //    string result = config.ListData();
            //    ViewBag.result = result;
            //    ViewBag.navi = config.NaviList();//获取导航数据
            //    return View();
            //}
            return View();
        }
        public ActionResult details(int id=0) {
            string result = config.ListData(id);
            ViewBag.result = result;
            return View();
        }
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Admin(string admin) {
            try {

                if(admin == "yjx")
                {
                    return View();
                }
            }
            catch {
            }
            return Content(string.Format("打的啥玩意，滚犊子"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [ValidateInput(false)]
        [HttpPost]
        public string Admin(article art) {
            string result = string.Empty;
            //try {
            //    art.Time = DateTime.Now;
            //    art.type = 1;
            //    art.read = 0;
            //    art.top = 0;
            //    art.tread = 0;
            //    art.Enable = true;
            //    db.article.Add(art);

            //    result = db.SaveChanges().ToString();
            //}
            //catch {
            //    result = -1+"";
            //}
            return result;

        }
        public string ListAdd(string title,string zuozhe,string biaoqian,string txt) {
            return string.Empty;
        }
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <returns></returns>
        public string ListPost(int id)
        {
            Configs config = new Configs();
            string result = config.ListData(id);
            return result;
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public string search(string k) {
            string result = string.Empty;
            IEnumerable<article> art = null;
          
            JavaScriptSerializer js = new JavaScriptSerializer();
            try {
                if (!string.IsNullOrEmpty(k.Trim()))
                {
                    art = db.article.Where(f => f.Title.Contains(k));

                    foreach (var item in art) {
                        item.Text = HttpUtility.UrlEncode(item.Text);
                    }
                    result = js.Serialize(art);
                }
      
            }
            catch {
                result = string.Empty;
            }
            return result;
        }
        /// <summary>
        /// 读取音乐列表
        /// </summary>
        /// <returns></returns>
        public string MusicList() {
            string result = string.Empty;
            IEnumerable<Music> music = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                music = db.Music;
                List<Model.Music> ModelMusics = new List<Model.Music>();
                foreach (var item in music)
                {

                    Model.Music ModelMusic = new Model.Music();
                    ModelMusic.title = item.Name;
                    ModelMusic.artist = item.Author;
                    ModelMusic.album = item.Author;
                    ModelMusic.cover = item.ImageURL;
                    ModelMusic.mp3 = item.URL;
                    ModelMusic.ogg = string.Empty;
                    ModelMusics.Add(ModelMusic);
                }
                result = js.Serialize(ModelMusics);
            }
            catch {
                result = string.Format("{0}", -1);
                }
            return HttpUtility.UrlEncode(result);
        }
        /// <summary>
        /// 读取书签列表
        /// </summary>
        /// <returns></returns>
        public string BookMark(){
            string result = string.Empty;
            IEnumerable<Bookmark> books = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try {
                books = db.Bookmark;
                result = js.Serialize(books);
            }
            catch {
                result = string.Format("{0}", -1);
            }
            return result;
        }
        public ActionResult test() {
            return View();
        }

    }
}
