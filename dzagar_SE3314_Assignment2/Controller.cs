using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace dzagar_SE3314_Assignment2
{
    class Controller
    {
        private static View _view; //Reference to main View
        RTSP _rtspModel = null; //One instance of RTSP
        RTP _rtpModel = null;   //RTP model
        String sessionNo = "";  //Session number
        Thread videoPlaybackThread = null;  //Server listening thread
        byte[] frameBytes;  //Frame in bytes

        ///--------------BUTTON FUNCTIONS--------------///

        //Connect button click
        public void OnConnect(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();  //Find View
            _view.DisableButton("Connect"); //Disable Connect btn
            RTSPListen();   //Begin listening for server
        }

        //Exit button click
        public void OnExit(object sender, EventArgs e)
        {
            //If the thread is not null, abort safely
            if (videoPlaybackThread != null)
            {
                videoPlaybackThread.Abort();
            }
            Application.Exit(); //Exit application
        }

        //Setup button click
        public void OnSetup(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            //Send server SETUP message in appropriate format
            bool success = _rtspModel.SendServer("SETUP", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), "no");
            if (success)
            {
                //Listen for server response on RTSP and parse response
                String servResponse = _rtspModel.ListenServer();
                String[] msg = ParseServerResponse(servResponse);
                sessionNo = msg[6];
                //Update server and client activity boxes with server message and client prompt
                UpdateServerActivity(FormatServerResponse(msg));
                UpdateClientActivity("New RTSP State: READY");
                //Disable Setup, editing of the video filename
                _view.DisableButton("Setup");
                _view.DisableButton("VideoName");
                //Enable Play
                _view.EnableButton("Play");
            } else
            {
                UpdateClientActivity("Server has disconnected. Please try again...");
                _view.DisableButton("Setup");
                _view.DisableButton("Play");
                _view.DisableButton("Pause");
                _view.DisableButton("Teardown");
                _view.DisableVideoView();
                _view.EnableButton("Connect");
                _view.EnableButton("VideoName");
            }
        }

        //Play button click
        public void OnPlay(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            //Send server PLAY message in appropriate format
            bool success = _rtspModel.SendServer("PLAY", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            if (success)
            {
                //Listen for server response on RTSP and parse response
                String servResponse = _rtspModel.ListenServer();
                String[] msg = ParseServerResponse(servResponse);
                //If the rcvd message is over appropriate protocol, update server activity with msg
                if (msg[0] == "RTSP/1.0")
                {
                    UpdateServerActivity(FormatServerResponse(msg));
                }
                //If the video playback thread does not exist, create and start it in background
                if (videoPlaybackThread == null)
                {
                    videoPlaybackThread = new Thread(PlaybackCommunications);
                    videoPlaybackThread.IsBackground = true;
                    videoPlaybackThread.Start();
                }
                //Update client activity with prompt
                UpdateClientActivity("New RTSP State: PLAYING");
                //Disable Play button
                _view.DisableButton("Play");
                //Enable Pause and Teardown btns
                _view.EnableButton("Pause");
                _view.EnableButton("Teardown");
            } else
            {
                UpdateClientActivity("Server has disconnected. Please try again...");
                _view.DisableButton("Setup");
                _view.DisableButton("Play");
                _view.DisableButton("Pause");
                _view.DisableButton("Teardown");
                _view.DisableVideoView();
                _view.EnableButton("Connect");
                _view.EnableButton("VideoName");
            }
        }

        //Pause button click
        public void OnPause(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            //Send server PAUSE message in appropriate format
            bool success = _rtspModel.SendServer("PAUSE", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            if (success)
            {
                //Listen for server response on RTSP and parse response
                String servResponse = _rtspModel.ListenServer();
                String[] msg = ParseServerResponse(servResponse);
                //If the rcvd message is over appropriate protocol, update server activity with msg
                if (msg[0] == "RTSP/1.0")
                {
                    UpdateServerActivity(FormatServerResponse(msg));
                }
                //Update client activity with prompt
                UpdateClientActivity("New RTSP State: PAUSED");
                //Disable Pause, Enable Play
                _view.DisableButton("Pause");
                _view.EnableButton("Play");
            } else
            {
                //Server disconnected; update client
                UpdateClientActivity("Server has disconnected. Please try again...");
                _view.DisableButton("Setup");
                _view.DisableButton("Play");
                _view.DisableButton("Pause");
                _view.DisableButton("Teardown");
                _view.DisableVideoView();
                _view.EnableButton("Connect");
                _view.EnableButton("VideoName");
            }
        }

        //Teardown button click
        public void OnTeardown(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            //Send server TEARDOWN message in appropriate format
            bool success = _rtspModel.SendServer("TEARDOWN", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            if (success)
            {
                //Listen for server response on RTSP and parse response
                String servResponse = _rtspModel.ListenServer();
                String[] msg = ParseServerResponse(servResponse);
                //If the rcvd message is over appropriate protocol, update server activity with msg
                if (msg[0] == "RTSP/1.0")
                {
                    UpdateServerActivity(FormatServerResponse(msg));
                }
                //Update client activity with prompt
                UpdateClientActivity("New RTSP State: TEARDOWN");
                //Reset sequence number to 1, disable all buttons except Setup and video filename
                _rtspModel.ResetSeqNum();
                _view.DisableButton("Play");
                _view.DisableButton("Pause");
                _view.DisableButton("Teardown");
                _view.EnableButton("Setup");
                _view.EnableButton("VideoName");
            } else {
                //Server disconnected; update client
                UpdateClientActivity("Server has disconnected. Please try again...");
                _view.DisableButton("Setup");
                _view.DisableButton("Play");
                _view.DisableButton("Pause");
                _view.DisableButton("Teardown");
                _view.DisableVideoView();
                _view.EnableButton("Connect");
                _view.EnableButton("VideoName");
            }
        }

        ///--------------PROTOCOL FUNCTIONS--------------///

        //RTSP Listen
        public void RTSPListen()
        {
            //Update client view with prompt
            UpdateClientActivity("Client is waiting patiently for a friend (server)...");
            //Instantiate RTSP model and connect to server
            _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
            //Try to connect to server (included error handling in event of no server)
            bool success = _rtspModel.ConnectServer();
            //If server connects
            if (success)
            {
                _view.EnableVideoView();    //Enable video area with controls
                _view.EnableButton("Setup");    //Enable Setup btn
                UpdateClientActivity("Client has connected to server.");
            }
            else //if server is not connected
            {
                //Reenable Connect to allow user to try again
                _view.EnableButton("Connect");
                UpdateClientActivity("Client cannot find requested server. Try again...");
            }
        }

        //Video playback communications
        public void PlaybackCommunications()
        {
            //Instantiate RTP model
            _rtpModel = new RTP(_view.GetPortNo(), _view.GetServIPAddr());
            Image frameImg = null;
            //loop
            while (true)
            {
                //Get bytes of frame through RTP
                frameBytes = _rtpModel.GetFrame();
                //If null, the video is over; disable Pause and break out of loop
                if (frameBytes == null)
                {
                    _view.DisableButton("Pause");
                    break;
                }
                //Create image from frame bytes
                frameImg = _rtpModel.FrameToImage(frameBytes);
                //Create header byte array, block copy from frame bytes (very nifty function)
                byte[] header = new byte[12];
                Buffer.BlockCopy(frameBytes, 0, header, 0, header.Length);
                if (_view.ShowPacketReport())   //if Packet Report is checked
                {
                    int i = 1;
                    //Grab payload type, sequence number, and timestamp from header of frame
                    int payloadType = header[i++] & 0x7f;
                    int seqNo = header[i++] << 8 | header[i++];
                    int timestamp = header[i++] << 24 | header[i++] << 16 | header[i++] << 8 | header[i++];
                    //Create packet report string from info and update client view
                    string packetReport = "Got RTP packet with SeqNum #" + seqNo + " Timestamp " + timestamp + "ms, of type " + payloadType;
                    UpdateClientActivity(packetReport);
                }
                if (_view.ShowHeader()) //if Print Header is checked
                {
                    //Convert header to bits, convert to string and print to client activity
                    string bitHeader = string.Concat(header.Select(b => Convert.ToString(b, 2)));
                    UpdateClientActivity(bitHeader);
                }
                //Set video area to current frame
                _view.SetImage(frameImg);
            }
        }

        ///--------------TEXT/TO VIEW FUNCTIONS--------------///

        //Add server activity to view
        public void UpdateServerActivity(string text)
        {
            _view.AddServerRequestText(text + "\r\n");
        }

        //Add client activity to view
        public void UpdateClientActivity(string text)
        {
            _view.AddClientActivityText(text + "\r\n");
        }

        //Parse server response
        public String[] ParseServerResponse(String msg)
        {
            //Trim message, split on delims and return string array
            msg = msg.Trim();
            char[] delim = new char[0];
            String[] brokenMsg = msg.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            return brokenMsg;
        }

        //Format server response
        public String FormatServerResponse(String[] msg)
        {
            //Format string to appropriate look and return
            String resp = msg[0] + " " + msg[1] + " " + msg[2] + "\r\n" + msg[3] + " " + msg[4] + "\r\n" + msg[5] + " " + msg[6] + "\r\n";
            return resp;
        }
    }
}
