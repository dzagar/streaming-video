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

            IPAddress ipAddr = ipHostInfo.AddressList[0];
            Console.WriteLine(ipAddr.ToString());
            //ipAddr is in format it doesn't like... investigate
            IPEndPoint localEP = new IPEndPoint(ipAddr, Convert.ToInt32(PortNumberTextBox.Text));
            Console.WriteLine(localEP.ToString());
            try
            {
                Socket servSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                servSocket.Bind(localEP);
                Console.WriteLine("Binding werked!");
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
