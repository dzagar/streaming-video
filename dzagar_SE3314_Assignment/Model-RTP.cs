using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Timers;
using System.Net.Sockets;

namespace dzagar_SE3314_Assignment
{
    class RTP
    {
        //Store client endpoint, sending socket
        IPEndPoint endPointClient;
        Socket framesToCliSock;

        public RTP(int portNo)
        {
            framesToCliSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1"); //local IP
            endPointClient = new IPEndPoint(ipAddr, portNo);
        }

        public IPEndPoint GetClientEndPoint()
        {
            return endPointClient;
        }
        
        //Send RTP packet
        public void SendPacketViaUDP(byte[] packet)
        {
            framesToCliSock.SendTo(packet, endPointClient);
        }
    }
}
