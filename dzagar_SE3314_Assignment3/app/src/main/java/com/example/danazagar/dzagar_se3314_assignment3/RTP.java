package com.example.danazagar.dzagar_se3314_assignment3;

/**
 * Created by danazagar on 2017-03-23.
 */

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.SocketException;

public class RTP {
    InetSocketAddress endPointServ; //server endpoint
    DatagramSocket framesFromServCli = null;    //frames from server to client
    RTPPacket _rtpPacket = null;    //one instance of RTP packet

    public RTP (int port, InetAddress servIP){
        //Instantiate server endpoint, UDP socket, rtp packet
        endPointServ = new InetSocketAddress(servIP, port);
        try {
            framesFromServCli = new DatagramSocket(25000);
        } catch (SocketException e){
            Log.d("RTP", "Frame socket null");
            framesFromServCli = null;
        }
        _rtpPacket = new RTPPacket();
    }

    public byte[] GetFrame(){

        try {
            //Rcv packetized video frame from server
            int length = framesFromServCli.getReceiveBufferSize();
            Log.d("RTP", "Buffer size: " + length);
            byte[] vidFramePkt = new byte[length];
            DatagramPacket pkt = new DatagramPacket(vidFramePkt, vidFramePkt.length);
            pkt.setData(vidFramePkt);
            framesFromServCli.receive(pkt);
            //If rcvd, return, otherwise return null
            if (vidFramePkt.length > 0){
                return vidFramePkt;
            } else {
                return null;
            }
        } catch (SocketException e){
            return null;
        } catch (IOException e){
            return null;
        }
    }

    //Frame to image
    public Bitmap FrameToImage(byte[] frame){
        //Create unpacketized frame byte array
        byte[] vidFramePkt = _rtpPacket.UnpackFrame(frame);
        try {
            //Convert unpacketized frame byte array to bitmap and return
            Bitmap bmp = BitmapFactory.decodeByteArray(vidFramePkt, 0, vidFramePkt.length);
            return bmp;
        } catch (Exception e){
            return null;
        }
    }
}
