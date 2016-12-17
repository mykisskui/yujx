using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JasoftWeixin.Log4net
{
    public interface ILogger
    {
        void Write(object message);
    }
}