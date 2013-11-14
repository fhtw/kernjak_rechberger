﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1_webserver_KR;

namespace naviPlugin
{
    public class naviPlugin : iPlugin
    {
        public string getName()
        {
            return "navi";
        }
        public bool checkRequest(string input)
        {
            string[] url = input.Split('/');
            if (url[1].Equals("navi"))
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

            return "<html><head></head><body><p>Hello World <br/> Navi</p></body></html>";
        }
    }
}
