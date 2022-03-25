using System;
using System.Linq;
using System.Reflection;
using CommonLib;
using SharePackages;

namespace LoginServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            TcpNetClient tcpNet = new TcpNetClient(new HandlerManager(), new Parser(),"192.168.0.194", 5678);
            string workStr = "";
            foreach (var item in tcpNet.handlerManager.actions.Keys)
            {
                
                workStr += ","+item;
               
            }

            workStr = workStr.TrimStart(',');
          
            Logger.Info(workStr);
            //Console.WriteLine("Hello World!");
            if (tcpNet.ConnectAsync())
            {
                Console.WriteLine("Hello World!");
               
            }
            

            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.A) {
                    LoginRequestPackage login = new LoginRequestPackage()
                    {

                        Username = "abc",
                        Password = "456"
                    };
                    tcpNet.SendPackageAsyn(login);
                }
                if (key == ConsoleKey.S)
                {
                    ServicePackage worker = new ServicePackage();
                    worker.worklist = workStr;
                    tcpNet.SendPackageAsyn(worker);
                }
                if (key == ConsoleKey.Escape) break;
            }
        }
    }
}
