using System;

namespace dzagar_SE3314_Assignment2
{
    class RTPPacket
    {
        //Unpack Frame (get rid of header)
        public byte[] UnpackFrame(byte[] frame)
        {
            //Create new byte array for new frame, block copy everything except header into new frame
            byte[] newFrame = new byte[frame.Length - 12];
            Buffer.BlockCopy(frame, 12, newFrame, 0, newFrame.Length);
            return newFrame;
        }
    }
}
