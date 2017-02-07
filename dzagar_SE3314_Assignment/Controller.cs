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
        private static readonly Random rnd = new Random();  //For RNG (below..)
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
            _view.DisableListenButton();
            //Spawn new RTSP listening thread
            listenRTSP = new Thread(RTSPListen);
            listenRTSP.IsBackground = true;
            listenRTSP.Start();
        }

        public void RTSPListen()
        {
            AddServerActivity("Server is waiting patiently for a friend (client)...");
            //Instantiate RTSP with port number
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

        private void ClientConnection(Object obj)   //Call proper fcn
        {
            ClientConnection((Socket)obj);
        }

        private void ClientConnection(Socket sock)      //Maintain client communication!
        {
            //Initialize necessary vars: rcvBuffer for receiving bytes from socket, new client, RTP model, current video etc
            byte[] rcvBuffer = new byte[1024];
            int i = 0;
            MJPEGVideo currentVid = null;
            _rtpModel = null;
            Client newCli = null;
            try
            {
                while (true)
                {
                    //Receive bytes; if not receiving, break
                    int numBytes = sock.Receive(rcvBuffer);
                    if (numBytes <= 0) break;
                    //Get UTF8 string, print to view
                    string msg = Encoding.UTF8.GetString(rcvBuffer, 0, numBytes);
                    AddClientActivity(msg);
                    //Break down message to find request type
                    char[] delimiters = { ',', ':', ';', '/', '\n', '\r', ' ' };
                    string[] brokenMsg = msg.Split(delimiters);
                    string requestType = brokenMsg[0];
                    //if SETUP, do SETUP!
                    if (requestType == "SETUP")
                    {
                        //index 20 is client port #
                        int clientPortNo = int.Parse(brokenMsg[20]);
                        //Create RTP model with client port
                        _rtpModel = new RTP(clientPortNo); 
                        //Create new video with video name (index 6)
                        currentVid = new MJPEGVideo(brokenMsg[6]);
                        //Create new client and add to client list
                        newCli = new Client(GenerateRandomInt(), clientPortNo, currentVid);
                        clients.Add(newCli);
                        i = clientCount++;
                        //Add timer to elapsed (client timer) delegate
                        clients.ElementAt(i).GetClientTimer().Elapsed += FileProcessingTimer;
                        //Send client response
                        sock.Send(clients.ElementAt(i).ClientUTF8Response());
                    } else
                    {
                        switch (requestType)
                        {
                            case "PLAY":
                                //Send client response and start timer
                                sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                clients.ElementAt(i).StartClientTimer();
                                break;
                            case "PAUSE":
                                //Send client response and stop timer
                                sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                clients.ElementAt(i).StopClientTimer();
                                break;
                            case "TEARDOWN":
                                //Send client response, initiate teardown and remove client ID from list
                                sock.Send(clients.ElementAt(i).ClientUTF8Response());
                                clients.ElementAt(i).InitiateTeardown();
                                uniqueClientIDs.Remove(clients.ElementAt(i).GetClientID());
                                break;
                            default:
                                //Just in case..
                                break;
                        }
                    }
                }
            } catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                //Safely close socket
                if (sock != null) sock.Close();
            } finally
            {
                //Stop timer and safely close socket no matter what
                clients.ElementAt(i).StopClientTimer();
                if (sock != null) sock.Close();
            }

        }

        private int GenerateRandomInt()     //Generate unique client identifier
        {
            int newRand = rnd.Next();
            while (uniqueClientIDs.Contains(newRand))   //If random number already exists in list
            {
                newRand = rnd.Next();
            }
            uniqueClientIDs.Add(newRand);   //Add to list
            return newRand;
        }

        private void FileProcessingTimer(Object source, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)source;
            int i = 0;
            while (i < clients.Count)   //for each client
            {
                if (clients.ElementAt(i).GetClientTimer().Equals(timer))    //If the timers match
                {
                    //Create packet
                    RTPPacket newPacket = clients.ElementAt(i).CreatePacket();
                    _view.SetFrameNoText(clients.ElementAt(i).GetSeqNo().ToString());
                    if (_view.ShowRTPHeader())      //If user has checked "Print Header" in view
                    {
                        //Display header as binary string (used Linq)
                        AddServerActivity(string.Concat(newPacket.GetPacketHeader().Select(b => Convert.ToString(b, 2))));
                    }
                    //Teardown video if there are no bytes left
                    if (newPacket.GetPacketBytes() == null)
                    {
                        clients.ElementAt(i).InitiateTeardown();
                    }
                    else
                    {
                        //SEND DA PACKET!
                        clients.ElementAt(i).GetClientRTP().SendPacketViaUDP(newPacket.GetPacketBytes());
                    }
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
