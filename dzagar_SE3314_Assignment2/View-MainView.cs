using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace dzagar_SE3314_Assignment2
{
    public partial class View : Form
    {
        Controller _controller; //one instance of Controller

        public View()
        {
            InitializeComponent();
            //Instantiate one instance of Controller
            _controller = new Controller();
            //Add onClick events (defined in Controller) for each button
            ConnectButton.Click += new EventHandler(_controller.OnConnect);
            ExitButton.Click += new EventHandler(_controller.OnExit);
            SetupButton.Click += new EventHandler(_controller.OnSetup);
            PlayButton.Click += new EventHandler(_controller.OnPlay);
            PauseButton.Click += new EventHandler(_controller.OnPause);
            TeardownButton.Click += new EventHandler(_controller.OnTeardown);
        }

        ///--------------GET FUNCTIONS--------------///

        public int GetPortNo()      //Returns port number
        {
            return int.Parse(PortNumberTextBox.Text);
        }

        public String GetVideoFilename()        //Get video file name
        {
            return VideoNameComboBox.Text;
        }

        public IPAddress GetServIPAddr()        //Get server IP address text
        {
            return IPAddress.Parse(ServerIPAddressTextBox.Text);
        }

        ///--------------SET FUNCTIONS--------------///

        public void SetImage(Image frame)       //Set to current frame
        {
            VideoImageBox.Image = frame;
        }

        public void EnableVideoView()   //Make video area visible and enabled
        {
            VideoGroupBox.Enabled = true;
            VideoGroupBox.Visible = true;
        }

        public void DisableVideoView()   //Make video area invisible and disabled
        {
            VideoGroupBox.Enabled = false;
            VideoGroupBox.Visible = false;
            VideoImageBox.Image = null;
        }

        public void AddServerRequestText(String serverText)        //Add server request text
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(AddServerRequestText), new object[] { serverText });
                return;
            }
            ServerActivityTextBox.Text += serverText;
        }

        public void AddClientActivityText(String clientText)        //Add client activity text
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(AddClientActivityText), new object[] { clientText });
                return;
            }
            ClientActivityTextBox.Text += clientText;
        }

        public bool ShowHeader()        //Show print header (true if checked, false if not)
        {
            return PrintHeaderCheckBox.Checked;
        }

        public bool ShowPacketReport()        //Show packet report (true if checked, false if not)
        {
            return PacketReportCheckBox.Checked;
        }

        public void EnableButton(string name)   //Switch through to enable appropriate button
        {
            switch (name)
            {
                case "Connect":
                    ConnectButton.Enabled = true;
                    break;
                case "Setup":
                    SetupButton.Enabled = true;
                    break;
                case "Play":
                    PlayButton.Enabled = true;
                    break;
                case "Pause":
                    PauseButton.Enabled = true;
                    break;
                case "Teardown":
                    TeardownButton.Enabled = true;
                    break;
                case "VideoName":
                    VideoNameComboBox.Enabled = true;
                    break;
            }
        }

        public void DisableButton(string name)  //switch through to disable appropriate button
        {
            switch (name)
            {
                case "Connect":
                    ConnectButton.Enabled = false;
                    break;
                case "Setup":
                    SetupButton.Enabled = false;
                    break;
                case "Play":
                    PlayButton.Enabled = false;
                    break;
                case "Pause":
                    PauseButton.Enabled = false;
                    break;
                case "Teardown":
                    TeardownButton.Enabled = false;
                    break;
                case "VideoName":
                    VideoNameComboBox.Enabled = false;
                    break;
            }
        }
    }
}
