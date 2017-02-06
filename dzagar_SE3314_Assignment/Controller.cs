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
        RTP _rtpModel;
        Thread listenRTSP;
        int sessionNo = 50;
        int rcvdSession;
        private static MainView _view;
        RTSP _rtspModel = null;
        Thread listenClient;

        public void onListenClick(object sender, EventArgs e)
        {
            //Find view
            _view = (MainView)((Button)sender).FindForm();

            //Spawn the listen RTSP thread
            listenRTSP = new Thread(RTSPListen);
            listenRTSP.IsBackground = true;
            //Start thread
            listenRTSP.Start();
        }
        private void RTSPListen()
        {
            //Create new RTSP
            _rtspModel = new RTSP(_view.GetPortNo());
            //Update server IP in view
            _view.SetServerIPText(_rtspModel.GetIP().ToString());
            //Loop through
            while (true)
            {
                AddServerActivity("Server is waiting for new connection.");
                Socket RTSPSocket = _rtspModel.AcceptClient(); //Wait for client
                AddServerActivity("The client " + _rtspModel.GetEndPoint().ToString() + " has joined.");
                //when client connects, create client thread
                listenClient = new Thread(new ParameterizedThreadStart(OnClientConnection));
                //start on client connection
                listenClient.IsBackground = true;
                listenClient.Start(RTSPSocket);
            }

        }

        private void OnClientConnection(Object obj)
        {
            OnClientConnection((Socket)obj);
        }

        //When client is connected...
        private void OnClientConnection(Socket sock)
        {
            //Instantiate client and RTP model
            _clientModel = new Client(sock);
            _rtpModel = new RTP();
            while (true)
            {
                try
                {
                    string type, seqNo, videoName, clientPort;
                    //Parse through client message
                    string msg = Encoding.UTF8.GetString(_clientModel.Communication());
                    AddClientActivity(msg);
                    //Split msg on non-words
                    char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
                    string[] brokenMsg = msg.Split(delimiters);
                    //Inspecting client strings, type is always first word, videoName is always 7th, and sequence # is always 13th
                    type = brokenMsg[0];
                    videoName = brokenMsg[6];
                    seqNo = brokenMsg[12];
                    if (brokenMsg.Length > 20)
                    {
                        clientPort = brokenMsg[20];
                    } else if (brokenMsg.Length > 12)
                    {
                        rcvdSession = int.Parse(brokenMsg[17]);
                    }

                    //Go through each case of types: SETUP, PLAY, PAUSE, TEARDOWN
                    switch (type)
                    {
                        case "SETUP":
                            _rtpModel.InitializeSocketAndVideo(_view.GetPortNo().ToString(), videoName);
                            AddClientActivity(videoName);
                            _clientModel.SendUTF8Response(sessionNo++, seqNo);
                            break;
                        case "PLAY":
                            AddClientActivity(seqNo);
                            _clientModel.SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.StartTime();
                            break;
                        case "PAUSE":
                            _clientModel.SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.PauseTime();
                            break;
                        case "TEARDOWN":
                            _clientModel.SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.ClearMJPEGFile();
                            _rtpModel.PauseTime();
                            _rtpModel.CloseSocketOnTeardown();
                            break;
                    }

                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                

            }
        }

        //Show update in server activity
        private void AddServerActivity(string text)
        {
            _view.AddServerActivityText(text + "\r\n");
        }
        
        //Show update in client activity
        private void AddClientActivity(string text)
        {
            _view.AddClientRequestText(text + "\r\n");
        }

    }
}
