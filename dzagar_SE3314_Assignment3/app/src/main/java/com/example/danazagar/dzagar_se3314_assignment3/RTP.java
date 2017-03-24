package com.example.danazagar.dzagar_se3314_assignment3;

/**
 * Created by danazagar on 2017-03-23.
 */
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.SocketException;

public class RTP {
    InetSocketAddress endPointServ;
    DatagramSocket framesFromServCli;
    RTPPacket _rtpPacket = null;

    public RTP (int port, InetAddress servIP){
        endPointServ = new InetSocketAddress(servIP, port);
        try {
            framesFromServCli = new DatagramSocket(25000);
        } catch (SocketException e){
            framesFromServCli = null;
        }
        _rtpPacket = new RTPPacket();
    }

    public byte[] GetFrame(){

        try {
            int length = framesFromServCli.getReceiveBufferSize();
            byte[] vidFramePkt = new byte[length];
            DatagramPacket pkt = new DatagramPacket(vidFramePkt, vidFramePkt.length);
            framesFromServCli.receive(pkt);
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

    public Bitmap FrameToImage(byte[] frame){
        byte[] vidFramePkt = _rtpPacket.UnpackFrame(frame);
        try {
            Bitmap bmp = BitmapFactory.decodeByteArray(frame, 0, frame.length);
            return bmp;
        } catch (Exception e){
            return null;
        }
    }
}
