using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Folgene Namespaces werden für Sockets benötigt
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace SWE1_webserver_KR
{
    class Server
    {
        

   



        public class MyHttpServer 
        {
            
            protected int port;
            TcpListener listener;
            bool is_active = true;

            public MyHttpServer(int port)
            {
                this.port = port;
            }

            public void listen()
            {
                listener = new TcpListener(port);
                listener.Start();
                while (is_active)
                {
                    TcpClient s = listener.AcceptTcpClient();
                    ResponseProcessor processor = new ResponseProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }

            public void handleGETRequest(ResponseProcessor p)
            {
                string url = p.GetUrl();

                if (url.Equals("/BILD"))
                {
                    Stream fs = File.Open("../../test.png", FileMode.Open);

                    p.writeSuccess("image/png");
                    fs.CopyTo(p.OutPutStream.BaseStream);
                    p.OutPutStream.BaseStream.Flush();
                }

                Console.WriteLine("request: {0}", url);
                p.writeSuccess();
                p.OutPutStream.WriteLine("<html><body><h1>test server</h1>");
                p.OutPutStream.WriteLine("Current Time: " + DateTime.Now.ToString());
                p.OutPutStream.WriteLine("url : {0}", url);

                p.OutPutStream.WriteLine("<form method=post action=/form>");
                p.OutPutStream.WriteLine("<input type=text name=foo value=foovalue>");
                p.OutPutStream.WriteLine("<input type=submit name=bar value=barvalue>");
                p.OutPutStream.WriteLine("</form>");
            }

            public void handlePOSTRequest(ResponseProcessor p, string data)
            {
                string url = p.GetUrl();
                Console.WriteLine("POST request: {0}", url);
                
                p.writeSuccess();
                p.OutPutStream.WriteLine("<html><body><h1>test server</h1>");
                p.OutPutStream.WriteLine("<a href=/test>return</a><p>");
                p.OutPutStream.WriteLine("postbody: <pre>{0}</pre>", data);
                


            }
        }
    }
}
