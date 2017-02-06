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
        //Store frame #, the MJPEG object, client endpoint, sending socket, TIMER?
        int frameNo;
        int timerInterval = 200; //Chose 200 ms to be my interval
        MJPEGVideo currentVideo;
        IPEndPoint endPointClient;
        Socket framesToCliSock;
        Timer countdownTimer;
        RTPPacket rtpPacket;

        public RTP()
        {
            //Create elapsed event handler to handle elapsed event
            ElapsedEventHandler sender = new ElapsedEventHandler(SendRTPPacket);
            //Default frame to 0
            frameNo = 0;
            //Create new timer with specified interval and add handler
            countdownTimer = new Timer(timerInterval);
            countdownTimer.Elapsed += sender;
        }

        //Create socket and load video (create MJPEG object)
        public void InitializeSocketAndVideo(string port, string videoFileName)
        {

        }
        
        //Send RTP packet
        private void SendRTPPacket(Object source, ElapsedEventArgs e)
        {
            //Increment frame number
            frameNo++;
            //Check if the frame is null (in which case, terminate), else send the packet to the client
            

        }
    }
}
