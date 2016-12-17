using Mykisskui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Mykisskui
{
    /// <summary>
    /// ws 的摘要说明
    /// </summary>
    public class ws : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            timeStamp.writelog("测试11111");
          //  if (context.IsWebSocketRequest)
         //   {
                context.AcceptWebSocketRequest(ProcessChat);

            timeStamp.writelog("过了一个坎");
         //   }
        }
        private async Task ProcessChat(AspNetWebSocketContext context)
        {
            timeStamp.writelog("context:" + context.SecWebSocketKey);
            WebSocket socket = context.WebSocket;

            while (true)
            {
                if (socket.State == WebSocketState.Open)
                {
                    

                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                    WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                    string userMsg = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    userMsg = "你发送了：" + userMsg + "于" + DateTime.Now.ToLongTimeString();
                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMsg));
                    await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    break;
                }
            }

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}