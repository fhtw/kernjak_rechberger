using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1_webserver_KR;

namespace naviPlugin
{
    public class naviPlugin : iPlugin
    {
        public bool checkRequest(string input)
        {

            return true;
        }

        public string handleRequest(string input)
        {

            return "<html><head></head><body><p>Hello World <br/> Navi</p></body></html>";
        }
    }
}
