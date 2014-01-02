using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using SWE1_webserver_KR;


namespace weatherPlugin
{
    public class weatherPlugin : iPlugin
    {

        public string getName()
        {
            return "weather";
        }
        public bool checkRequest(string input)
        {
            string[] url = input.Split('/');
            if (url[1].Equals("weather"))
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
            //soapclient configuration
            //string loading = "<html>Loading Data</html>";
            //WriteResponse(loading , "text/html", OutPutStream, loading.Length);

            var binding = new BasicHttpBinding();
            binding.Name = "GlobalWeatherSoap";

            string endpointStr = "http://www.webservicex.net/globalweather.asmx";
            var endpoint = new EndpointAddress(endpointStr);

            var client = new WeatherServiceSoap.GlobalWeatherSoapClient(binding, endpoint);
            


            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body><div><h1>Weather</h1>");

            sb.Append(client.GetCitiesByCountry("Austria"));

            sb.Append("</div></body></html>");
            WriteResponse(sb.ToString(), "text/html", OutPutStream, sb.Length);
            return;
        }

        private void WriteResponse(string content, string type, StreamWriter OutPutStream, int length)
        {
            OutPutStream.WriteLine("HTTP/1.0 200 OK");
            OutPutStream.WriteLine("Content-Type: " + type);
            OutPutStream.WriteLine("Content-Length: " + length);
            OutPutStream.WriteLine("Connection: close");
            OutPutStream.WriteLine("");

            OutPutStream.WriteLine(content);
        }

        private string getCities(string country, WeatherServiceSoap.GlobalWeatherSoapClient client)
        {
            return "a";
        }

        private string getWeather(string country, string city, WeatherServiceSoap.GlobalWeatherSoapClient client)
        {
            return "b";
        }


    }
}
