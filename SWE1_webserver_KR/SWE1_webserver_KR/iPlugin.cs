using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1_webserver_KR
{
    public interface iPlugin
    {
        bool checkRequest(string input);
        string handleRequest(string input);
    }
}
