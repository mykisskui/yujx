using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mykisskui.Models
{
    public class Index
    {

        yujingxueEntities db = new yujingxueEntities();

        public IEnumerable<Navi> navi()
        {
            IEnumerable<Navi> navi = null;
            try {
                navi = db.Navi.Where(f => f.Enable == true).Take(5).OrderBy(f => f.Id);
            }
            catch {
            
            }
            return navi;
        }
        /// <summary>
        ///  首页文章部分
        /// </summary>
        /// <returns></returns>
        public IEnumerable<article> acticle()
        {
            IEnumerable<article> article = null;
            try
            {
               article = db.article.Where(f => f.Enable == true).Take(5).OrderByDescending(f=>f.Id);
            }
            catch {
            }
            return article;
        }
        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public article actyjx(string actid)
        {
             article article = null;
            int id = 0;
            try {
                id = int.Parse(actid);
            }
            catch {
                return article;
            }
            try
            {
                article = db.article.Where(f => f.Enable == true).Where(f => f.Id == id).OrderBy(f => f.Id).FirstOrDefault();
            }
            catch
            {

            }
            return article;
        }
        

    }
}