using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonLib;

namespace CenterServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            SingleTcpServer server =  new SingleTcpServer(new HandlerManager (),new Parser(),5678);
            //SubTcpServer server = new SubTcpServer( new Parser(), 5678);
            //SingleUdpServer server =  new SingleUdpServer(new HandlerManager (),new Parser(),5678);
            server.Start();
            //Service service = new Service();
            //service.Start();
            
            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape) break;
            }
        }
    }


}
