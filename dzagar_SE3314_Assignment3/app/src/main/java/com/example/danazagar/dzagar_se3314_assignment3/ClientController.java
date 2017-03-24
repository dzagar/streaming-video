package com.example.danazagar.dzagar_se3314_assignment3;

import android.graphics.Bitmap;
import android.os.AsyncTask;
/**
 * Created by danazagar on 2017-03-23.
 */

public class ClientController{
    MainActivity _view;
    RTSP _rtspModel = null;
    RTP _rtpModel = null;
    String sessionNo = "";
    Thread videoPlaybackThread = null;
    byte[] frameBytes;

    public ClientController(MainActivity view){
        _view = view;
    }

    public void OnConnect(){
        _view.DisableButton("Connect");
        RTSPListen();
    }

    public void OnExit(){
        if (videoPlaybackThread != null)
        {
            videoPlaybackThread.interrupt();
        }
    }

    public void OnSetup(){
        boolean success = _rtspModel.SendServer("SETUP", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), "no");
        if (success){
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            sessionNo = msg[6];
            _view.DisableButton("Setup");
            _view.DisableButton("VideoName");
            _view.EnableButton("Play");
        } else {
            _view.DisableButton("Setup");
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.DisableVideoView();
            _view.EnableButton("Connect");
            _view.EnableButton("VideoName");
        }
    }

    public void OnPlay(){
        boolean success = _rtspModel.SendServer("PLAY", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
        if (success){
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            if (videoPlaybackThread == null)
            {
                videoPlaybackThread = new Thread(new PlaybackCommunications());
                videoPlaybackThread.start();
            }
            _view.DisableButton("Play");
            _view.EnableButton("Pause");
            _view.EnableButton("Teardown");
        } else {
            _view.DisableButton("Setup");
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.DisableVideoView();
            _view.EnableButton("Connect");
            _view.EnableButton("VideoName");
        }
    }

    public void OnPause(){
        boolean success = _rtspModel.SendServer("PAUSE", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
        if(success){
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            _view.DisableButton("Pause");
            _view.EnableButton("Play");
        } else {
            _view.DisableButton("Setup");
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.DisableVideoView();
            _view.EnableButton("Connect");
            _view.EnableButton("VideoName");
        }
    }

    public void OnTeardown(){
        boolean success = _rtspModel.SendServer("TEARDOWN", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
        if(success){
            String servResponse = _rtspModel.ListenServer();
            String[] msg = ParseServerResponse(servResponse);
            _rtspModel.ResetSeqNum();
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.EnableButton("Setup");
            _view.EnableButton("VideoName");
        } else {
            _view.DisableButton("Setup");
            _view.DisableButton("Play");
            _view.DisableButton("Pause");
            _view.DisableButton("Teardown");
            _view.DisableVideoView();
            _view.EnableButton("Connect");
            _view.EnableButton("VideoName");
        }
    }

    public void OnVideoTap(){

    }

    private void RTSPListen(){
        _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
        AsyncTask<Void, Void, Void> connectServer = new AsyncTask<Void, Void, Void>(){
            @Override
            protected Void doInBackground(Void... params){
                boolean success = _rtspModel.ConnectServer();
                if (success)
                {
                    _view.EnableVideoView();    //Enable video area with controls
                    _view.EnableButton("Setup");    //Enable Setup btn
                    return null;
                }
                else //if server is not connected
                {
                    //Reenable Connect to allow user to try again
                    _view.EnableButton("Connect");
                    return null;
                }
            }
        };
    }

    private String[] ParseServerResponse(String msg){
        return new String[1];
    }

    private String FormatServerResponse(String[] msg){
        return new String();
    }

    private class PlaybackCommunications implements Runnable {
        public void run(){
            _rtpModel = new RTP(_view.GetPortNo(), _view.GetServIPAddr());
            Bitmap frameImg = null;
            while (true)
            {
                //Get bytes of frame through RTP
                frameBytes = _rtpModel.GetFrame();
                //If null, the video is over; disable Pause and break out of loop
                if (frameBytes == null)
                {
                    _view.DisableButton("Pause");
                    break;
                }
                //Create image from frame bytes
                frameImg = _rtpModel.FrameToImage(frameBytes);
                //Set video area to current frame
                _view.SetImage(frameImg);
            }
        }
    }


}
