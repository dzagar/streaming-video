using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace dzagar_SE3314_Assignment2
{
    class RTSP
    {
        IPAddress ipAddrServ;
        int portNo;
        Socket RTSPSock = null;
        IPEndPoint endPointServ;
        int CSeqNum;

        //Constructor
        public RTSP(int port, IPAddress servIP)
        {
            portNo = port;
            ipAddrServ = servIP;
            CSeqNum = 1;
            endPointServ = new IPEndPoint(ipAddrServ, portNo);
            RTSPSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        //Connect to server
        public void ConnectServer()
        {
            try
            {
                RTSPSock.Connect(endPointServ);
            } catch (SocketException e)
            {
                if (RTSPSock != null)
                {
                    RTSPSock.Close();
                }
            }
        }

        //Send msg to server

        //Listen to server
    }
}
