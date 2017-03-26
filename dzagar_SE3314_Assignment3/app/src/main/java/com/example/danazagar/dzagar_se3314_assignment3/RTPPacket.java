package com.example.danazagar.dzagar_se3314_assignment3;

/**
 * Created by danazagar on 2017-03-23.
 */

public class RTPPacket {
    //Unpack frame (get rid of header)
    public byte[] UnpackFrame(byte[] frame){
        //Create new btye array for new frame, block copy everything except header into new frame
        byte[] newFrame = new byte[frame.length-12];
        System.arraycopy(frame, 12, newFrame, 0, newFrame.length);
        return newFrame;
    }
}
