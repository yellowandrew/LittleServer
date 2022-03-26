using System;
using System.Configuration;
using System.Xml;
namespace CommonLib
{
    public enum NetType {
        TCP,
        UDP,
        TCP_TCP,
        TCP_UDP,
        UDP_UDP,
        UDP_TCP
    }
    public class Service
    {
        HandlerManager manager;
        Parser parser;
        int mainport = 5678;
        int subport = 5679;
        public Service()
        {
            manager = new HandlerManager();
            parser = new Parser();
        }
       
        public void Start() {
           // masterTcp.OnRepackageData += subTcp.OnInternalData;
           // subTcp.OnWorkerData += masterTcp.OnInternalData;
           // masterTcp.Start();
           // subTcp.Start();
        }

        public void LoadServer() {
            string sv = ConfigurationManager.AppSettings[""];
        }

        SingleTcpServer CreateSingleTcp() {
            return new SingleTcpServer(manager, parser, mainport);
        }

        SingleUdpServer CreateSingleUdp() {
            return new SingleUdpServer(manager, parser, mainport);
        }

        MasterTcpServer CreateMasterTcp() {
            return new MasterTcpServer(parser, mainport);
        }
        SubTcpServer CreateSubTcp() {
            return new SubTcpServer(parser, subport);
        }

        MasterUdpServer CreateMasterUcp() {
            return new MasterUdpServer();
        }

        SubUdpServer CreateSubUdp() {
            return new SubUdpServer();
        }
    }
}
