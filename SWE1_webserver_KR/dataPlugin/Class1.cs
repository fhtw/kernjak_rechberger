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
        public bool checkRequest(string input){

            string[] url = input.Split('/');
            if(url[1].Equals("temp")){
                return true;
            } else {
                return false;
            }
        }

        public string handleRequest(string input){

            return "<html><head></head><body><p>Hello World <br/> Data</p></body></html>";
        }
    }
}