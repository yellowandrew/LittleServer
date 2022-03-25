using System;
using System.Collections.Generic;

namespace CommonLib
{
    public class SubTcpServer:TcpNetServer
    {
        IParser parser;
        Dictionary<UInt32, NetConnection> workers = new Dictionary<uint, NetConnection>();
        public SubTcpServer(IParser parser, int port):base(port)
        {
            this.parser = parser;
        }

        public Action<byte[]> OnWorkerData;
        //从子服务器收到数据
        public override void OnNetData(NetConnection connection, byte[] buffer)
        {


            //var _package = parser.ReadPackageFromBuffer(buffer) as WorkerPackage;
            //connection.SendAsync(parser.WritePackageToBuffer(_package));
            /*
            if (_package.id == 777)//注册服务类型（登陆服务，战斗服务。。。）
            {
                Logger.Info("worker register");
                string[] arry = _package.worklist.Split(',');
                foreach (var item in arry)
                {

                    workers.Add(UInt32.Parse(item), connection);
                }
                    
            }
            else {
                Logger.Info("Call Masterserver");
                //转发到主服务器-内部转发
                OnWorkerData?.Invoke(buffer);
            }

            */

            connection.SendAsync(buffer);
        }

        //从主服务器收到数据
        public override void OnInternalData(byte[] buffer)
        {
            Logger.Info("Data From Masterserver");
            //转发到子服务器-网络发送
            var _package = parser.ReadPackageFromBuffer(buffer);
            workers[_package.id].SendAsync(buffer);
        }

    }
}
