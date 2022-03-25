using System;
using System.Net;
using CommonLib;
using NetProtocol;

namespace LoginServer
{
    public class LoginHandler : Handler
    {
        public LoginHandler()
        {

        }
        protected override void HandleUnKownPackage(object data, uint id)
        {

        }

        [PackageHandle(1)]
        public void OnLogin(object connection, NetPackage package, IParser parser, object endpoint=null)
        {
            var req = package as LoginRequestPackage;
            Console.WriteLine($"OnLogin Request->username:{req.Username} password:{req.Password}");

            var res = new LoginResponePackage();
            res.msg = "登录失败";
            res.netid = req.netid;
            if (endpoint == null)
            {
                ((TcpNetClient)connection).SendAsync(parser.WritePackageToBuffer(res));
            }
            else
            {
                ((UdpNetClient)connection).SendAsync((EndPoint)endpoint, parser.WritePackageToBuffer(res));
            }
        }

        [PackageHandle(11)]
        public void OnRegister(object connection, NetPackage package, IParser parser, object endpoint=null)
        {
            var req = package as LoginRequestPackage;
            Console.WriteLine($"OnLogin Request->username:{req.Username} password:{req.Password}");

            var res = new LoginResponePackage();
            res.msg = "登录成功";
            res.netid = req.netid;
            if (endpoint == null)
            {
                ((TcpNetClient)connection).SendAsync(parser.WritePackageToBuffer(res));
            }
            else
            {
                ((UdpNetClient)connection).SendAsync((EndPoint)endpoint, parser.WritePackageToBuffer(res));
            }
        }

        [PackageHandle(2)]
        public void OnLoginall(object connection, NetPackage package, IParser parser, object endpoint=null)
        {

            Console.WriteLine($"OnLogin Callback");


        }

    }

}
