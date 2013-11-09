using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Reflection;

namespace SWE1_webserver_KR
{
    class pluginM
    {
        private ArrayList plugins;
        private Type type;

        public pluginM(){

            plugins = new ArrayList();
            type = typeof(SWE1_webserver_KR.iPlugin);

        }

       public void loadPlugins()
        {
            foreach(string dll in Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "*.dll")){
                    Assembly Aplugin = Assembly.LoadFrom(dll);

                    foreach(Type pluType in Aplugin.GetExportedTypes()){
                        if (pluType.IsPublic && pluType.IsClass && pluType.GetInterface(type.FullName) != null)
                        {
                            plugins.Add(Aplugin.CreateInstance(pluType.FullName));
                        }
                    }
                }

        }

        public string handleRequest(string url)
        {
            foreach (iPlugin addin in plugins)
                {
                    if (addin.checkRequest(url) == true)
                    {   
                        return(addin.handleRequest(url));
                    }
                }
            return "<h1>Plugin not available</h1>";
        }
    }
}
