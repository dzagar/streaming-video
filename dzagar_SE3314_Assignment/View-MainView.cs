using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace dzagar_SE3314_Assignment
{
    public partial class MainView : Form
    {
        Controller _controller;
        public MainView()
        {
            InitializeComponent();
            _controller = new Controller();
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            //Disable button
            ListenButton.Enabled = false;
            //Call controller onListen
            _controller.onListenClick(sender, e);
        }

        //GET FUNCTIONS

        public int GetPortNo()         //Returns port number
        {
            return int.Parse(PortNumberTextBox.Text);
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
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AddClientRequestText), new object[] { clientMsg });
                return;
            }
            ClientRequestsTextBox.Text += clientMsg;
        }

        public void AddServerActivityText(string serverMsg)        //Add server activity message to Server dialog box
        {
            if (InvokeRequired)
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
