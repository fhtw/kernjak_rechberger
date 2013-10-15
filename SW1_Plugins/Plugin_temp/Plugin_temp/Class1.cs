using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_temp
{
    

    public class Temp : Plugin.IPlugin
    {
        private string text;

        bool Plugin.iPLugin.checkRequest(string request)
        {
           if(request = "Temp"){
               text = request;
               return true;
           } else {
               return false;
           }
       }

        string Plugin.iPLugin.handleRequest()
        {
            return text += "_done";
        }
    }
}
