package com.example.danazagar.dzagar_se3314_assignment3;

import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Spinner;

import java.net.InetAddress;
import java.net.UnknownHostException;


public class MainActivity extends AppCompatActivity {

    //References to View elements

    private ImageView ivDisplay;    //video image display
    private Button setupBtn;    //setup button
    private Button playBtn;     //play button
    private Button pauseBtn;    //pause button
    private Button teardownBtn; //teardown button
    private EditText serverIPText;  //server ip text field
    private EditText portNoText;    //port number text field
    private Spinner videoDropdown;  //video dropdown list
    private Button connectBtn;      //connect server button
    private LinearLayout imageBtns; //grouping of buttons over image display
    private ImageButton setupImgBtn;    //setup image button
    private ImageButton playImgBtn;     //play image button
    private ImageButton pauseImgBtn;    //pause image button
    private ImageButton teardownImgBtn; //teardown image btuton
    private ClientController _controller;   //one instance of controller

    private String[] videoNames = {"video1.mjpeg","video2.mjpeg","video3.mjpeg"};


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //Set references to elements
        ivDisplay = (ImageView) findViewById(R.id.videoImageView);
        setupBtn = (Button) findViewById(R.id.setupBtn);
        playBtn = (Button) findViewById(R.id.playBtn);
        pauseBtn = (Button) findViewById(R.id.pauseBtn);
        teardownBtn = (Button) findViewById(R.id.teardownBtn);
        connectBtn = (Button) findViewById(R.id.connectBtn);
        serverIPText = (EditText) findViewById(R.id.serverIPText);
        portNoText = (EditText) findViewById(R.id.portNoText);
        videoDropdown = (Spinner) findViewById(R.id.videoNameDropdown);
        imageBtns = (LinearLayout) findViewById(R.id.imageButtons);
        setupImgBtn = (ImageButton) findViewById(R.id.setupImgBtn);
        playImgBtn = (ImageButton) findViewById(R.id.playImgBtn);
        pauseImgBtn = (ImageButton) findViewById(R.id.pauseImgBtn);
        teardownImgBtn = (ImageButton) findViewById(R.id.teardownImgBtn);

        //Create new controller with reference to Main Activity and a handler
        _controller = new ClientController(this, new Handler());

        //Set video names in dropdown
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_spinner_item, videoNames);
        videoDropdown.setAdapter(adapter);

        //Add listeners to each button to call appropriate functions in Controller
        connectBtn.setOnClickListener(e -> {
            _controller.OnConnect();
        });
        setupBtn.setOnClickListener(e -> {
            _controller.OnSetup();
        });
        setupImgBtn.setOnClickListener(e -> {
            _controller.OnSetup();
        });
        playBtn.setOnClickListener(e -> {
            _controller.OnPlay();
        });
        playImgBtn.setOnClickListener(e -> {
            _controller.OnPlay();
        });
        pauseBtn.setOnClickListener(e -> {
            _controller.OnPause();
        });
        pauseImgBtn.setOnClickListener(e -> {
            _controller.OnPause();
        });
        teardownBtn.setOnClickListener(e -> {
            _controller.OnTeardown();
        });
        teardownImgBtn.setOnClickListener(e -> {
            _controller.OnTeardown();
        });
        //Add listener to image view to show image buttons
        ivDisplay.setOnClickListener(e -> {
            _controller.OnVideoTap();
        });
    };

    //Safely exit
    public void onDestroy(){
        _controller.OnExit();
        super.onDestroy();
    }


    public int GetPortNo()      //Returns port number
    {
        return Integer.parseInt(portNoText.getText().toString());
    }

    public String GetVideoFilename()        //Get video file name
    {
        return videoNames[videoDropdown.getSelectedItemPosition()];
    }

    public InetAddress GetServIPAddr()        //Get server IP address text
    {
        try {
            return InetAddress.getByName(serverIPText.getText().toString());
        } catch (UnknownHostException e){
            return null;
        }
    }

    //Show/hide image buttons over image view
    public void ShowHoverIcons(){
        imageBtns.setVisibility(View.VISIBLE);
    }
    public void HideHoverIcons(){
        imageBtns.setVisibility(View.INVISIBLE);
    }

    public void SetImage(Bitmap frame){     //Set frame in view
        ivDisplay.setImageBitmap(frame);
    }

    public void EnableVideoView()   //Make video area visible and enabled
    {
        ivDisplay.setEnabled(true);
    }

    public void DisableVideoView()   //Make video area invisible and disabled
    {
        ivDisplay.setImageBitmap(null);
        ivDisplay.setEnabled(false);
    }

    public void EnableButton(String name)  //switch through to disable appropriate button
    {
        switch (name) {
            case "Connect":
                connectBtn.setEnabled(true);
                break;
            case "Setup":
                setupBtn.setEnabled(true);
                setupImgBtn.setEnabled(true);
                setupImgBtn.setVisibility(View.VISIBLE);
                break;
            case "Play":
                playBtn.setEnabled(true);
                playImgBtn.setEnabled(true);
                playImgBtn.setVisibility(View.VISIBLE);
                break;
            case "Pause":
                pauseBtn.setEnabled(true);
                pauseImgBtn.setEnabled(true);
                pauseImgBtn.setVisibility(View.VISIBLE);
                break;
            case "Teardown":
                teardownBtn.setEnabled(true);
                teardownImgBtn.setEnabled(true);
                teardownImgBtn.setVisibility(View.VISIBLE);
                break;
            case "VideoName":
                videoDropdown.setEnabled(true);
                break;
        }
    }

    public void DisableButton(String name)  //switch through to disable appropriate button
    {
        switch (name) {
            case "Connect":
                connectBtn.setEnabled(false);
                break;
            case "Setup":
                setupBtn.setEnabled(false);
                setupImgBtn.setEnabled(false);
                setupImgBtn.setVisibility(View.GONE);
                break;
            case "Play":
                playBtn.setEnabled(false);
                playImgBtn.setEnabled(false);
                playImgBtn.setVisibility(View.GONE);
                break;
            case "Pause":
                pauseBtn.setEnabled(false);
                pauseImgBtn.setEnabled(false);
                pauseImgBtn.setVisibility(View.GONE);
                break;
            case "Teardown":
                teardownBtn.setEnabled(false);
                teardownImgBtn.setEnabled(false);
                teardownImgBtn.setVisibility(View.GONE);
                break;
            case "VideoName":
                videoDropdown.setEnabled(false);
                break;
        }
    }
}
