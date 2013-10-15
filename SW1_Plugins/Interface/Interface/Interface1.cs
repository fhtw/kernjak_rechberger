using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pugin

{
    interface IPlugin
    {
        bool checkRequest(string request);
        string handleRequest();
    }
}
