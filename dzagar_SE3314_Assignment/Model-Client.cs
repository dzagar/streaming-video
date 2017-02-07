using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Timers;

namespace dzagar_SE3314_Assignment
{
    class Client
    {
        int clientID;
        int portNo;
        MJPEGVideo currentVideo;
        Timer videoCountdown;
        int seqNo;

        public Client (int id, int port, MJPEGVideo vid)
        {
            seqNo = 1;
            clientID = id;
            portNo = port;
            currentVideo = vid;
        }

        public int GetClientID()
        {
            return clientID;
        }

        public Timer GetClientTimer()
        {
            return videoCountdown;
        }

        public void StartClientTimer()
        {
            videoCountdown.Enabled = true;
        }

        public void StopClientTimer()
        {
            videoCountdown.Stop();
        }

        public void InitiateTeardown()
        {
            currentVideo.DeleteVideo();
        }

        //Store socket
        /*Socket clientSocket;

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
                return null;
            }
        }*/

        //Send proper UTF8 response to client
        public byte[] ClientUTF8Response()
        {
            string response = "RTSP/1.0 200 OK";
            response += "\r\nCSeq: " + seqNo;
            response += "\r\nSession: " + clientID;
            return Encoding.UTF8.GetBytes(response);
        }

    }
}
