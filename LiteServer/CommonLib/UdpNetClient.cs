using System;
using System.Net;
using System.Threading;
using NetCoreServer;

namespace CommonLib
{
    public class UdpNetClient:UdpClient
    {
        IParser parser;
        HandlerManager handlerManager;
        public UdpNetClient(HandlerManager handlerManager, IParser parser, string address, int port) : base(address, port) {
            this.parser = parser;
            this.handlerManager = handlerManager;
        }
        public void DisconnectAndStop()
        {
            _stop = true;
            Disconnect();
            while (IsConnected)
                Thread.Yield();
        }

        protected override void OnConnected()
        {
            Logger.Info($" UDP client connected a new session with Id {Id}");

            // Start receive datagrams
            ReceiveAsync();
        }

        protected override void OnDisconnected()
        {
            Logger.Info($"UDP client disconnected a session with Id {Id}");

            // Wait for a while...
            Thread.Sleep(1000);

            // Try to connect again
            if (!_stop)
                Connect();
        }

        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            handlerManager.Handle(this, parser, buffer, endpoint);
            // Continue receive datagrams
            ReceiveAsync();
        }

        protected override void OnError(System.Net.Sockets.SocketError error)
        {
            Logger.Info($"UDP client caught an error with code {error}");
        }

        public void SendPackage(NetPackage package)
        {
            Send(parser.WritePackageToBuffer(package));
        }
        public void SendPackageAsyn(NetPackage package)
        {
            SendAsync(parser.WritePackageToBuffer(package));
        }

        private bool _stop;
    }
}
