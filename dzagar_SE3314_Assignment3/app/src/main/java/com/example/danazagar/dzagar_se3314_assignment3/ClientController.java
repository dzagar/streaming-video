package com.example.danazagar.dzagar_se3314_assignment3;

import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.os.Handler;

/**
 * Created by danazagar on 2017-03-23.
 */

public class ClientController{
    MainActivity _view; //reference to main view
    RTSP _rtspModel = null; //one instance of RTSP
    RTP _rtpModel = null;   //one instance of RTP
    String sessionNo = "";  //session number
    Thread videoPlaybackThread = null;  //server listening thread
    byte[] frameBytes;  //frame in bytes

    Handler uiHandler;  //handler for posting to Main Activity

    public ClientController(MainActivity view, Handler mHandler){
        _view = view;  //set view
        uiHandler = mHandler;   //set handler
    }


    ///--------------BUTTON FUNCTIONS--------------///


    public void OnConnect(){    //Connect button click
        AsyncTask<Void, Void, Boolean> connectServer = new AsyncTask<Void, Void, Boolean>(){
            @Override
            protected void onPreExecute(){
                //Disable connect button while attempting to connect
                _view.DisableButton("Connect");
            }
            @Override
            protected Boolean doInBackground(Void... params){   //RTSP Listen
                //Instantiate RTSP model and connect to server
                _rtspModel = new RTSP(_view.GetPortNo(), _view.GetServIPAddr());
                boolean success = _rtspModel.ConnectServer();
                //Return outcome (true if successful, false if not)
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){   //Interact with view
                if (result)
                {
                    _view.EnableVideoView();    //Enable video area with controls
                    _view.EnableButton("Setup");    //Enable Setup btn
                }
                else //if server is not connected
                {
                    //Re-enable Connect to allow user to try again
                    _view.EnableButton("Connect");
                }
            }
        };
        connectServer.execute();

    }

    public void OnExit(){   //If app exited
        if (videoPlaybackThread != null)
        {
            videoPlaybackThread.interrupt();
        }
    }

    public void OnSetup(){  //Setup button click
        AsyncTask<Void, Void, Boolean> setup = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                //Send server SETUP message in appropriate format
                boolean success = _rtspModel.SendServer("SETUP", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), "no");
                //Listen for server response on RTSP and parse out session number
                String response = _rtspModel.ListenServer();
                sessionNo = GetServerSessionNo(response);
                //Return outcome (true if successful, false if not)
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if (result){
                    //Disable setup, editing of video filename
                    _view.DisableButton("Setup");
                    _view.DisableButton("VideoName");
                    //enable play
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

    public void OnPlay(){   //Play button click
        AsyncTask<Void, Void, Boolean> play = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                //Send server PLAY message in appropriate format
                boolean success = _rtspModel.SendServer("PLAY", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if (result){
                    //If video playback thread doesn't exist, create and start it in background
                    if (videoPlaybackThread == null)
                    {
                        videoPlaybackThread = new Thread(new PlaybackCommunications());
                        videoPlaybackThread.start();
                    }
                    //Disable Play button, enable pause and teardown
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

    public void OnPause(){  //Pause button click
        AsyncTask<Void, Void, Boolean> pause = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                //Send server PAUSE message in appropriate format
                boolean success = _rtspModel.SendServer("PAUSE", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if(result){
                    //Disable pause, enable play
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

    public void OnTeardown(){   //Teardown button click
        AsyncTask<Void, Void, Boolean> teardown = new AsyncTask<Void, Void, Boolean>() {
            @Override
            protected Boolean doInBackground(Void... params) {
                //Send server TEARDOWN message in appropriate format
                boolean success = _rtspModel.SendServer("TEARDOWN", _view.GetPortNo(), _view.GetVideoFilename(), _view.GetServIPAddr(), sessionNo);
                return success;
            }
            @Override
            protected void onPostExecute(Boolean result){
                if(result){
                    //Reset sequence num to 1; disable all buttons except Setup and video filename
                    _rtspModel.ResetSeqNum();
                    _view.DisableButton("Play");
                    _view.DisableButton("Pause");
                    _view.DisableButton("Teardown");
                    _view.EnableButton("Setup");
                    _view.EnableButton("VideoName");
                } else {
                    //Server disconnected
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

    public void OnVideoTap(){   //On ImageView tap, where video is played
        //Show the hover icons
        uiHandler.post(new Runnable(){
            public void run(){
                _view.ShowHoverIcons();
            }
        });
        //After 3 seconds, hide the hover icons
        uiHandler.postDelayed(new Runnable(){
            public void run(){
                _view.HideHoverIcons();
            }
        }, 3000);
    }


    ///--------------OTHER FUNCTIONS--------------///


    //Parse server response
    public String GetServerSessionNo(String msg)
    {
        //Trim message, split and return string array
        msg = msg.trim();
        String[] brokenMsg = msg.split("\\s+");
        return brokenMsg[6];
    }


    ///--------------COMMUNICATION CLASS--------------///


    private class PlaybackCommunications implements Runnable {
        //Bitmap of frame to be set to imageview
        Bitmap frameImg = null;
        public void run(){
            //Instantiate RTP model
            _rtpModel = new RTP(_view.GetPortNo(), _view.GetServIPAddr());
            frameImg = null;
            //loop
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
                //Post to Main Activity
                uiHandler.post(new Runnable(){
                    public void run(){
                        //Set video area to current frame
                        _view.SetImage(frameImg);
                    }
                });
            }
        }
    }
}
