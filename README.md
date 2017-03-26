# Streaming Video Application

Using RTSP, RTP, UDP, TCP, multithreading and socket programming to create a streaming video client/server application.
---
For the server to properly fetch the video files:
1. Place video files in C:\Videos (if you do not have a directory called Videos there, please make one!)
2. Done. I don't know why I made this into a list.

To run the client/server application:
1. Build and run (or click Start) in Visual Studios. I programmed this on VS 2015 Community.
2. Open the client .exe file (given with the assignment) or one of the Client applications I have built (assignment 2 or 3). Or both at the same time.
3. Click "Listen" after typing in a port number > 1024 in the text box. Make sure to do this before trying to connect on the Client.
4. In the Client, make sure the port number field is the same one as the Server. Once it is the same, click Connect. 

*Note:* In Android (assignment 3), the IP address will be 10.0.2.2 (NOT 127.0.0.1). You also must set up a redirection that will handle all incoming UDP connections to your dev machine (127.0.0.1:25000) and passes them through 10.0.2.15:25000. "telnet localhost 5554" (or whatever the port number of the emulator is), authenticate, then "redir add udp:25000:25000".

Once the client/server are connected, all further video interactions are in the Client.
To play a video:
1. Make sure the video file selected in the dropdown is the file you want to play.
2. Click "Setup".
3. Once the server responds, click "Play".
4. To pause, click "Pause"; to teardown the video, click "Teardown".

*Note:* In Android (assignment 3), you can also tap the video view for onscreen buttons. This is a completely redundant feature but oh well.

To display the RTP header fields as a bit stream in the Server text box, click the "Print Header" checkbox.