using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace Mykisskui.Models
{
    public class timeStamp
    {
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        static public DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        static public int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static void writelog(string msg)
        {
            try
            {
                string directory = MapPath("/logs");
                if (directory != "failed")
                {
                    if (Directory.Exists(directory) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(directory);
                    }
                }


                //Directory.Delete(Server.MapPath("~/upimg/hufu"), true);//删除文件夹以及文件夹中的子目录，文件    

                string url =MapPath("/logs/logs.txt");   //服务器域名
                if (url != "failed")
                {
                    msg = DateTime.Now.ToString() + "\t" + msg + "\r\n";
                    //判断文件的存在
                    if (File.Exists(url) == false)
                    {
                        File.AppendAllText(url, msg);
                    }
                    else
                    {
                        File.AppendAllText(url, msg);
                    }
                }
            }
            catch
            {

            }
        }
        public static string MapPath(string strPath)
        {
            string path = "";
            try
            {
                //在多线程里面使用HttpContext.Current,HttpContext.Current是得到null的.
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath(strPath);
                }
                else //非web程序引用
                {
                    strPath = strPath.Replace("/", "\\");
                    if (strPath.StartsWith("\\"))
                    {
                        //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                        strPath = strPath.TrimStart('\\');
                    }
                    path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
                }
            }
            catch
            {
                path = "failed";
            }
            return path;
        }
    }
}