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
    }
}
