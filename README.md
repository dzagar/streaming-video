Streaming Video Application

Using RTSP, RTP, UDP, multithreading and socket programming to create a streaming video application.

Assignment 1: Streaming Server

For the server to properly fetch the video files:
1. Place video files in C:\Videos (if you do not have a directory called Videos there, please make one!)
2. Done. I don't know why I made this into a list.

To run the client/server application:
1. Build and run (or click Start) in Visual Studios. I programmed this on VS 2015 Community.
2. Open the client .exe file (given with the assignment).
3. Click "Listen" after typing in a port number > 1024 in the text box. Make sure to do this before trying to connect on the Client.
4. In the CLient, make sure the port number field is the same one as the Server. Once it is the same, click Connect.

Once the client/server are connected, all further video interactions are in the Client.
To play a video:
1. Make sure the video file selected in the dropdown is the file you want to play.
2. Click "Setup".
3. Once the server responds, click "Play".
4. To pause, click "Pause"; to teardown the video, click "Teardown".

To display the RTP header fields as a bit stream in the Server text box, click the "Print Header" checkbox.
