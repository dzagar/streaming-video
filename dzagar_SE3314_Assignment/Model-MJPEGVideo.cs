using System;
using System.Text;
using System.IO;
namespace dzagar_SE3314_Assignment
{
    class MJPEGVideo
    {
        //Store filestream (video)
        FileStream videoStream;

        //On creation, do stuff
        public MJPEGVideo(string videoFileName)
        {
            //Store video files in C:\Videos and it will find them! Thanks Stack Overflow!
            string targetDirectory = @"C:\Videos";
            //Set current directory
            Environment.CurrentDirectory = targetDirectory;
            string filePath = Directory.GetCurrentDirectory();
            //Create new FileStream -- open file and readonly
            videoStream = new FileStream(videoFileName, FileMode.Open, FileAccess.Read);
        }

        public void DeleteVideo()       //Delete this video safely
        {
            //Dispose of FileStream before closing
            videoStream.Dispose();
            //Close stream
            videoStream.Close();
        }

        public byte[] GetFollowingFrame()        //Getting frames to pass off to client function
        {
            //Read video header
            byte[] header = new byte[5];
            videoStream.Read(header, 0, 5);
            //if all header bytes are zero, frame DNE
            if (header[0] == 0 && header[1] == 0 && header[2] == 0 && header[3] == 0 && header[4] == 0)
            {
                return null;
            }
            else
            {
                //Get frames size
                int size = Int32.Parse(Encoding.UTF8.GetString(header));
                //Create byte array which stores frames, read in, and return
                byte[] loadFrames = new byte[size];
                videoStream.Read(loadFrames, 0, size);
                return loadFrames;
            }
        }
    }
}
