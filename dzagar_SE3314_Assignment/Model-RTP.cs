using System.Net;
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
            //Create UDP socket
            framesToCliSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1"); //local IP
            //Create client endpoint
            endPointClient = new IPEndPoint(ipAddr, portNo);
        }

        public IPEndPoint GetClientEndPoint()
        {
            return endPointClient;
        }
        
        public void SendPacketViaUDP(byte[] packet)        //Send RTP packet
        {
            framesToCliSock.SendTo(packet, endPointClient);
        }
    }
}
