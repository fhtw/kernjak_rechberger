using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1_webserver_KR;

namespace dataPlugin
{
    public class dataPlugin : iPlugin
    {
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

        public string handleRequest(Dictionary<string, string> data)
        {

            return "<p>Hello World <br/> Data</p>";
        }
    }
}