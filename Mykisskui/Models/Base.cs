using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Mykisskui.Models
{
    public class Base
    {
        
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sDataIn"></param>
        /// <returns></returns>
        public static string GetMD5(string sDataIn)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] byt, bytHash;
            byt = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(byt);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return sTemp;
        }



        /**/
        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        /// <summary>
        /// 上传文件 方法
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="toFilePath"></param>
        /// <returns>返回上传处理结果   格式说明： 0|file.jpg|msg   成功状态|文件名|消息    </returns>
        public static string UpLoadFile(string FileType, HttpPostedFileWrapper Filedata, string weid, string Id, string FilePath)
        {
            string media;
            //if (string.IsNullOrEmpty(account))
            //{
            //    return "0|errorfile|" + "文件上传失败,错误原因：您尚未登录或登录超时!";
            //}
            try
            {
                // HttpPostedFile uploadFile = null;
                HttpPostedFileWrapper uploadFile = null;
                try
                {
                    uploadFile = Filedata;
                    // uploadFile = context.Request.Files[0];
                }
                catch (HttpException ex)
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：服务器不能接受您的文件!";
                }
                string fileType = FileType;

                // string weid = weid;  //如果异常 默认放到default中
                try
                {
                    //weid =((ims_wechats)Session["currentGZH"]).weid.ToString();
                    if (string.IsNullOrEmpty(weid))
                    {
                        weid = "default";
                    }
                }
                catch (Exception)
                {
                    weid = "default";
                }
                string _path = "";
                string toFilePath = FilePath;
                //文件为空
                if (uploadFile == null || string.IsNullOrEmpty(uploadFile.FileName))
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：未选择任何文件！";
                }
                //获取要保存的文件信息
                FileInfo file = new FileInfo(uploadFile.FileName);
                //获得文件扩展名
                string fileNameExt = file.Extension;

                //验证合法的文件
                if (CheckFileExt(fileNameExt, fileType))
                {
                    //toFilePath += "/" + fileType.Replace('.', ' ').Trim() + "/" + DateTime.Now.ToString("yyyyMM") + "/";
                    //生成将要保存的随机文件名
                   string fileName = Rand.Str(8).ToLower() + fileNameExt;
                 //   string fileName = Filedata.FileName;
                    //获得要保存的文件路径
                    //需要上传的变量
                    string serverFileNameThumb = toFilePath + fileName;
                    ////执行语句时使用
                    //FIle.serverFileName = toFilePath + fileName;

                    string serverPath = PathHelper.Map(serverFileNameThumb);

                    System.IO.DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(serverPath));
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    uploadFile.SaveAs(serverPath);

                    if (IsPic(fileNameExt))
                    {
                        try
                        {
                            //System.Util.MakeThumbnail(PathHelper.Map(serverFileName), PathHelper.Map(serverFileNameThumb), 80, 80, "HW");
                            return "1|" + serverFileNameThumb + "|" + "恭喜你，文件上传成功!|" + Id;

                        }
                        catch
                        {
                            return "1|" + serverFileNameThumb + "|" + "恭喜你，文件上传成功，但生成缩略图错误!";
                        }
                    }
                    //上传类型是视频时生成缩略图
                    //if (IsMp4(fileNameExt))
                    //{
                    //    //视频文件
                    //    Base ify = new Base();
                    //    //生成缩略图时判断是否存在缩略图文件夹
                    //    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/css/Video/VideoImage/" + JasoftWeixin.Controllers.MaterialController.SeesionWeid() + "/")))
                    //    {
                    //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/css/Video/VideoImage/" + JasoftWeixin.Controllers.MaterialController.SeesionWeid() + "/"));
                    //    }
                    //    ify.CatchImg(serverPath, fileName);

                    //    int rets = Help.Videomediaid(fileName);//获取临时素材mediaid
                    //    if (rets != 0)
                    //    {
                    //        FileInfo infos = new FileInfo(VideoURL + fileName);
                    //        if (infos.Exists)
                    //        {
                    //            infos.Delete();
                    //            return "0|errorfile|" + "系统异常，请稍后重试！";
                    //        }
                    //    }
                    //}
                    //if (IsAudio(fileNameExt))
                    //{
                    //    int ret = Help.Voicemediaid(fileName);//获取临时素材mediaid
                    //    if (ret != 0)
                    //    {
                    //        FileInfo infos = new FileInfo(VoiceURL + fileName);
                    //        if (infos.Exists)
                    //        {
                    //            infos.Delete();
                    //            return "0|errorfile|" + "系统异常，请稍后重试！";
                    //        }
                    //    }
                    //}
                    return "1|" + serverFileNameThumb + "|" + "恭喜你，文件上传成功!" + Id;
                }
                else
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：您选择的文件格式错误";
                }
            }
            catch (DirectoryNotFoundException e)
            {
                return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
            }
            catch (Exception e)
            {
                //return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
                return "0|errorfile|" + "文件上传失败,错误原因：" + e.Message;
            }
        }
        private static bool CheckFileExt(string ext, string type)
        {
            //string extlist = Tool.GetConfiger("UploadExt");
            string extlist = "";
            if (string.IsNullOrEmpty(extlist) || extlist.Contains(ext))
            {
                if (type == "pic")
                {
                    return IsPic(ext);
                }
                else if (type == "audio")
                {
                    return IsAudio(ext);
                }
                else if (type == "image")
                {
                    return IsPic(ext);
                }
                else if (type == "thumb")
                {
                    return IsPic(ext);
                }
                else if (type == "mp4")
                {
                    return IsMp4(ext);
                }

            }
            return false;
        }

        private static bool IsPic(string ext)
        {
            if (".jpg,.gif,.png".Contains(ext.ToLower()))
            {
                return true;
            }
            return false;
        }
        private static bool IsAudio(string ext)
        {
            if (".mp3,.avi,.rm,.amr".Contains(ext.ToLower()))
            {
                return true;
            }
            return false;
        }
        private static bool IsMp4(string ext)
        {
            if (".mp4,.MP4".Contains(ext.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}