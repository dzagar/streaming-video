using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace dzagar_SE3314_Assignment
{
    class Client
    {
        //Store socket
        Socket clientSocket;

        //On creation, initialize client socket
        public Client(Socket tcpSock)
        {
            clientSocket = tcpSock;
        }

        //Receive info from client
        public byte[] Communication()
        {
            //Set buffer size, I just randomly chose 2048
            byte[] rcvBuffer = new byte[2048];
            try
            {
                //Receive stream from socket, return
                int rcvFromSocket = clientSocket.Receive(rcvBuffer);
                return rcvBuffer;
            } catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //Send proper UTF8 response to client
        public void SendUTF8Response(int sessionNo, string seqNo)
        {
            string response = "RTSP/1.0 200 OK";
            response += "\r\nCSeq: " + seqNo;
            response += "\r\nSession: " + sessionNo;
            clientSocket.Send(Encoding.UTF8.GetBytes(response));

        }

    }
}
