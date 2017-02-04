using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace dzagar_SE3314_Assignment
{
    public class Controller
    {
        List<Client> clients;
        Client _clientModel;
        Thread listenRTSP;
        private static MainView _view;
        RTSP _rtspModel;
        Thread listenClient;


        public Controller()
        {
            //Initial info needed to secure socket
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHostInfo.AddressList[0];
            IPEndPoint localEP = new IPEndPoint(ipAddr, 8000);
            //Create socket
            Socket servSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            //Bind socket
            try
            {
                servSocket.Bind(localEP);
                //servSocket.Listen(100);
                
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
