using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebSockets;

namespace Mykisskui.Controllers
{
    public class wsController :ApiController//Controller
    {
       public List<user> users = new List<user>();
        //
        // GET: /ws/
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {

                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
          return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        private async Task ProcessWSChat(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            JavaScriptSerializer js = new JavaScriptSerializer();
            user u = new user();
            Message m = new Message();
            string returnMessage = string.Empty;
            while (true)
            {
                if (socket.State == WebSocketState.Open)
                {
                    ///
                    var ss = users.AsEnumerable().Where(f => f.key == context.SecWebSocketKey).FirstOrDefault();
                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                    if (ss == null)
                    {
                        u = js.Deserialize<user>(message);

                        //首次连接的客户端
                        users.Add(new user() { key = context.SecWebSocketKey,name = u.name,admin = u.admin });
                        if (u.admin == 10010)
                        {
                            //连接的为管理员
                        }
                        else {
                            //客户端
                        }
                        m.name = string.Format("{0}", "服务器");
                        m.Text = string.Format("{0}", "连接成功");
                        m.level = string.Format("{0}", "ws_admin");
                        m.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        returnMessage = "{\"name\":\""+m.name+"\",\"text\":\""+m.Text+"\",\"ws\":\""+m.level+"\",\"time\":\""+m.Time+"\"}";
                        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else {
                        m.name = ss.name;
                        m.Text = string.Format("{0}", message);
                        m.level = string.Format("{0}", "ws_user");
                        m.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        //已经连接的客户端
                        //用户已经连接状态直接返回
                        returnMessage = "{\"name\":\"" + m.name + "\",\"text\":\"" + m.Text + "\",\"ws\":\"" + m.level + "\",\"time\":\"" + m.Time + "\"}";
                        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);


                        m.name = string.Format("{0}", "服务器");
                        m.Text = string.Format("{0}", "服务端接收成功");
                        m.level = string.Format("{0}", "ws_admin");
                        m.Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        returnMessage = "{\"name\":\"" + m.name + "\",\"text\":\"" + m.Text + "\",\"ws\":\"" + m.level + "\",\"time\":\"" + m.Time + "\"}";
                        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                   // string returnMessage = "You send :" + message + ". at" + DateTime.Now.ToLongTimeString();
         


                }
                else
                {
                    break;
                }
            }
        }
        public class user {
               public string key { get; set; }
            public string name { get; set; }
            public int admin  { get; set; }
        }
        public class Message {
                public string name { get; set; }
            public string Text { get; set; }
            /// <summary>
            ///  ws_admin
            ///  ws_user
            /// </summary>
            public string level { get; set; }
            public string Time { get; set; }

        }
    }
}
