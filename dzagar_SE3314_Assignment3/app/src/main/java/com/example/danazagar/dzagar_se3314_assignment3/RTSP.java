package com.example.danazagar.dzagar_se3314_assignment3;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;


/**
 * Created by danazagar on 2017-03-23.
 */

public class RTSP {
    InetAddress ipAddrServ; //server ip
    int portNo; //port number
    Socket RTSPSock = null; //RTSP communication socket
    InetSocketAddress endPointServ; //server endpoint
    int CSeqNum;    //sequence number of msgs
    byte[] rcvBuffer;   //rcving buffer to send over RTSP

    public RTSP(int port, InetAddress servIP){
        //Instantiate port num, server IP, sequence num, server endpoint, RTSP socket and buffer
        portNo = port;
        ipAddrServ = servIP;
        CSeqNum = 1;
        endPointServ = new InetSocketAddress(ipAddrServ, portNo);
        RTSPSock = new Socket();
        rcvBuffer = new byte[2048];
    }

    //Connect to server
    public Boolean ConnectServer(){
        try {
            RTSPSock.connect(endPointServ); //connect to endpoint
            return true;
        } catch (IOException e){
            if (RTSPSock != null){
                try {
                    RTSPSock.close();
                } catch (IOException f){
                    return false;
                }
            }
            return false;
        }
    }

    public boolean SendServer(String action, int port, String vidName, InetAddress servIP, String sessionNo){
        String portStr = Integer.toString(port);
        String servIPStr = servIP.getHostAddress();
        //Craft message to be sent to server
        String msg = action + " rtsp://" + servIPStr + ":" + portStr + "/" + vidName + " RTSP/1.0\r\n" + "CSeq: " + CSeqNum + "\r\n";
        if (action == "SETUP" || sessionNo == "no")
        {
            msg += "Transport: RTP/UDP; client_port= 25000";
        } else
        {
            msg += "Session: " + sessionNo;
        }
        //Increment sequence num of msgs
        CSeqNum++;
        try {
            //Convert msg to bytes and send to server
            rcvBuffer = msg.getBytes();
            DataOutputStream dos = new DataOutputStream(RTSPSock.getOutputStream());
            dos.write(rcvBuffer);
            return true;
        } catch (IOException e){
            return false;
        }
    }

    //Reset sequence num to 1
    public void ResetSeqNum(){
        CSeqNum = 1;
    }

    //Listen to server
    public String ListenServer()
    {
        try {
            //Rcv server message
            DataInputStream dis = new DataInputStream(RTSPSock.getInputStream());
            dis.read(rcvBuffer);
            return new String(rcvBuffer,"UTF-8");
        } catch (IOException e){
            return "Error on socket";
        }
    }
}
