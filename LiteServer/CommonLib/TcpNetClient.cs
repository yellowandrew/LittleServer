using System;
using System.Threading;
using NetCoreServer;

namespace CommonLib
{
    public class TcpNetClient : TcpClient
    {
        IParser parser;
        public HandlerManager handlerManager;
        public TcpNetClient(HandlerManager handlerManager, IParser parser, string address, int port) : base(address, port)
        {
            this.parser = parser;
            this.handlerManager = handlerManager;
        }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Logger.Info($" TCP client connected a new session with Id {Id}");
        }

        protected override void OnDisconnected()
        {
            Logger.Info($" TCP client disconnected a session with Id {Id}");

            // Wait for a while...
            //Thread.Sleep(1000);

            // Try to connect again
            //if (!_stop)
                //ConnectAsync();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            handlerManager.Handle(this, parser, buffer);
        }
        public void SendPackage(NetPackage package)
        {
            Send(parser.WritePackageToBuffer(package));
        }
        public void SendPackageAsyn(NetPackage package)
        {
            SendAsync(parser.WritePackageToBuffer(package));
        }
        protected override void OnError(System.Net.Sockets.SocketError error)
        {
            Logger.Info($"Chat TCP client caught an error with code {error}");
        }

        private bool _stop;
    }
}
