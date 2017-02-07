using System;
using System.Windows.Forms;

namespace dzagar_SE3314_Assignment
{
    public partial class MainView : Form
    {
        Controller _controller;
        public MainView()
        {
            InitializeComponent();
            //Instantiate one instance of Controller
            _controller = new Controller();
            //this.ListenButton_Click += new EventHandler(_controller.OnListen);
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            //Disable Listen button
            ListenButton.Enabled = false;
            //Call controller OnListen method
            _controller.OnListen(sender, e);
        }

        //GET FUNCTIONS

        public int GetPortNo()         //Returns port number
        {
            return int.Parse(PortNumberTextBox.Text);
        }

        public bool ShowRTPHeader()     //Returns boolean value of Show RTP Header checkbox
        {
            return PrintHeaderCheckBox.Checked;
        }

        //SET FUNCTIONS

        public void SetServerIPText(string ipAddress)         //Set value in IP address text box
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(SetServerIPText), new object[] { ipAddress });
                return;
            }
            IPAddressTextBox.Text = ipAddress;
        }

        public void AddClientRequestText(string clientMsg)        //Add client response message to Client Request text box
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(AddClientRequestText), new object[] { clientMsg });
                return;
            }
            ClientRequestsTextBox.Text += clientMsg;
        }

        public void AddServerActivityText(string serverMsg)        //Add server activity message to Server dialog box
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(AddServerActivityText), new object[] { serverMsg });
                return;
            }
            ServerDialogBox.Text += serverMsg;
        }

        public void SetFrameNoText(string frameNo)        //Set frame number text box
        {
            if (InvokeRequired)     //required since we are multithreading
            {
                this.Invoke(new Action<string>(SetFrameNoText), new object[] { frameNo });
                return;
            }
            FrameNoTextBox.Text = frameNo;
        }
    }
}
