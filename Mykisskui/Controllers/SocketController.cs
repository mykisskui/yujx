using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mykisskui.Controllers
{
    public class SocketController : Controller
    {
        //
        // GET: /Socket/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void open() {
            Socket socket = null;
            Socket serversocket = null;
            try
            {
                int port = 9001;
                string host = "127.0.0.1";
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(ipe);//关联
                socket.Listen(0);//监听
                Console.WriteLine("监听开启");
                serversocket = socket.Accept();
                Console.WriteLine("连接陈功");
                string recStr = "";
                byte[] recByte = new byte[4096];
                int bytes = serversocket.Receive(recByte, recByte.Length, 0);
                recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
                //send message
                Console.WriteLine("服务器端获得信息:{0}", recStr);
                string sendStr = "send to client :hello";
                byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                serversocket.Send(sendByte, sendByte.Length, 0);
                serversocket.Close();
                socket.Close();
            }
            catch {
                serversocket.Close();
                socket.Close();
            }
        }

    }
}
