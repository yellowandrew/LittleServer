using System;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace CommonLib
{
    public abstract class UdpNetServer:UdpServer
    {
        public UdpNetServer(int port) : base(IPAddress.Any, port) { }

        protected override void OnStarted()
        {
            // Start receive datagrams
            ReceiveAsync();
        }
        
        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            OnData(endpoint,buffer);
            // Continue receive datagrams
            ReceiveAsync();
        }

        protected override void OnError(SocketError error)
        {
            Logger.Info($" UDP server caught an error with code {error}");
        }

        public abstract void OnData(EndPoint endpoint, byte[] buffer);

    }
}
