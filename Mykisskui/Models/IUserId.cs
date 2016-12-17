using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Mykisskui.Models
{
    public class IUserId : IUserIdProvider
    {
        /// <summary>
        ///  实现接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetUserId(IRequest request)
        {
                //if (request.GetHttpContext().Request.Cookies[IUser.SignalrID] != null)
                //{
                //    return request.GetHttpContext().Request.Cookies[IUser.SignalrID].Value;
                //}
              
            return string.Empty;
            //throw new NotImplementedException();
        }
    }
}