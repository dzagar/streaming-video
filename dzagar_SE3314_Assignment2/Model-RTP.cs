using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.IO;

namespace dzagar_SE3314_Assignment2
{
    class RTP
    {
        IPEndPoint endPointServ;    //server endpoint
        UdpClient framesFromServCli;    //frames from server to client
        RTPPacket _rtpPacket = null;    //one instance of RTP packet

        //Constructor
        public RTP(int port, IPAddress servIP)
        {
            //Instantiate server endpoint, socket, and rtp packet
            endPointServ = new IPEndPoint(servIP, port);
            framesFromServCli = new UdpClient(25000);
            _rtpPacket = new RTPPacket();
        }

        //Get frame bytes
        public byte[] GetFrame()
        {
           try
            {
                //Rcv packetized video frame from server
                byte[] vidFramePkt = framesFromServCli.Receive(ref endPointServ);
                //If rcvd, return, otherwise return null
                if (vidFramePkt.Length > 0)
                {
                    return vidFramePkt;
                } else
                {
                    return null;
                }
            } catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //Frame to image
        public Image FrameToImage(byte[] frame)
        {
            //Create unpacketized frame byte array
            byte[] vidFrameUnpkt = _rtpPacket.UnpackFrame(frame);
            try
            {
                //Convert unpacketized frame byte array to image and return
                Image img = Image.FromStream(new MemoryStream(vidFrameUnpkt));
                return img;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
