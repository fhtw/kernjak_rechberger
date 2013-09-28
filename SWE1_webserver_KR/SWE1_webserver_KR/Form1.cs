using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Folgene Namespaces werden für Sockets benötigt
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SWE1_webserver_KR
{ 
    public partial class Form1 : Form
    {
    
        private TcpListener httplistener;
        private int port = 8080;

        public HTTPServer()
        {

           try 
	{	        
		 httplistener=new TcpListener(port);
            httplistener.Start();
            label1.Text="HTTPServer läuft ...";
            Thread th = new Thread (new ThreadStart(StartListen))
            th.Start;
             
	}
	catch (Exception ex)
	{
		
		MessageBox.Show(ex.Message);
	}
}

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {  


            label1.Text = "Running...";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
