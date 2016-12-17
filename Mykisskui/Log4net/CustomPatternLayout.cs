using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net.Layout;
/*
 * 转换成功log4net能够识别的属性
 * */
namespace JasoftWeixin.Log4net
{
    public class CustomPatternLayout : PatternLayout
    {
        public CustomPatternLayout()
        {
            AddConverter("property", typeof(CustomPatternLayoutConverter));
        }
    }
}