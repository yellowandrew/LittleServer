using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CommonLib
{
    public class SingleTcpServer:TcpNetServer
    {
        
        IParser parser;
        HandlerManager handlerManager;
      
        public SingleTcpServer(HandlerManager handlerManager,IParser parser, int port) : base(port)
        {
            this.parser = parser;
            this.handlerManager = handlerManager;
        }

        public override void OnInternalData(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public override void OnNetData(NetConnection connection, byte[] buffer)
                                                         {
            handlerManager.Handle(connection,parser,buffer);
        }
        


    }
}
