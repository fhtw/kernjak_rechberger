using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SWE1_webserver_KR;
using System.Globalization;

namespace tempPlugin
{
    public class tempPlugin : iPlugin
    {
        public string getName()
        {
            return "temp";
        }
        public bool checkRequest(string input)
        {
            string[] url = input.Split('/');
            if (url[1].Equals("temp"))
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

            if (!data.ContainsKey("type"))
            {
                data.Add("type", "display");
            }
            if (!data.ContainsKey("date"))
            {
                data.Add("date", "2013");
            }


            StringBuilder answer = new StringBuilder();
            string query;
            

            int count = data["date"].Length - data["date"].Replace(".", "").Length;
            SqlParameter[] myparam = new SqlParameter[(count + 2)];

            if(data["date"].Contains("-")){

                string[] range = data["date"].Split('-');

                DateTime end = new DateTime();
                DateTime start = new DateTime();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("de-AT");
                DateTimeStyles dateStyle = DateTimeStyles.None;

                int countStart = range[0].Length - range[0].Replace(".", "").Length;
                int countEnd = range[1].Length - range[1].Replace(".", "").Length;

                

                if (countStart == 2 && countEnd == 2)
                {
                   
                    if(! (DateTime.TryParse(range[1], culture ,dateStyle,out end)))
                    {
                        return "Invalid Date";
                    }
                    end = end.AddDays(1);
                    if (!(DateTime.TryParse(range[0], culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 2 && countEnd == 1)
                {

                    if(! (DateTime.TryParse(range[1].Insert(0, "1."), culture ,dateStyle,out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0], culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 2 && countEnd == 0)
                {

                    if(! (DateTime.TryParse(range[1].Insert(0, "1.1."), culture ,dateStyle,out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0], culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 1 && countEnd == 2)
                {

                    if (!(DateTime.TryParse(range[1], culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    end = end.AddDays(1);
                    if (!(DateTime.TryParse(range[0].Insert(0, "1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }

                }
                else if (countStart == 0 && countEnd == 2)
                {

                    if (!(DateTime.TryParse(range[1], culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    end = end.AddDays(1);
                    if (!(DateTime.TryParse(range[0].Insert(0, "1.1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 0 && countEnd == 0)
                {

                    if (!(DateTime.TryParse(range[1].Insert(0, "1.1."), culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0].Insert(0, "1.1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 0 && countEnd == 1)
                {

                    if (!(DateTime.TryParse(range[1].Insert(0, "1."), culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0].Insert(0, "1.1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 1 && countEnd == 0)
                {

                    if (!(DateTime.TryParse(range[1].Insert(0, "1.1."), culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0].Insert(0, "1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else if (countStart == 1 && countEnd == 1)
                {

                    if (!(DateTime.TryParse(range[1].Insert(0, "1."), culture, dateStyle, out end)))
                    {
                        return "Invalid Date";
                    }
                    if (!(DateTime.TryParse(range[0].Insert(0, "1."), culture, dateStyle, out start)))
                    {
                        return "Invalid Date";
                    }
                }
                else
                {
                    return "Invalid Date Entered";
                }
                try{
                    myparam[0] = new SqlParameter("@begin", System.Data.SqlDbType.DateTime);
                    myparam[0].Value = start;
                    myparam[1] = new SqlParameter("@end", System.Data.SqlDbType.DateTime);
                    myparam[1].Value = end;//.AddDays(1);
                    query = "Select Datum, Temperatur from Messdaten where Datum between @begin and @end order by Datum DESC";
                }
                catch (System.Data.SqlTypes.SqlTypeException)
                {
                    return "ERROR: Out of Range Date";
                }
            } else {

                if (count == 0)
                {

                    myparam[0] = new SqlParameter("@year", data["date"]);
                    query = "select Datum, Temperatur from Messdaten where year(Datum) = @year order by Datum DESC";

                }
                else if (count == 1)
                {

                    string[] dates = data["date"].Split('.');
                    myparam[0] = new SqlParameter("@year", dates[1]);
                    myparam[1] = new SqlParameter("@month", dates[0]);
                    query = "select Datum, Temperatur from Messdaten where year(Datum) = @year and month(Datum) = @month  order by Datum DESC";

                }
                else if (count == 2)
                {

                    string[] dates = data["date"].Split('.');
                    myparam[0] = new SqlParameter("@year", dates[2]);
                    myparam[1] = new SqlParameter("@month", dates[1]);
                    myparam[2] = new SqlParameter("@day", dates[0]);
                    query = "select Datum, Temperatur from Messdaten where year(Datum) = @year and month(Datum) = @month and day(Datum) = @day  order by Datum DESC";

                }
                else
                {
                    return "Invalid Date Entered";
                }
            }
            try{
            using(SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=ThermoDB;integrated Security=SSPI")){
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                foreach(SqlParameter par in myparam){
                    if(par != null){
                    cmd.Parameters.Add(par);
                    }
                }
            

                using(SqlDataReader reader = cmd.ExecuteReader()){

                    if(data["type"] == "xml"){
                        XmlDocument xml = new XmlDocument();
                        XmlNode root, dateNode, measureNode;
                        XmlAttribute date, time;

                        answer.Append("x<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");

                        root = xml.CreateElement("TempXML");
                        xml.AppendChild(root);

                        while (reader.Read())
                        {

                            DateTime MeasureDate = reader.GetDateTime(0);
                            dateNode = xml.CreateElement("Measurement");
                            date = xml.CreateAttribute("Date");
                            date.InnerText = MeasureDate.ToString("dd.MM.yyyy");

                            time = xml.CreateAttribute("Time");
                            time.InnerText = MeasureDate.ToString("HH:mm:ss");

                            dateNode.Attributes.Append(date);
                            dateNode.Attributes.Append(time);

                            measureNode = xml.CreateElement("Temperature");
                            measureNode.InnerText = reader.GetSqlDecimal(1).ToString() + '°';

                            dateNode.AppendChild(measureNode);

                            root.AppendChild(dateNode);

                        }

                        answer.Append(xml.OuterXml);


                    } else {

                        answer.Append("<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><style>#wrapper{width:39%;height:100%;background-color:grey;margin:auto;}#DateForm{margin-left:40%;margin-bottom:1%;}th{width: 6em;height: 2em;border: 1px solid black;background-color:white;}table{margin:auto;margin-top:6%;}#previous, #next, #xml{background-color:black;color:white;width:9em;cursor:pointer;}#previous p, #next p, #xml p{margin: 5% 13% 5% 11%;;}</style><script src=\"http://code.jquery.com/jquery-latest.min.js\"        type=\"text/javascript\"></script><script>function xml(){var url = window.location.href;if(url.indexOf(\"&type\") !== -1){var parts = url.split(\"&type\");url = parts[0] + \"&type=xml\";} else if(url.indexOf(\"?type\") !== -1){var parts = url.split(\"?type\");url = parts[0] + \"?type=xml\";} else if(url.indexOf(\"?date\") !== -1){url = url + \"&type=xml\";} else {url = url + \"?type=xml\";}window.location.replace(url);}function current(e){if(e.which == 13){var date = document.getElementById(\"dateInput\").value;if(date == \"\"){alert(\"Pleas enter a date or date range.\");return;}var url = window.location.href;if(url.indexOf(\"?\") !== -1){var parts = url.split(\"?\");url = parts[0];}url = url + \"?date=\" + date + \"&type=display\";window.location.replace(url);}}</script><title>Temperature</title></head><body><div id=\"wrapper\"><div id=\"DateForm\" style=\"margin-left:39.45%; margin-bottom:2%;\" ><p style=\"Color:white; padding-top:1%; margin-left:3%;\">Enter Date or Range</p><input id=\"dateInput\" onkeypress=\"current(event);\" type=\"text\" style=\"length:5em; height:1.1em;\"/></div><div id=\"xml\" style=\"margin:auto; padding-top:0.01%; padding-bottom:0,01%;\"  onclick=\"xml();\"><p>Download XML</p></div><table><tr><th>Date</th><th>Time</th><th>Temperature</th>");

                        while (reader.Read())
                        {
                            DateTime MeasureDate = reader.GetDateTime(0);
                            answer.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}°</th></tr>", MeasureDate.ToString("dd.MM.yyyy"), MeasureDate.ToString("HH:mm:ss"), reader.GetSqlDecimal(1));
                        }

                        answer.Append("</table></div></body></html>");

                    }


                } // end reader using
            } // end connection using
            }
            catch (System.Data.SqlClient.SqlException e)
            {
               // return "ERROR: An error ocurred within the database.";
                return e.Message;
            }

            return answer.ToString();
        }
    }
}
