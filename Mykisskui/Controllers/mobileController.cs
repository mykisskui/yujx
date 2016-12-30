using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mykisskui.Controllers
{
    public class mobileController : Controller
    {
        private JavaScriptSerializer js = new JavaScriptSerializer();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 모바일 뉴스 list
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsList(int id = 0) {
            article art = mobileDetails(id);
            return View(art);
        }
        /// <summary>
        /// PWA를 사용 하여 개발 test
        /// </summary>
        /// <returns></returns>
        public ActionResult socket() {

            return View();
        }
        /// <summary>
        /// 输出首页数据集
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string mobileIndex() {
            string result = string.Empty;
            IEnumerable<article> art = Configs.articleListData(3);
            //一共20条数据
            //前10条为 top 
            //后10条为 new
            StringBuilder sb = new StringBuilder();
            List<article> article = null;
            article = art.Take(10).ToList();
            sb.Append("[");
            sb.Append("{");
            sb.Append("\"top\":");
            sb.Append("[");
            foreach (article item in art.Take(10)) {
                sb.Append("{");
                sb.AppendFormat("\"id\":{0},", item.Id);
                sb.AppendFormat("\"name\":\"{0}\",", item.Title);
                sb.AppendFormat("\"time\":\"{0}\"", item.Time.Value.ToString("yyyy-MM-dd hh:mm"));
                sb.Append("},");
            }
            sb.Append("],");
            sb = sb.Replace("},]", "}]");
            sb.Append("\"new\":");
            sb.Append("[");
            foreach (article item in art.Skip(10))
            {
                sb.Append("{");
                sb.AppendFormat("\"id\":{0},", item.Id);
                sb.AppendFormat("\"name\":\"{0}\",", item.Title);
                sb.AppendFormat("\"time\":\"{0}\"", item.Time.Value.ToString("yyyy-MM-dd hh:mm"));
                sb.Append("},");
            }
            sb.Append("]");
            sb = sb.Replace("},]", "}]");
            sb.Append("}");
            sb.Append("]");
            result = sb.ToString();
            return result;
        }
        /// <summary>
        /// 返回对应编号的数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public article mobileDetails(int data = 0) {
            string result = string.Empty;
            IEnumerable<article> art = Configs.articleListData(2,0,data);
            return art.First();
        }
        /// <summary>
        /// 输出输出列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string mobileList(int pageIndex= 1) {
            IEnumerable<article> art =  Configs.articleListData(0, pageIndex);
            string result = js.Serialize(art);
            return result;
        }
    }
}
