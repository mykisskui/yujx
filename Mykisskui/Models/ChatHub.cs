using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Mykisskui.Models;
using System.Security.Principal;

namespace Mykisskui.Models
{

    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
       public List<HubMessage> users = null;
        public HubMessage message = null;
        JavaScriptSerializer js = new JavaScriptSerializer();
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        [HubMethodName("SendMessag")]
        public async Task SendMessag(string content)
        {


            message = new HubMessage();
            message.Name = _connections.GetConnections(Context.ConnectionId).Last();
            message.content = content;  
            message.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string result = js.Serialize(message);
            await Clients.All.talk(result);
        }
        /// <summary>
        /// 通知方法
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        public string cast(string ClientName) {
            message = new HubMessage();
            message.connectionId = Context.ConnectionId;
            message.content = string.Format("客户端: {0} 成功连接", ClientName);
            message.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return js.Serialize(message);
        }
        /// <summary>
        /// 连接初始化
        /// 默认创建随机数作为name值
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnected() {
           string random = new Random().Next(100000, 1000000).ToString();
            _connections.Add(Context.ConnectionId,random);
            message = new HubMessage();
            message.connectionId = Context.ConnectionId;
            message.content = string.Format("{0}", "连接成功");
            message.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string result = js.Serialize(message);
            await Clients.Client(message.connectionId).talk(result);
            message.Name =random;
            await Clients.All.talk(cast(message.Name));
            //return base.OnConnected();
        }
        /// <summary>
        /// 修改昵称通知
        /// </summary>
        /// <returns></returns>
        [HubMethodName("Change")]
        public async Task ChangeName(string clientName) {

            await Task.Run(()=> _connections.Add(Context.ConnectionId, clientName)
                );

        }
    }
    public class HubMessage {
        public string connectionId { get; set; }
        /// <summary>
        /// ws_admin
        /// ws_user
        /// </summary>
        private string hubs = "ws_user";
        public string Hubs {
            get { return hubs; }
            set { hubs = value; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        private string name = "系统";
        public string Name {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// 发送的信息
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 发送的时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public int code { get; set; }
    }
}