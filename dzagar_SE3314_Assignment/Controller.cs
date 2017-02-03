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
        //public static ManualResetEvent allDone = new ManualResetEvent(false);
        //QUESTION: Asynchronous or synchronous server socket?
        //Async: https://msdn.microsoft.com/en-us/library/fx6588te(v=vs.110).aspx
        //Sync: https://msdn.microsoft.com/en-us/library/6y0e13d3(v=vs.110).aspx

        public Controller()
        {
            //allDone.Reset();
            //Initial info needed to secure socket
            /*IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
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
            }*/
        }
        public void SpawnRTSPThread()
        {

        }
    }
}
