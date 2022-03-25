using System;
using System.Linq;
namespace CommonLib
{
    public class MasterTcpServer:TcpNetServer
    {
        IParser parser;
        public Action<byte[]> OnRepackageData;
        public MasterTcpServer(IParser parser,int port) : base(port)
        {
            this.parser = parser;
        }
        
        //从客户端收到数据
        public override void OnNetData(NetConnection connection, byte[] buffer)
        {
            var _package = parser.ReadPackageFromBuffer(buffer);
            _package.netid = connection.Id.ToString();
            byte[] bt = parser.WritePackageToBuffer(_package);
            //转发到子服务器-内部转发
            OnRepackageData?.Invoke(bt);

        }

        //从子服务器收到数据
        public override void OnInternalData(byte[] buffer)
        {
            var _package = parser.ReadPackageFromBuffer(buffer);
            Guid uid = new Guid(_package.netid);
            //转发到客户端-网络发送
            Sessions[uid].SendAsync(buffer);
        }

    }
}
