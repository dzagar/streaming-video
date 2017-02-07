using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace dzagar_SE3314_Assignment
{
    class MJPEGVideo
    {
        //Video
        FileStream videoStream;

        //On creation, do stuff
        public MJPEGVideo(string videoFileName)
        {
            string path = "\\Documents\\Third Year\\Second Semester\\SE 3314\\Labs\\streaming-video\\dzagar_SE3314_Assignment\\Resources\\" + videoFileName;
            videoStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public void DeleteVideo()
        {
            videoStream.Dispose();
            videoStream.Close();
        }

        //Getting frames to pass off to client function (no clue how to do this yet)
        public byte[] GetFollowingFrame()
        {
            //Find frame size
            byte[] byteArr = new byte[5];
            videoStream.Read(byteArr, 0, 5);
            if (byteArr[0] == 0 && byteArr[1] == 0 && byteArr[2] == 0 && byteArr[3] == 0 && byteArr[4] == 0)
            {
                return null;
            }
            else
            {
                int size = Int32.Parse(Encoding.UTF8.GetString(byteArr));
                //Create byte array which stores frames
                byte[] loadFrames = new byte[size];
                videoStream.Read(loadFrames, 0, size);
                return loadFrames;
            }
        }
    }
}
