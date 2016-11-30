using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mykisskui.Models
{
    public class Model
    {

        /// <summary>
        /// 前端音乐模型
        /// </summary>
        public class Music {
            public string title { get; set; }
            public string artist { get; set; }
            public string album { get; set; }
            public string cover { get; set; }
            public string mp3 { get; set; }
            public string ogg { get; set; }
        }
        /// <summary>
        /// 新闻列表分页
        /// </summary>
        public class NewListPageModel {
            /// <summary>
            /// 分页总页
            /// </summary>
            public static int PageCount { get; set; }
            public static int PageIndex { get; set; }
        }
        /// <summary>
        /// 新闻详情上下文章
        /// </summary>
        public class NewDetailsTopDown {
            /// <summary>
            /// 上一条
            /// </summary>
           public static article top { get; set; }
            /// <summary>
            /// 下一条
            /// </summary>
           public static article down { get; set; }
            /// <summary>
            /// 相关文章(暂不使用)
            /// </summary>
           public static List<article> relate { get; set; }
            
        }
        /// <summary>
        /// 百度热搜新闻
        /// </summary>
        public class baiduNews {
           public int errno { get; set; }
            public List<baiduNewsData> data { get; set; }
        }
        public class baiduNewsData {
            public string title { get; set; }
            public string desc { get; set; }
            public string query_word { get; set; }
            public string image { get; set; }
            public string image_v { get; set; }
        }



    }
}