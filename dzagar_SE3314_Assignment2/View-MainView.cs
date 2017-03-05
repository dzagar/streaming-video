using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dzagar_SE3314_Assignment2
{
    public partial class View : Form
    {
        Controller _controller;

        public View()
        {
            InitializeComponent();
            //Instantiate one instance of Controller
            _controller = new Controller();
            //Add onClick events (defined in Controller) for each button
        }

        //Get port number

        //Get video file name

        //Get server IP address text

        //Add server request text

        //Add client activity text

        //Show print header (true if checked, false if not)
    }
}
