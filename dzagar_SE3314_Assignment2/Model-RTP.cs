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
        IPEndPoint endPointServ;
        UdpClient framesFromServCli;
        RTPPacket _rtpPacket = null;

        //Constructor
        public RTP(int port, IPAddress servIP)
        {
            endPointServ = new IPEndPoint(servIP, port);
            framesFromServCli = new UdpClient(25000);
            _rtpPacket = new RTPPacket();
        }

        //Get frame bytes
        public byte[] GetFrame()
        {
           try
            {
                byte[] vidFramePkt = framesFromServCli.Receive(ref endPointServ);
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
            byte[] vidFrameUnpkt = _rtpPacket.UnpackFrame(frame);
            try
            {
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
