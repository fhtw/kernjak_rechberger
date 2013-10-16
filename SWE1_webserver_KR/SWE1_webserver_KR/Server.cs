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
        

        public class HttpProcessor
        {
            private TcpClient socket;
            private MyHttpServer srv;

            private Stream inputStream;
            private StreamWriter outputStream;
            public StreamWriter OutPutStream { get { return outputStream; } }
            HttpUrl url = new HttpUrl();
            HttpRequest hr = new HttpRequest();


            public string GetUrl ()
            {
            return hr.GetUrl();
            }

            private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

            public HttpProcessor(TcpClient s, MyHttpServer srv)
            {
                this.socket = s;
                this.srv = srv;
            }


            private string streamReadLine(Stream inputStream)
            {
                int next_char;
                string data = "";
                while (true)
                {
                    next_char = inputStream.ReadByte();
                    if (next_char == '\n') { break; }
                    if (next_char == '\r') { continue; }
                    if (next_char == -1) { Thread.Sleep(1); continue; };
                    data += Convert.ToChar(next_char);
                }
                return data;
            }
            public void process()
            {
                // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
                // "processed" view of the world, and we want the data raw after the headers
                inputStream = new BufferedStream(socket.GetStream());

                // we probably shouldn't be using a streamwriter for all output from handlers either
                outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
                try
                {
                    hr.parseRequest(streamReadLine( inputStream));
                    bool trigger = true;
                    while (trigger)
                    {
                        string input = streamReadLine(inputStream);
                        if (input == "")
                        { break; }
                        else
                        {
                           hr.readHeaders(input);
                        }
                    }
                    if (hr.GetMethod().Equals("GET"))
                    {
                        handleGETRequest();
                    }
                    else if (hr.GetMethod().Equals("POST"))
                    {
                        handlePOSTRequest();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                outputStream.Flush();
                // bs.Flush(); // flush any remaining output
                inputStream = null; outputStream = null; // bs = null;            
                socket.Close();
            }
    
            private void handleGETRequest()
            {
                //HttpRequest.handleGETRequest(this);
                srv.handleGETRequest(this);
            }

            private const int BUF_SIZE = 4096;
            public void handlePOSTRequest()
            {
                // this post data processing just reads everything into a memory stream.
                // this is fine for smallish things, but for large stuff we should really
                // hand an input stream to the request processor. However, the input stream 
                // we hand him needs to let him see the "end of the stream" at this content 
                // length, because otherwise he won't know when he's seen it all! 

                Console.WriteLine("get post data start");
                int content_len = 0;
                MemoryStream ms = new MemoryStream();
                if (hr.getHeaders().ContainsKey("Content-Length"))
                {
                    content_len = Convert.ToInt32(hr.getHeaders()["Content-Length"]);
                    if (content_len > MAX_POST_SIZE)
                    {
                        throw new Exception(
                            String.Format("POST Content-Length({0}) too big for this simple server",
                              content_len));
                    }
                    byte[] buf = new byte[BUF_SIZE];
                    int to_read = content_len;
                    while (to_read > 0)
                    {
                        Console.WriteLine("starting Read, to_read={0}", to_read);

                        int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                        Console.WriteLine("read finished, numread={0}", numread);
                        if (numread == 0)
                        {
                            if (to_read == 0)
                            {
                                break;
                            }
                            else
                            {
                                throw new Exception("client disconnected during post");
                            }
                        }
                        to_read -= numread;
                        ms.Write(buf, 0, numread);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                }
                Console.WriteLine("get post data end");
                srv.handlePOSTRequest(this, new StreamReader(ms));

            }

            public void writeSuccess(string content_type = "text/html")
            {
                outputStream.WriteLine("HTTP/1.0 200 OK");
                outputStream.WriteLine("Content-Type: " + content_type);
                outputStream.WriteLine("Connection: close");
                outputStream.WriteLine("");
            }

            public void writeFailure()
            {
                outputStream.WriteLine("HTTP/1.0 404 File not found");
                outputStream.WriteLine("Connection: close");
                outputStream.WriteLine("");
            }
        }



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
                    HttpProcessor processor = new HttpProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }
          
            public  void handleGETRequest(HttpProcessor p)
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

            public  void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
            {
                string url = p.GetUrl();
                Console.WriteLine("POST request: {0}", url);
                string data = inputData.ReadToEnd();

                p.writeSuccess();
                p.OutPutStream.WriteLine("<html><body><h1>test server</h1>");
                p.OutPutStream.WriteLine("<a href=/test>return</a><p>");
                p.OutPutStream.WriteLine("postbody: <pre>{0}</pre>", data);


            }
        }
    }
}
