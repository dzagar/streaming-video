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
        public MainView()
        {
            InitializeComponent();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            Console.WriteLine(ipHostInfo.ToString());
            IPAddress ipAddr = ipHostInfo.AddressList[0];
            IPEndPoint localEP = new IPEndPoint(ipAddr, Convert.ToInt32(PortNumberTextBox.Text));
            Socket servSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            try
            {
                servSocket.Bind(localEP);
                //servSocket.Listen(100);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {

        }
    }
}
