package com.example.danazagar.dzagar_se3314_assignment3;

import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.os.Handler;
import android.util.Log;

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

    Handler uiHandler;

    public ClientController(MainActivity view, Handler mHandler){
        _view = view;
        uiHandler = mHandler;
    }

    public void OnConnect(){
        AsyncTask<Void, Void, Boolean> connectServer = new AsyncTask<Void, Void, Boolean>(){
            @Override
            protected void onPreExecute(){
                _view.DisableButton("Connect");
                Log.d("CONNECT", "calling RTSPListen");
            }
            @Override
            protected Boolean doInBackground(Void... params){
                Log.d("CONNECT", "Port no: " + _view.GetPortNo() + " ServIP: " + _view.GetServIPAddr().toString());
                _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
                boolean success = _rtspModel.ConnectServer();
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if (result)
                {
                    Log.d("CONNECT", "post execute worked");
                    _view.EnableVideoView();    //Enable video area with controls
                    _view.EnableButton("Setup");    //Enable Setup btn
                }
                else //if server is not connected
                {
                    //Reenable Connect to allow user to try again
                    Log.d("CONNECT", "post execute got false");
                    _view.EnableButton("Connect");
                }
            }
        };
        connectServer.execute();

    }

    public void OnExit(){
        if (videoPlaybackThread != null)
        {
            videoPlaybackThread.interrupt();
        }
    }

    public void OnSetup(){
        AsyncTask<Void, Void, Boolean> setup = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                Log.d("SETUP","entered do in bg");
                boolean success = _rtspModel.SendServer("SETUP", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), "no");
                String response = _rtspModel.ListenServer();
                sessionNo = GetServerSessionNo(response);
                Log.d("SETUP", "Session: " + sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if (result){
                    Log.d("SETUP", "setup success");
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
        };
        setup.execute();
    }

    public void OnPlay(){
        AsyncTask<Void, Void, Boolean> play = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                boolean success = _rtspModel.SendServer("PLAY", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if (result){
                    if (videoPlaybackThread == null)
                    {
                        Log.d("PLAY", "Gonna start new thread");
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
        };
        play.execute();
    }

    public void OnPause(){
        AsyncTask<Void, Void, Boolean> pause = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                boolean success = _rtspModel.SendServer("PAUSE", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if(result){
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
        };
        pause.execute();
    }

    public void OnTeardown(){
        AsyncTask<Void, Void, Boolean> teardown = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                boolean success = _rtspModel.SendServer("TEARDOWN", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if(result){
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
        };
        teardown.execute();
    }

    public void OnVideoTap(){
        uiHandler.post(new Runnable(){
            public void run(){
                _view.ShowHoverIcons();
            }
        });
        uiHandler.postDelayed(new Runnable(){
            public void run(){
                _view.HideHoverIcons();
            }
        }, 3000);
    }

    private class PlaybackCommunications implements Runnable {
        Bitmap frameImg = null;
        public void run(){
            _rtpModel = new RTP(_view.GetPortNo(), _view.GetServIPAddr());
            Log.d("PC", "RUNNING RUNNABLE");
            frameImg = null;
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
                uiHandler.post(new Runnable(){
                    public void run(){
                        //Set video area to current frame
                        _view.SetImage(frameImg);
                    }
                });
            }
        }
    }

    //Parse server response
    public String GetServerSessionNo(String msg)
    {
        //Trim message, split and return string array
        msg = msg.trim();
        String[] brokenMsg = msg.split("\\s+");
        return brokenMsg[6];
    }


}
