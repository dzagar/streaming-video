using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace dzagar_SE3314_Assignment2
{
    class RTSP
    {
        IPAddress ipAddrServ;   //Server IP
        int portNo; //Port number
        Socket RTSPSock = null; //RTSP communication socket
        IPEndPoint endPointServ;    //server endpoint
        int CSeqNum;    //sequence number of msgs
        byte[] rcvBuffer;   //Rcving buffer to send over RTSP

        //Constructor
        public RTSP(int port, IPAddress servIP)
        {
            //Instantiate port num, server IP, sequence num, server endpoint, RTSP socket and buffer
            portNo = port;
            ipAddrServ = servIP;
            CSeqNum = 1;
            endPointServ = new IPEndPoint(ipAddrServ, portNo);
            RTSPSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            rcvBuffer = new byte[2048];
        }

        //Connect to server
        public bool ConnectServer()
        {
            try
            {
                RTSPSock.Connect(endPointServ); //Connect to endpoint
                return true;
            } catch (SocketException e)
            {
                //If exception thrown, close socket safely if not null
                if (RTSPSock != null)
                {
                    RTSPSock.Close();
                }
                return false;
            }
        }

        //Send msg to server
        public void SendServer(string action, int port, string vidName, IPAddress servIP, String sessionNo)
        {
            String portStr = port.ToString();
            String servIPStr = servIP.ToString();
            //Craft message to be sent to server
            String msg = action + " rtsp://" + servIPStr + ":" + portStr + "/" + vidName + " RTSP/1.0\r\n" + "CSeq: " + CSeqNum + "\r\n";
            if (action == "SETUP" || sessionNo == "no")
            {
                msg += "Transport: RTP/UDP; client_port= 25000";
            } else
            {
                msg += "Session: " + sessionNo;
            }
            //Increment sequence num of msgs
            CSeqNum++;
            try
            {
                //Convert msg to bytes and send to server
                rcvBuffer = Encoding.UTF8.GetBytes(msg);
                RTSPSock.Send(rcvBuffer);
            } catch (SocketException e){}
        }

        //Listen to server
        public String ListenServer()
        {
            try
            {
                //Receive server message
                int open = RTSPSock.Receive(rcvBuffer);
                //If nothing received, close socket safely
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

        //Reset sequence num to 1
        public void ResetSeqNum()
        {
            CSeqNum = 1;
        }
    }
}
