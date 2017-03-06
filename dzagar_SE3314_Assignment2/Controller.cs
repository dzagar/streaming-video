using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Net.Sockets;

namespace dzagar_SE3314_Assignment2
{
    class Controller
    {
        private static View _view; //reference to main View
        RTSP _rtspModel = null;
        RTP _rtpModel = null;
        String sessionNo = "";
        Thread videoPlaybackThread = null;
        byte[] frameBytes;

        //Connect button click
        public void OnConnect(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _view.DisableButton("Connect");
            RTSPListen();
            _view.EnableVideoView();
            _view.EnableButton("Setup");
        }

        //Exit button click
        public void OnExit(object sender, EventArgs e)
        {
            if (videoPlaybackThread != null)
            {
                videoPlaybackThread.Abort();
            }
            Application.Exit();
        }

        //Setup button click
        public void OnSetup(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _rtspModel.SendServer("SETUP", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), "no");
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            sessionNo = msg[6];
            UpdateServerActivity(FormatServerResponse(msg));
            UpdateClientActivity("New RTSP State: READY\r\n");
            _view.DisableButton("Setup");
            _view.DisableButton("VideoName");
            _view.EnableButton("Play");
            _view.ResetImage();
        }

        //Play button click
        public void OnPlay(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _rtspModel.SendServer("PLAY", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            if (msg[0] == "RTSP/1.0")
            {
                UpdateServerActivity(FormatServerResponse(msg));
            }
            if (videoPlaybackThread == null)
            {
                videoPlaybackThread = new Thread(PlaybackCommunications);
                videoPlaybackThread.IsBackground = true;
                videoPlaybackThread.Start();
            }
            UpdateClientActivity("New RTSP State: PLAYING\r\n");
            _view.DisableButton("Play");
            _view.EnableButton("Pause");
            _view.EnableButton("Teardown");
        }

        //Pause button click
        public void OnPause(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _rtspModel.SendServer("PAUSE", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            if (msg[0] == "RTSP/1.0")
            {
                UpdateServerActivity(FormatServerResponse(msg));
            }

            UpdateClientActivity("New RTSP State: PAUSED\r\n");
            _view.DisableButton("Pause");
            _view.EnableButton("Play");
        }

        //Teardown button click
        public void OnTeardown(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _rtspModel.SendServer("TEARDOWN", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            if (msg[0] == "RTSP/1.0")
            {
                UpdateServerActivity(FormatServerResponse(msg));
            }
            UpdateClientActivity("New RTSP State: TEARDOWN\r\n");
            _rtspModel.ResetSeqNum();
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.EnableButton("Setup");
            _view.EnableButton("VideoName");
        }

        //Update server activity
        public void UpdateServerActivity(string text)
        {
            _view.AddServerRequestText(text + "\r\n");
        }

        //Add client activity to view
        public void UpdateClientActivity(string text)
        {
            _view.AddClientActivityText(text + "\r\n");
        }

        //Video playback communications
        public void PlaybackCommunications()
        {
            _rtpModel = new RTP(_view.GetPortNo(), _view.GetServIPAddr());
            Image frameImg = null;
            //loop
            while (true)
            {
                frameBytes = _rtpModel.GetFrame();
                if (frameBytes == null)
                {
                    _view.DisableButton("Pause");
                    break;
                }
                frameImg = _rtpModel.FrameToImage(frameBytes);
                byte[] header = new byte[12];
                Buffer.BlockCopy(frameBytes, 0, header, 0, header.Length);
                if (_view.ShowPacketReport())
                {
                    //figure this out
                }
                if (_view.ShowHeader())
                {
                    
                }
                _view.SetImage(frameImg);
            }
        }

        //RTSP Listen
        public void RTSPListen()
        {
            UpdateClientActivity("Client is waiting patiently for a friend (server)...");
            _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
            _rtspModel.ConnectServer();
            UpdateClientActivity("Client has connected to server.");
        }

        //Parse server response
        public String[] ParseServerResponse(String msg)
        {
            msg = msg.Trim();
            char[] delim = new char[0];
            String[] brokenMsg = msg.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            return brokenMsg;
        }

        //Format server response
        public String FormatServerResponse(String[] msg)
        {
            String resp = msg[0] + " " + msg[1] + " " + msg[2] + "\r\n" + msg[3] + " " + msg[4] + "\r\n" + msg[5] + " " + msg[6] + "\r\n";
            return resp;
        }
    }
}
