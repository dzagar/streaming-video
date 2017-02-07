using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Timers;

namespace dzagar_SE3314_Assignment
{
    public class Controller
    {
        private static MainView _view;  //reference to Main View
        private static readonly Random rnd = new Random();  //For RNG (below)
        List<int> uniqueClientIDs = new List<int>();    //To make sure IDs are unique
        RTSP _rtspModel = null; //One instance of RTSP
        Thread listenRTSP;  //RTSP listening thread
        List<Client> clients = new List<Client>();     //List of clients to handle
        Thread listenClient;    //Client listening thread
        RTP _rtpModel;  //RTP model (used across clients)
        int clientCount = 0;    //Keeps track of # clients, to index client list properly

        public void OnListen(object sender, EventArgs e)        //On Listen, find View reference and start listening RTSP
        {
            //Initialize view
            _view = (MainView)((Button)sender).FindForm();
            //Spawn new RTSP listening thread
            listenRTSP = new Thread(RTSPListen);
            listenRTSP.IsBackground = true;
            listenRTSP.Start();
        }

        public void RTSPListen()
        {
            AddServerActivity("Server is waiting patiently for a friend (client)...");
            _rtspModel = new RTSP(_view.GetPortNo());
            _view.SetServerIPText(_rtspModel.GetIP().ToString());
            _view.SetFrameNoText("0");
            while (true)
            {
                //Wait for new client to be accepted
                Socket RTSPSocket = _rtspModel.AcceptClient();
                AddServerActivity("Client at " + RTSPSocket.RemoteEndPoint.ToString() + " has joined.");
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
            byte[] rcvBuffer = new byte[1024];
            int randInt = 0;
            int i = 0;
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
                    AddClientActivity(msg);
                    char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
                    string[] brokenMsg = msg.Split(delimiters);
                    string requestType = brokenMsg[0];
                    if (requestType == "SETUP")
                    {
                        char[] equals = { '=' };
                        string[] temp = msg.Split(equals);
                        int clientPortNo = int.Parse(temp[1]);
                        _rtpModel = new RTP(clientPortNo); 
                        currentVid = new MJPEGVideo(brokenMsg[6]);
                        randInt = GenerateRandomInt();
                        newCli = new Client(randInt, clientPortNo, currentVid);
                        clients.Add(newCli);
                        i = clientCount++;
                        ElapsedEventHandler sender = new ElapsedEventHandler(FileProcessingTimer);
                        clients.ElementAt(i).GetClientTimer().Elapsed += FileProcessingTimer;
                        sock.Send(clients.ElementAt(i).ClientUTF8Response());
                    } else
                    {
                        Console.WriteLine(requestType);
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
                                break;
                            default:
                                break;
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
            System.Timers.Timer timer = (System.Timers.Timer)source;
            int i = 0;
            while (i < clients.Count)
            {
                if (clients.ElementAt(i).GetClientTimer().Equals(timer))
                {
                    RTPPacket newPacket = clients.ElementAt(i).CreatePacket();
                    _view.SetFrameNoText(clients.ElementAt(i).GetSeqNo().ToString());
                    if (_view.ShowRTPHeader()) AddServerActivity(newPacket.GetPacketHeader().ToList().ToString());
                    if (newPacket.GetPacketBytes() == null) clients.ElementAt(i).InitiateTeardown();
                    else clients.ElementAt(i).GetClientRTP().SendPacketViaUDP(newPacket.GetPacketBytes());
                }
                i++;
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
