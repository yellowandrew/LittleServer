using System;
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
        public Service()
        {
        }

        MasterTcpServer masterTcp;
        SubTcpServer subTcp;
        public void Start() {
            masterTcp = new MasterTcpServer(new Parser(),5678);
            subTcp = new SubTcpServer(new Parser(), 5679);

            masterTcp.OnRepackageData += subTcp.OnInternalData;
            subTcp.OnWorkerData += masterTcp.OnInternalData;
            

            masterTcp.Start();
            subTcp.Start();
        }

        public void LoadConfig() { }

        
    }
}
