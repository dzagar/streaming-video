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
    InetSocketAddress endPointServ;
    DatagramSocket framesFromServCli = null;
    RTPPacket _rtpPacket = null;

    public RTP (int port, InetAddress servIP){
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
            int length = framesFromServCli.getReceiveBufferSize();
            Log.d("RTP", "Buffer size: " + length);
            byte[] vidFramePkt = new byte[length];
            DatagramPacket pkt = new DatagramPacket(vidFramePkt, vidFramePkt.length);
            pkt.setData(vidFramePkt);
            framesFromServCli.receive(pkt); //hangs here
            if (vidFramePkt.length > 0){
                Log.d("RTP", "Returning video frame bytes");
                return vidFramePkt;
            } else {
                Log.d("RTP", "Not returning shit");
                return null;
            }
        } catch (SocketException e){
            return null;
        } catch (IOException e){
            return null;
        }
    }

    public Bitmap FrameToImage(byte[] frame){
        byte[] vidFramePkt = _rtpPacket.UnpackFrame(frame);
        try {
            Bitmap bmp = BitmapFactory.decodeByteArray(vidFramePkt, 0, vidFramePkt.length);
            return bmp;
        } catch (Exception e){
            return null;
        }
    }
}
