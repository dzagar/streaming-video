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

    private ImageView ivDisplay;
    private Button setupBtn;
    private Button playBtn;
    private Button pauseBtn;
    private Button teardownBtn;
    private EditText serverIPText;
    private EditText portNoText;
    private Spinner videoDropdown;
    private Button connectBtn;
    private LinearLayout imageBtns;
    private ImageButton setupImgBtn;
    private ImageButton playImgBtn;
    private ImageButton pauseImgBtn;
    private ImageButton teardownImgBtn;
    private ClientController _controller;

    private String[] videoNames = {"video1.mjpeg","video2.mjpeg","video3.mjpeg"};


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

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

        _controller = new ClientController(this, new Handler());

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_spinner_item, videoNames);
        videoDropdown.setAdapter(adapter);

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
        ivDisplay.setOnClickListener(e -> {
            _controller.OnVideoTap();
        });
    };

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

    public void ShowHoverIcons(){
        imageBtns.setVisibility(View.VISIBLE);
    }
    public void HideHoverIcons(){
        imageBtns.setVisibility(View.INVISIBLE);
    }

    public void SetImage(Bitmap frame){
        Log.d("MAIN", "Entered set image");
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
