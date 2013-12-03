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
            if (url[1].Equals("data") || url[1].Equals(""))
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
            Stream fs = File.Open("../../index.html", FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);
            byte[] bytes = new byte[fs.Length];
            int read;
            String sResponse = "";
            int iTotBytes = 0;
            while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Read from the file and write the data to the network
                sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);

                iTotBytes = iTotBytes + read;

            }
            reader.Close();
            fs.Close();
            //  writeSuccess("text/html", bytes.Length);
            //OutPutStream.Write(sResponse);
//            return "d" + size.ToString() + "ö" + sResponse;

            OutPutStream.WriteLine("HTTP/1.0 200 OK");
            OutPutStream.WriteLine("Content-Type: text/html");
            OutPutStream.WriteLine("Content-Length: " + Convert.ToString(bytes.Length));
            OutPutStream.WriteLine("Connection: close");
            OutPutStream.WriteLine("");
            OutPutStream.WriteLine(sResponse);

//            OutPutStream.WriteLine("<h2>Data Plugin</h2>");
            return;
        }
    }
}