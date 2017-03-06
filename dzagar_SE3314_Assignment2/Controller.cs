using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace dzagar_SE3314_Assignment2
{
    class Controller
    {
        private static View _view; //reference to main View
        RTSP _rtspModel = null;
        RTP _rtpModel = null;

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
    }
}
