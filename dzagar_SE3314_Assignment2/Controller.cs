using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace dzagar_SE3314_Assignment2
{
    class Controller
    {
        private static View _view; //reference to main View
        RTSP _rtspModel = null;
        RTP _rtpModel = null;
        String sessionNo = "";

        //Connect button click
        public void OnConnect(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _view.DisableButton("Connect");
            RTSPListen();
            _view.EnableButton("Setup");
        }

        //Exit button click
        public void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Setup button click
        public void OnSetup(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _rtspModel.SendServer("SETUP", GetPortNo(), GetVideoFilename(), GetServIPAddr(), 0);
            String servResponse = _rtspModel.ListenServer();
            UpdateServerActivity(ParseServerResponse(servResponse));
            UpdateClientActivity("New RTSP State: READY\r\n");
            _view.DisableButton("Setup");
            _view.EnableButton("Play");
        }

        //Play button click
        public void OnPlay(object sender, EventArgs e)
        {

        }

        //Pause button click
        public void OnPause(object sender, EventArgs e)
        {

        }

        //Teardown button click
        public void OnTeardown(object sender, EventArgs e)
        {

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

        }

        //RTSP Listen
        public void RTSPListen()
        {
            UpdateClientActivity("Client is waiting patiently for a friend (server)...");
            _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
            _rtspModel.ConnectServer();
            UpdateClientActivity("Client has connected to server.");
        }

        //Get port number
        public int GetPortNo()
        {
            return _view.GetPortNo();
        }

        //Get video name
        public string GetVideoFilename()
        {
            return _view.GetVideoFilename();
        }

        //Get Server IP
        public IPAddress GetServIPAddr()
        {
            return _view.GetServIPAddr();
        }

        //Parse server response
        public String ParseServerResponse(String msg)
        {
            msg = msg.Trim();
            char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
            String[] brokenMsg = msg.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            sessionNo = brokenMsg[6];
            msg = brokenMsg[0] + " " + brokenMsg[1] + " " + brokenMsg[2] + "\r\n";
            msg += brokenMsg[3] + " " + brokenMsg[4] + "\r\n";
            msg += brokenMsg[5] + " " + brokenMsg[6];
            return msg;
        }
    }
}
