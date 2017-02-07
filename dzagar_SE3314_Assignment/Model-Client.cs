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
        RTP rtpModel;

        public Client (int id, int port, MJPEGVideo vid)
        {
            seqNo = 1;
            clientID = id;
            portNo = port;
            currentVideo = vid;
            rtpModel = new RTP(port);
            videoCountdown = new Timer(id);
        }

        public RTPPacket CreatePacket()
        {
            RTPPacket packet = new RTPPacket(seqNo, currentVideo.GetFollowingFrame());
            seqNo++;
            return packet;
        }

        public int GetSeqNo()
        {
            return seqNo;
        }

        public RTP GetClientRTP()
        {
            return rtpModel;
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
