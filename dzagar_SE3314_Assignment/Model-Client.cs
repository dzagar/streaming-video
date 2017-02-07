using System.Text;
using System.Timers;

namespace dzagar_SE3314_Assignment
{
    class Client
    {
        //Store unique client identifier, port no, video, timer, sequence # and RTP model
        int clientID;
        int portNo;
        MJPEGVideo currentVideo;
        Timer videoCountdown;
        int seqNo;
        RTP rtpModel;

        public Client (int id, int port, MJPEGVideo vid)
        {
            //Set sequence number to 1 as default, set parameters to vars
            seqNo = 1;
            clientID = id;
            portNo = port;
            currentVideo = vid;
            //Create RTP model and new timer for video
            rtpModel = new RTP(port);
            videoCountdown = new Timer();
        }

        public RTPPacket CreatePacket()     //Create new RTP Packet to be sent through RTP
        {
            RTPPacket packet = new RTPPacket(seqNo, currentVideo.GetFollowingFrame());
            //Increment sequence number
            seqNo++;
            return packet;
        }

        //GET FUNCTIONS

        public int GetSeqNo()       //Get current sequence number
        {
            return seqNo;
        }

        public RTP GetClientRTP()       //Get RTP model of this client
        {
            return rtpModel;
        }

        public int GetClientID()        //Get unique client identifier
        {
            return clientID;
        }

        public Timer GetClientTimer()       //Get client timer
        {
            return videoCountdown;
        }

        public void StartClientTimer()      //Start client timer
        {
            videoCountdown.Enabled = true;
        }

        public void StopClientTimer()       //Stop client timer (actually pauses)
        {
            videoCountdown.Stop();
        }

        public void InitiateTeardown()      //Stop client timer and delete the video
        {
            videoCountdown.Stop();
            currentVideo.DeleteVideo();
        }

        public byte[] ClientUTF8Response()         //Send proper UTF8 response to client
        {
            string response = "RTSP/1.0 200 OK";
            response += "\r\nCSeq: " + seqNo;
            response += "\r\nSession: " + clientID;
            return Encoding.UTF8.GetBytes(response);
        }
    }
}
