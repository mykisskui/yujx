using Mykisskui.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Mykisskui.Models
{
    public class Configs
    {

        private JavaScriptSerializer js = new JavaScriptSerializer();
        yujingxueEntities db = new yujingxueEntities();
        /// <summary>
        /// 文章数据获取
        /// </summary>
        /// <param name="type">
        /// 0:获取文章列表(分页)
        /// 1:获取首页文章列表(根据Read排序)
        /// 2:获取文章详情(获取一次增加浏览量)
        /// </param>
        /// <param name="id">编号</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="enable">是否根据enable查询</param>
        /// <returns></returns>
        public static IEnumerable<article> articleListData(int type = 0,int pageindex = 0,int id = 0,bool enable = true ){
            IEnumerable<article> article = null;
            yujingxueEntities db = new yujingxueEntities();
            int pagesize = 10;
            try {
                if (enable)
                {
                    article = db.article.Where(f => f.Enable == true).OrderByDescending(f => f.Time);
                }
                else {
                    article = db.article.OrderByDescending(f => f.Time);
                    pagesize = 20;
                }
                switch (type) { 
                    case 0:
                        Model.NewListPageModel.PageCount = (article.Count() + pagesize - 1) / pagesize;
                        Model.NewListPageModel.PageIndex = pageindex;
                        foreach (article item in article) {
                            item.Text = Regex.Replace(item.Text, @"< */? *\w+[^>]*>|&nbsp;*", "");
                            item.Text = item.Text.Length > 280 ? item.Text.Substring(0, 280) : item.Text;
                    //       item.Text = Base.StripTagsCharArray(item.Text);
                          
                        }
                        article = article.Skip(pagesize * (pageindex - 1)).Take(pagesize).OrderByDescending(f=>f.Time);
                        break;
                    case 1:
                        article = article.OrderByDescending(f => f.read).Take(5);
                        foreach (article item in article) {
                            item.Text = Regex.Replace(item.Text, @"< */? *\w+[^>]*>|&nbsp;", "");
                            item.Text = item.Text.Length > 280 ? item.Text.Substring(0, 280) : item.Text;
                      //   item.Text = Base.StripTagsCharArray(item.Text);
              
                        }
                        break;
                    case 2:

                        List<article> art = TopDownsub(article.ToList(), id);
                        Model.NewDetailsTopDown.top = art[0];
                        Model.NewDetailsTopDown.down = art[1];
                        article = article.Where(f => f.Id == id);
                        article.First().read = article.First().read + 1;
                        db.SaveChanges();
                        break;
                }
            }
            catch { }

            return article;
        }
        /// <summary>
        /// 后台管理文章[article]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static article AdminArticleData(int id = 1) {
            article article = null;
            yujingxueEntities db = new yujingxueEntities();
            article = db.article.Where(f => f.Id == id).FirstOrDefault();
            if (article == null)
            {
                article = new article();
                article.Id = -1;
                article.Title = string.Empty;
                article.Text = string.Empty;
                article.author = string.Empty;
                article.read = 0;
                article.top = 0;
                article.tread = 0;
                article.key = string.Empty;
                article.type = 0;
            }
            return article; 
        }

        /// <summary>
        /// 后台导航管理
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public static IEnumerable<Navi> AdminNaviData() {
            IEnumerable<Navi> navi = null;
            yujingxueEntities db = new yujingxueEntities();
            navi = db.Navi;
            return navi;
        }
        /// <summary>
        /// 计算上下一条数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<article> TopDownsub(List<article> list,int id = 0) {
            List<article> result = new List<article>();
            int sub = 0;//记录下标
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Id == id) {
                    sub = i;
                }
            }
            result.Add(sub <= 0 ? new article(){ Id = 0,Title=string.Format("{0}","无")} : list[sub - 1]); //上
            result.Add(sub >= list.Count - 1 ? new article(){Id =0,Title =string.Format("{0}","无")} : list[sub + 1]);//下
            return result;
        }
        /// <summary>
        /// 导航数据获取
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Navi> naviListData() {
            IEnumerable<Navi> navis = null;
            try {
                yujingxueEntities db = new yujingxueEntities();
                navis = db.Navi.Where(f => f.Enable == true);
            }
            catch { }
            return navis;
        }
        /// <summary>
        /// 右侧最新文章,推荐文章
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,IEnumerable<article>> articleRightListData() {
            Dictionary<string,IEnumerable<article>> dic = new Dictionary<string,IEnumerable<article>>();
            yujingxueEntities db = new yujingxueEntities();
         
            try {
                dic.Add("New", db.article.Where(f => f.Enable == true).OrderByDescending(f => f.Time).Take(8));
                dic.Add("Top", db.article.Where(f => f.Enable == true).OrderByDescending(f => f.top).Take(8));
            }
            catch { }
            return dic;
        }
        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static MyAdmin loginVerifiCation(MyAdmin admin) {

            yujingxueEntities db = new yujingxueEntities();
            try { 
                    admin.pwd = Base.GetMD5(admin.pwd);
                    admin = db.MyAdmin.Where(f => f.UserName == admin.UserName.Trim().ToLower()).Where(f => f.pwd == admin.pwd).FirstOrDefault();
                    if (admin == null)
                    {
                        admin = new MyAdmin();
                        admin.Id = 0;
                    }
                    else {
                        HttpContext.Current.Session["yujx"] = admin;
                    }
            }
            catch { }

            return admin;
        }
        /// <summary>
        /// 修改数据状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int EnableUpdate(string type,int id = 0) {
            yujingxueEntities db = new yujingxueEntities();
            try {
                switch (type) { 
                    case "article":
                        article art = null;
                        art = db.article.Where(f => f.Id == id).FirstOrDefault();
                        if (art != null)
                        {
                            art.Enable = art.Enable == true ? false : true;
                            db.SaveChanges();
                            id = 0;
                        }
                        else
                        {
                            id = -1;
                        }
                        break;
                    case "navi":
                        Navi navi = null;
                        navi = db.Navi.Where(f => f.Id == id).FirstOrDefault();
                        if (navi != null)
                        {
                            navi.Enable = navi.Enable == true ? false : true;
                            db.SaveChanges();
                            id = 0;
                        }
                        else {
                            id = -1;
                        }
                        break;
                
                }
            }
            catch {
                id = -1;
            }
            return id;
        }
        /// <summary>
        /// 数据删除
        /// </summary>
        /// <returns></returns>
        public static int DataRemove(int id = 0) {
            article art = null;
            yujingxueEntities db = new yujingxueEntities();
            try {
                art = db.article.Where(f => f.Id == id).FirstOrDefault();
                db.article.Remove(art);
                if (db.SaveChanges() > 0)
                {
                    
                }
                else {
                    id = -1;
                }
            }
            catch {
                id = -1;
            }
            return id;
        }
        /// <summary>
        /// 后台管理文章操作
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static article articleAddAndEdit(article article) {
            article art = new article();
            yujingxueEntities db = new yujingxueEntities();
            try {
                art = db.article.Where(f => f.Id == article.Id).FirstOrDefault();
                if (art == null)
                {
                    art = new article();
                    art.Title = article.Title;
                    art.Text = article.Text;
                    art.author = article.author;
                    art.Time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    art.key = article.key == null ? string.Empty : article.key;
                    art.type = article.type;
                    art.read = article.read;
                    art.top = article.top;
                    art.tread = article.tread;
                    art.Enable = true;
                    db.article.Add(art);
                }
                else {
                    art.Title = article.Title;
                    art.Text = article.Text;
                    art.author = article.author;
                    art.key = article.key == null ? string.Empty : article.key;
                    art.type = article.type;
                    art.read = article.read;
                    art.top = article.top;
                    art.tread = article.tread;
                }
                if (db.SaveChanges() > 0)
                {
                    art.Id = 0;
                }
                else {
                    art.Id = -1;
                }
            }
            catch {
                art.Id = -1;
            
            }

            return art;
        }
        /// <summary>
        /// 转换articleType类型
        /// </summary>
        /// <returns></returns>
        public static string TypeToString(int type = 0) {
            string result = string.Empty;
            switch (type) { 
                case 1:
                    result = string.Format("{0}", "文章");
                    break;
                case 2:
                    result = string.Format("{0}", "游戏");
                    break;
            }
            return result;
                
        }










        /////////////////////////////////////-------------------------////////////////////////////////////////////



        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public string ListData()
        {
                string result = string.Empty;
            try
            {
              
                List<article> articles = db.article.Where(f => f.Enable == true).OrderByDescending(f=>f.Time).ToList();
                for (var item = 0; item < articles.Count();item++ )
                {
                    articles[item].Text = HttpUtility.UrlEncode(articles[item].Text);//HttpUtility.UrlEncode(articles[item].Text.Length > 300 ? articles[item].Text.Substring(0,300)+".." : articles[item].Text);
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = js.Serialize(articles);
            }
            catch {
                result = string.Empty;
            }
            return result;
        
        }
        /// <summary>
        /// 导航列表
        /// </summary>
        /// <returns></returns>
        public string NaviList() {
            string result = string.Empty;
            try {
                List<Navi> navis = null;
                navis = db.Navi.Where(f => f.Enable == true).ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = js.Serialize(navis);
            }
            catch {
                result = string.Format("{0}", -1);
            }
            return result;

        }
        /// <summary>
        /// 列表数据 1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ListData(int id)
        {
            string result = string.Empty;
            try
            {

                article articles = db.article.Where(f => f.Enable == true).Where(f=>f.Id == id).OrderBy(f => f.Time).FirstOrDefault();
                articles.Text = HttpUtility.UrlEncode(articles.Text);
                JavaScriptSerializer js = new JavaScriptSerializer();
                result = js.Serialize(articles);
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }
       // public static Boolean userAgent() {
       //     string u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
       // Regex b = new Regex(@"android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
       // Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
       // if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
       // {
       // //Label1.Text = "手机访问。";
       //     return false;
       // }
       // else
       // {
       //// Label1.Text = "电脑访问。";
       //     return true;
       // }

       // }
    }
}