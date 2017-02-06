using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace dzagar_SE3314_Assignment
{
    public class Controller
    {
        List<Client> clients;
        Client _clientModel;
        Thread listenRTSP;
        private static MainView _view;
        RTSP _rtspModel = null;
        Thread listenClient;

        public void onListenClick(object sender, EventArgs e)
        {
            //Find view
            _view = (MainView)((Button)sender).FindForm();

            //Spawn the listen RTSP thread
            this.listenRTSP = new Thread(RTSPListen);

            //Start thread
            this.listenRTSP.Start();
        }
        private void RTSPListen()
        {
            //Create new RTSP
            _rtspModel = new RTSP();
            //Loop through until client connects
            while (true)
            {
                //Socket RTSPSocket = _rtspModel.AcceptClientConnection()??? or something
                //when client connects, create client thread
                //listenClient = new Thread(hfdlshfjkdsl)
                //start on client connection

            }

        }

        //On Client Connection function
        private void OnClientConnection()
        {

        }




    }
}
