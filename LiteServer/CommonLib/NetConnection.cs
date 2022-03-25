using System;
using System.Net.Sockets;
using NetCoreServer;

namespace CommonLib
{
    public class NetConnection:TcpSession
    {

       
        public NetConnection(TcpNetServer server):base(server)
        {
           
        }
        protected override void OnConnected()
        {
            Logger.Info($" session with Id {Id} connected!");
        }
        protected override void OnDisconnected()
        {
            Logger.Info($" session with Id {Id} disconnected!");
        }
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            ((TcpNetServer)Server).OnNetData(this,buffer);
        }
        protected override void OnError(SocketError error)
        {
            Logger.Info($" session caught an error with code {error}");
        }

        public void Brocast(byte[] buffer)
        {
            Server.Multicast(buffer);
        }
    }
}
