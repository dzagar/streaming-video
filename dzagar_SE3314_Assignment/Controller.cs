using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Timers;

namespace dzagar_SE3314_Assignment
{
    public class Controller
    {
        private static MainView _view;
        private static readonly Random rnd = new Random();
        List<int> uniqueClientIDs = new List<int>();
        RTSP _rtspModel = null;
        Thread listenRTSP;
        List<Client> clients = new List<Client>();
        Thread listenClient;
        RTP _rtpModel;

        public void OnListen(object sender, EventArgs e)
        {
            //Initialize view
            _view = (MainView)((Button)sender).FindForm();
            //Spawn new RTSP thread
            listenRTSP = new Thread(RTSPListen);
            listenRTSP.IsBackground = true;
            listenRTSP.Start();
        }

        public void RTSPListen()
        {
            _rtspModel = new RTSP(_view.GetPortNo());
            while (true)
            {
                //Wait for new client to be accepted
                Socket RTSPSocket = _rtspModel.AcceptClient();
                //Spawn new client thread
                listenClient = new Thread(new ParameterizedThreadStart(ClientConnection));
                listenClient.IsBackground = true;
                listenClient.Start(RTSPSocket);
            }
        }

        private void ClientConnection(Object obj)
        {
            ClientConnection((Socket)obj);
        }

        private void ClientConnection(Socket sock)
        {
            bool isSetup = false;
            byte[] rcvBuffer = new byte[1024];
            int randInt = 0;
            int i = 0; //current index
            MJPEGVideo currentVid = null;
            _rtpModel = null;
            Client newCli = null;
            try
            {
                while (true)
                {
                    int numBytes = sock.Receive(rcvBuffer);
                    if (numBytes <= 0) break;
                    string msg = Encoding.UTF8.GetString(rcvBuffer, 0, numBytes);
                    char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
                    string[] brokenMsg = msg.Split(delimiters);
                    string requestType = brokenMsg[0];
                    if (requestType == "SETUP" && !isSetup)
                    {
                        char[] equals = { '=' };
                        string[] temp = msg.Split(equals);
                        int clientPortNo = int.Parse(temp[1]);
                        _rtpModel = new RTP(); //use port in this?
                        currentVid = new MJPEGVideo(brokenMsg[6]);
                        randInt = GenerateRandomInt();
                        newCli = new Client(randInt, clientPortNo, currentVid);
                        clients.Add(newCli);
                        i++;
                        ElapsedEventHandler sender = new ElapsedEventHandler(FileProcessingTimer);
                        clients.ElementAt(i).GetClientTimer().Elapsed += sender;
                        isSetup = true;
                        sock.Send(clients.ElementAt(i).ClientUTF8Response());
                    } else
                    {
                        while (isSetup)
                        {
                            switch (requestType)
                            {
                                case "PLAY":
                                    sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                    clients.ElementAt(i).StartClientTimer();
                                    break;
                                case "PAUSE":
                                    sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                    clients.ElementAt(i).StopClientTimer();
                                    break;
                                case "TEARDOWN":
                                    sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                    clients.ElementAt(i).InitiateTeardown();
                                    uniqueClientIDs.Remove(clients.ElementAt(i).GetClientID());
                                    isSetup = false;
                                    break;
                            }
                        }
                    }

                }
            } catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                if (sock != null) sock.Close();
            } finally
            {
                clients.ElementAt(i).StopClientTimer();
                sock.Close();
            }

        }

        private int GenerateRandomInt()
        {
            int newRand = rnd.Next();
            while (uniqueClientIDs.Contains(newRand))
            {
                newRand = rnd.Next();
            }
            uniqueClientIDs.Add(newRand);
            return newRand;
        }

        private void FileProcessingTimer(Object source, ElapsedEventArgs e)
        {

        }


        /*RTP _rtpModel;
        Thread listenRTSP;
        int sessionNo = 50;
        int rcvdSession;
        private static MainView _view;
        RTSP _rtspModel = null;
        Thread listenClient;
        string type, seqNo, videoName, clientPort;
        List<Client> clients = new List<Client>();
        int clientCount = 0;

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
            Client shinyClient = new Client(sock);
            clients.Add(shinyClient);
            _rtpModel = new RTP();
            int i = clientCount++;
            while (true)
            {
                bool quit = false;
                try
                {
                    //Parse through client message
                    string msg = Encoding.UTF8.GetString(clients.ElementAt(i).Communication());
                    AddClientActivity(msg);
                    //Split msg on non-words
                    char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
                    string[] brokenMsg = msg.Split(delimiters);
                    //Inspecting client strings, type is always first word, videoName is always 7th, and sequence # is always 13th
                    
                    if (brokenMsg.Length > 20)
                    {
                        type = brokenMsg[0];
                        videoName = brokenMsg[6];
                        seqNo = brokenMsg[12];
                        clientPort = brokenMsg[20];
                    } else if (brokenMsg.Length > 12)
                    {
                        type = brokenMsg[0];
                        videoName = brokenMsg[6];
                        seqNo = brokenMsg[12];
                        rcvdSession = int.Parse(brokenMsg[17]);
                    }
                    Console.WriteLine(type);
                    //Go through each case of types: SETUP, PLAY, PAUSE, TEARDOWN
                    switch (type)
                    {
                        case "SETUP":
                            _rtpModel.InitializeSocketAndVideo(_view.GetPortNo().ToString(), videoName);
                            AddClientActivity(videoName);
                            clients.ElementAt(i).SendUTF8Response(sessionNo++, seqNo);
                            break;
                        case "PLAY":
                            AddClientActivity(seqNo);
                            clients.ElementAt(i).SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.StartTime();
                            break;
                        case "PAUSE":
                            clients.ElementAt(i).SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.PauseTime();
                            break;
                        case "TEARDOWN":
                            clients.ElementAt(i).SendUTF8Response(rcvdSession, seqNo);
                            _rtpModel.ClearMJPEGFile();
                            _rtpModel.PauseTime();
                            _rtpModel.CloseSocketOnTeardown();
                            break;
                    }

                } catch (SocketException e)
                {
                    if (sock != null)
                    {
                        sock.Close();
                    }
                } finally
                {
                    _rtpModel.PauseTime();
                    _rtpModel.CloseSocketOnTeardown();
                    sock.Close();
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
        }*/

    }
}
