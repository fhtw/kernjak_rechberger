using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Device.Location;
using SWE1_webserver_KR;

namespace naviPlugin
{
    public class naviPlugin : iPlugin
    {
        private struct Address
        {
           public string City;
           public int PostalCode;
        }
        private static Mutex mutex = new Mutex();
        private static Dictionary<string, List<Address>> mapData = new Dictionary<string, List<Address>>();


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
            loadData();

            return "<html><head></head><body><p>Hello World <br/> Navi</p></body></html>";
        }

        private void loadData()
        {
            var file = "B:/austria-latest.osm";
            using(var fs = System.IO.File.OpenRead(file))
            {
                using(var xml = new System.Xml.XmlTextReader(fs))
                {
                    while(xml.Read())
                    {
                        if(xml.NodeType == System.Xml.XmlNodeType.Element && xml.Name == "osm")
                        {
                            parseOsm(xml);
                        }
                    }
                } // read from file
            } // open from file        
        }

        private void parseOsm(System.Xml.XmlTextReader xml)
        {
            using(var osm = xml.ReadSubtree())
            {
                while(osm.Read())
                {
                    if(osm.NodeType == System.Xml.XmlNodeType.Element && (osm.Name == "Node" || osm.Name == "way"))
                    {
                        parseElement(osm);
                    }
                }
            } // use xml subtree
        }

        private void parseElement( System.Xml.XmlReader osm)
        {
            Address address = new Address();
            string street = "";
            using(var element = osm.ReadSubtree())
            {
                while(element.Read())
                {
                    if(element.NodeType == System.Xml.XmlNodeType.Element && element.Name == "tag")
                    {
                        //readTag(element);
                        string tag = element.GetAttribute("k");
                        string value = element.GetAttribute("v");
                        

                        switch (tag)
                        {
                            case "addr:city":
                                address.City = value;
                                break;

                            case "addr:postcode":
                                address.PostalCode = Convert.ToInt32(value);
                                break;

                            case "addr:street":
                                street = value;
                                break;

                        }

                    }
//                    mapData.Add(street, address);

                }
            } // use xml subtree
            if(!street.Equals("")){
                if (!mapData.ContainsKey(street))
                {
                    mapData.Add(street, new List<Address>());
                    mapData[street].Add(address);
                }
                else
                {
                    mapData[street].Add(address);
                }
            }

        }

   //     private Address(System.Xml.XmlReader element)


    }
}
