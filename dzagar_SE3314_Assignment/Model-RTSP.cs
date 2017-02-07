using System;
using System.Net.Sockets;
using System.Net;

namespace dzagar_SE3314_Assignment
{
    class RTSP
    {
        //Store the IP address, port number, RTSP server socket, the end point, and connecting client socket
        IPAddress ipAddrServ;
        int portNo;
        Socket RTSPsocket = null;
        Socket clientSock = null;
        IPEndPoint endPointServ;

        public RTSP(int port)
        {
            //Set port number
            portNo = port;
            try
            {
                //Defaulting IP Address to 127.0.0.1 as shown in demo
                ipAddrServ = IPAddress.Parse("127.0.0.1");
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //Set an endpoint for the server socket to bind to based on the IP Address and port
            endPointServ = new IPEndPoint(ipAddrServ, portNo);
            try
            {
                //Instantiate and bind server socket
                RTSPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RTSPsocket.Bind(endPointServ);
            } catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                //Safely close socket
                if (!(RTSPsocket == null))
                {
                    RTSPsocket.Close();
                }
            }
            //Listen
            RTSPsocket.Listen(int.MaxValue);
            
        }

        public Socket AcceptClient()        //On client connection, accept and return the client socket
        {
            try
            {
                //Wait for client to accept
                clientSock = RTSPsocket.Accept();
                //Returns socket
                return clientSock;
            } catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                //Safely close socket
                if (!(clientSock == null))
                {
                    clientSock.Close();
                }
                return null;
            }
            
        }


        //GET FUNCTIONS

        public IPAddress GetIP()        //Get server IP address
        {
            return ipAddrServ;
        }

        public IPEndPoint GetEndPoint()     //Get server end point
        {
            return endPointServ;
        }
    }
}
