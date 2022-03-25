using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using NetCoreServer;

namespace CommonLib
{
    public abstract class TcpNetServer: TcpServer
    {
        
        
       
        public TcpNetServer(int port):base(IPAddress.Any,port)
        {
           
        }
        public abstract void OnInternalData(byte[] buffer);
        public abstract void OnNetData(NetConnection connection, byte[] buffer);
        protected override TcpSession CreateSession() => new NetConnection(this);

        protected override void OnStarting()
        {
            base.OnStarting();
            Logger.Info($"{GetType()} OnStarting......");
        }
        protected override void OnStarted()
        {
            base.OnStarted();
            Logger.Info($"{GetType()} OnStarted!!");
        }

        protected override void OnConnecting(TcpSession session)
        {
            base.OnConnecting(session);
            Logger.Info($"Connection:{session.Id} OnConnecting......");
        }
        protected override void OnConnected(TcpSession session)
        {
            base.OnConnected(session);
            Logger.Info($"Connection:{session.Id} OnConnected!!");
           
        }

        protected override void OnDisconnecting(TcpSession session)
        {
            base.OnDisconnecting(session);
            Logger.Info($"Connection:{session.Id} OnDisconnecting......");

        }

        protected override void OnDisconnected(TcpSession session)
        {
            base.OnDisconnected(session);
            Logger.Info($"Connection:{session.Id} OnDisconnected!!");
            if (Sessions.TryRemove(session.Id, out var s))
            {
                s.Disconnect();
            }
        }

        protected override void OnStopping()
        {
            base.OnStopping();
            Logger.Info($"Server OnStopping......");
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Logger.Info($"Server OnStopped!!");
        }
        protected override void OnError(SocketError error)
        {
            Logger.Info($"Server caught an error with code {error}");
        }
    }

}
