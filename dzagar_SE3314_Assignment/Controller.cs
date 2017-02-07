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
        int clientCount = 0;

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
                AddServerActivity("Server is waiting patiently for a friend (client)...");
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
            bool isSetup = false;
            byte[] rcvBuffer = new byte[1024];
            int randInt = 0;
            int i = 0;
            MJPEGVideo currentVid = null;
            _rtpModel = null;
            Client newCli = null;
            try
            {
                while (!isSetup)
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
                        _rtpModel = new RTP(clientPortNo); //use port in this?
                        currentVid = new MJPEGVideo(brokenMsg[6]);
                        randInt = GenerateRandomInt();
                        newCli = new Client(randInt, clientPortNo, currentVid);
                        clients.Add(newCli);
                        i = clientCount++;
                        //ElapsedEventHandler sender = new ElapsedEventHandler(FileProcessingTimer);
                        clients.ElementAt(i).GetClientTimer().Elapsed += FileProcessingTimer;
                        
                        isSetup = true;
                        sock.Send(clients.ElementAt(i).ClientUTF8Response());
                    }
                    while (isSetup)
                    {
                        int evenMoreBytes = sock.Receive(rcvBuffer);
                        if (numBytes <= 0) break;
                        string anotherMsg = Encoding.UTF8.GetString(rcvBuffer, 0, numBytes);
                        string[] anotherBrokenMsg = msg.Split(delimiters);
                        string anotherRequestType = anotherBrokenMsg[0];
                        switch (anotherRequestType)
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
                //if (timer == clients.ElementAt(i).GetClientTimer())
                //{
                    RTPPacket newPacket = clients.ElementAt(i).CreatePacket();
                    _view.SetFrameNoText(clients.ElementAt(i).GetSeqNo().ToString());
                    if (_view.ShowRTPHeader()) AddServerActivity(newPacket.GetPacketHeader().ToString());
                    clients.ElementAt(i).GetClientRTP().SendPacketViaUDP(newPacket.GetPacketBytes());
                //}
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
