using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1_webserver_KR;
using System.IO;

namespace dataPlugin
{
    public class dataPlugin : iPlugin
    {
        public string getName()
        {
            return "";
        }
        public bool checkRequest(string input)
        {

            string[] url = input.Split('/');
            if (url[1].Equals("data"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void handleRequest(Dictionary<string, string> data, StreamWriter OutPutStream)
        {
            OutPutStream.WriteLine("HTTP/1.0 200 OK");
            OutPutStream.WriteLine("Content-Type: text/html");
            OutPutStream.WriteLine("Connection: close");
            OutPutStream.WriteLine("");

            OutPutStream.WriteLine("<h2>Data Plugin</h2>");
            return;
        }
    }
}