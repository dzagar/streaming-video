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
        byte[] rcvBuffer;

        //Constructor
        public RTSP(int port, IPAddress servIP)
        {
            portNo = port;
            ipAddrServ = servIP;
            CSeqNum = 1;
            endPointServ = new IPEndPoint(ipAddrServ, portNo);
            RTSPSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            rcvBuffer = new byte[2048];
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
        public void SendServer(string action, int port, string vidName, IPAddress servIP, int sessionNo)
        {
            String portStr = port.ToString();
            String servIPStr = servIP.ToString();
            String msg = action + " rtsp://" + servIPStr + ":" + portStr + "/" + vidName + "RTSP/1.0\r\n" + "CSeq: " + CSeqNum + "\r\n";
            if (action == "SETUP" || sessionNo == 0)
            {
                msg += "Transport: RTP/UDP; client_port = 25000";
            } else
            {
                msg += "Session: " + sessionNo;
            }
            CSeqNum++;
            try
            {
                rcvBuffer = Encoding.UTF8.GetBytes(msg);
                RTSPSock.Send(rcvBuffer);
            } catch (SocketException e)
            {

            }
        }

        //Listen to server
        public String ListenServer()
        {
            try
            {
                int open = RTSPSock.Receive(rcvBuffer);
                if (open == 0)
                {
                    RTSPSock.Close();
                    return "Error: no byte transfer";
                }
                return Encoding.UTF8.GetString(rcvBuffer) + "\r\n"; 
            } catch (SocketException e)
            {
                return "Error on socket: " + e.Message + "\r\n";
            }
        }
    }
}
