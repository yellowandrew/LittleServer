
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using SharePackages;

namespace CenterServer
{
    public class LoginHandler : Handler
    {
        public LoginHandler() 
        {

        }
        protected override void HandleUnKownPackage(object data, uint id)
        {

        }

        [PackageHandle(PackageType.LOGIN_C2S)]
        public void OnLogin(object connection, NetPackage package, IParser parser, object endpoint)
        {
            var req = package as LoginRequestPackage;
            Console.WriteLine($"OnLogin Request->username:{req.Username} password:{req.Password}");

            var res = new LoginResponePackage() { msg = "登录成功" };
            if (endpoint == null)
            {
                ((NetConnection)connection).SendAsync(parser.WritePackageToBuffer(res));
            }
            else
            {
                ((UdpNetServer)connection).SendAsync((EndPoint)endpoint,parser.WritePackageToBuffer(res));
            }
        }

        
    }

    public class FightHandler : Handler
    {
        public FightHandler() 
        {

        }

        [PackageHandle(PackageType.FIGHT_C2S)]
        public void OnFight(object connection, NetPackage package,IParser parser, object endpoint)
        {
            
            var req = package as FightRequestPackage;
            Console.WriteLine($"OnFight Request->{req.Map}");
            var res = new FightResponePackage() { msg = "哈哈,开始战斗了!" };
            if (endpoint == null)
            {
                ((NetConnection)connection).SendAsync(parser.WritePackageToBuffer(res));
            }
            else
            {
                ((UdpNetServer)connection).SendAsync((EndPoint)endpoint, parser.WritePackageToBuffer(res));
            }
            

        }
    }

}
