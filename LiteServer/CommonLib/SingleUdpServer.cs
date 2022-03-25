using System;
using System.Net;

namespace CommonLib
{
    public class SingleUdpServer:UdpNetServer
    {
        IParser parser;
        HandlerManager handlerManager;
        public SingleUdpServer(HandlerManager handlerManager, IParser parser, int port) : base(port)
        {
            this.parser = parser;
            this.handlerManager = handlerManager;
        }

        public override void OnData(EndPoint endpoint, byte[] buffer)
        {
            handlerManager.Handle(this, parser, buffer, endpoint);
        }
    }
}
