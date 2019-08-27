using Newtonsoft.Json;
using SocketLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComClass clase = new ComClass();
            clase.funciones = ComClass.function.cash_handling;
            clase.Value = 4500;
            string Test=JsonConvert.SerializeObject(clase);
            using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
            {
                socket.Send(Test); // Sends some data
                var data = socket.Receive(); // Receives some data back (blocks execution)
            }
        }
    }
}
