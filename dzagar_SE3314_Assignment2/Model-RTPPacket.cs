using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dzagar_SE3314_Assignment2
{
    class RTPPacket
    {
        //Unpack Frame (get rid of header)
        public byte[] UnpackFrame(byte[] frame)
        {
            byte[] newFrame = new byte[frame.Length - 12];
            Buffer.BlockCopy(frame, 12, newFrame, 0, newFrame.Length);
            return newFrame;
        }
    }
}
