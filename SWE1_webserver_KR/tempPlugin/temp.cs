﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SWE1_webserver_KR;

namespace tempPlugin
{
    public class tempPlugin : iPlugin
    {
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

//            Console.WriteLine(data["date"]);
            string answer;
            string query;

            int count = data["date"].Length - data["date"].Replace(".", "").Length;
            SqlParameter[] myparam = new SqlParameter[(count + 2)];

            if(data["date"].Contains("-")){

                string[] range = data["date"].Split('-');
   //             int count = data["date"].Length - data["date"].Replace(".", "").Length;
    //            SqlParameter[] myparam = new SqlParameter[(count + 2)];

                int countStart = range[0].Length - range[0].Replace(".", "").Length;
                int countEnd = range[1].Length - range[1].Replace(".", "").Length;

                if (countStart == 2 && countEnd == 2)
                {
                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[2]);
                    myparam[1] = new SqlParameter("@month", dates[1]);
                    myparam[2] = new SqlParameter("@day", dates[0]);
                    myparam[3] = new SqlParameter("@year2", dates[5]);
                    myparam[4] = new SqlParameter("@month2", dates[4]);
                    myparam[5] = new SqlParameter("@day2", dates[3]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.'+@day+'.'+@year ) and convert(datetime, Dateadd(day, 1, @month2+'.'+@day2+'.'+@year2) ) order by Datum DESC";

                }
                else if (countStart == 2 && countEnd == 1)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[2]);
                    myparam[1] = new SqlParameter("@month", dates[1]);
                    myparam[2] = new SqlParameter("@day", dates[0]);
                    myparam[3] = new SqlParameter("@year2", dates[4]);
                    myparam[4] = new SqlParameter("@month2", dates[3]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.'+@day+'.'+@year ) and convert(datetime, @month2+'.1.'+@year2 ) order by Datum DESC";

                }
                else if (countStart == 2 && countEnd == 0)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[2]);
                    myparam[1] = new SqlParameter("@month", dates[1]);
                    myparam[2] = new SqlParameter("@day", dates[0]);
                    myparam[3] = new SqlParameter("@year2", dates[3]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.'+@day+'.'+@year ) and convert(datetime, '1.1.'+@year2) order by Datum DESC";

                }
                else if (countStart == 1 && countEnd == 2)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[1]);
                    myparam[1] = new SqlParameter("@month", dates[0]);
                    myparam[2] = new SqlParameter("@year2", dates[4]);
                    myparam[3] = new SqlParameter("@month2", dates[3]);
                    myparam[4] = new SqlParameter("@day2", dates[2]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.1.'+@year ) and convert(datetime, dateadd(day,1,@month2+'.'+@day2+'.'+@year2 )) order by Datum DESC";

                }
                else if (countStart == 0 && countEnd == 2)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[0]);
                    myparam[1] = new SqlParameter("@year2", dates[3]);
                    myparam[2] = new SqlParameter("@month2", dates[2]);
                    myparam[3] = new SqlParameter("@day2", dates[1]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, '1.1.'+@year ) and convert(datetime, dateadd(day, 1, @month2+'.'+@day2+'.'+@year2 )) order by Datum DESC";

                }
                else if (countStart == 0 && countEnd == 0)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[0]);
                    myparam[1] = new SqlParameter("@year2", dates[1]);
                 
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, '1.1.'+@year ) and convert(datetime, '1.1.'+@year2 ) order by Datum DESC";

                }
                else if (countStart == 0 && countEnd == 1)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[0]);
                    myparam[1] = new SqlParameter("@year2", dates[2]);
                    myparam[2] = new SqlParameter("@month2", dates[1]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, '1.1.'+@year ) and convert(datetime, @month2+'.1.'+@year2 ) order by Datum DESC";

                }
                else if (countStart == 1 && countEnd == 0)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[1]);
                    myparam[1] = new SqlParameter("@month", dates[0]);
                    myparam[2] = new SqlParameter("@year2", dates[2]);
 
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.1.'+@year ) and convert(datetime, '1.1.'+@year2 ) order by Datum DESC";

                }
                else if (countStart == 1 && countEnd == 1)
                {

                    string intermediate = data["date"].Replace("-", ".");
                    string[] dates = intermediate.Split('.');
                    myparam[0] = new SqlParameter("@year", dates[1]);
                    myparam[1] = new SqlParameter("@month", dates[0]);
                    myparam[2] = new SqlParameter("@year2", dates[3]);
                    myparam[3] = new SqlParameter("@month2", dates[2]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where Datum between convert(datetime, @month+'.1.'+@year ) and convert(datetime, @month2+'.1.'+@year2 ) order by Datum DESC";

                }
                else
                {
                    return "Invalid Date Entered";
                }
                
            } else {

       //         int count = data["date"].Length - data["date"].Replace(".", "").Length;
      //          SqlParameter[] myparam = new SqlParameter[(count + 1)];

                if (count == 0)
                {

                    myparam[0] = new SqlParameter("@year", data["date"]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where year(Datum) = @year order by Datum DESC";

                }
                else if (count == 1)
                {

                    string[] dates = data["date"].Split('.');
                    myparam[0] = new SqlParameter("@year", dates[1]);
                    myparam[1] = new SqlParameter("@month", dates[0]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where year(Datum) = @year and month(Datum) = @month  order by Datum DESC";

                }
                else if (count == 2)
                {

                    string[] dates = data["date"].Split('.');
                    myparam[0] = new SqlParameter("@year", dates[2]);
                    myparam[1] = new SqlParameter("@month", dates[1]);
                    myparam[2] = new SqlParameter("@day", dates[0]);
                    query = "select Convert( nvarchar(30),Datum, 104),CONVERT(nvarchar(30), Datum, 108), Temperatur from Messdaten where year(Datum) = @year and month(Datum) = @month and day(Datum) = @day  order by Datum DESC";

                }
                else
                {
                    return "Invalid Date Entered";
                }
            }
            try{
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=ThermoDB;integrated Security=SSPI");
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            foreach(SqlParameter par in myparam){
                if(par != null){
                cmd.Parameters.Add(par);
                }
            }
            

            SqlDataReader reader = cmd.ExecuteReader();

            if(data["type"] == "xml"){
                XmlDocument xml = new XmlDocument();
                XmlNode root, dateNode, measureNode;
                XmlAttribute date, time;

                answer = "x<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";

                root = xml.CreateElement("TempXML");
                xml.AppendChild(root);

                while (reader.Read())
                {

                    dateNode = xml.CreateElement("Measurement");
                    date = xml.CreateAttribute("Date");
                    date.InnerText = reader.GetString(0);

                    time = xml.CreateAttribute("Time");
                    time.InnerText = reader.GetString(1);

                    dateNode.Attributes.Append(date);
                    dateNode.Attributes.Append(time);

                    measureNode = xml.CreateElement("Temperature");
                    measureNode.InnerText = reader.GetSqlDecimal(2).ToString() + '°';

                    dateNode.AppendChild(measureNode);

                    root.AppendChild(dateNode);

                }

         /*       StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "&nbsp;";
                settings.NewLineChars = "</br>";
                settings.NewLineHandling = NewLineHandling.Replace;

                using(XmlWriter writer = XmlWriter.Create(sb, settings)){
                xml.Save(writer);
                }*/

            //    answer = sb.ToString();

                answer += xml.OuterXml;

              //  answer = answer.Replace("<", "&lt;");
              //  answer = answer.Replace(">", "&gt;");
              //  answer = answer.Replace("°", "&ordm;");


            } else {

                answer = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><style> #wrapper{  width:39%;  height:100%;  background-color:grey;  margin:auto; } #DateForm{  margin-left:40%;  margin-bottom:1%; } th{  width: 6em;  height: 2em;  border: 1px solid black;  background-color:white; } table{  margin:auto;  margin-top:6%; } #previous, #next, #xml{  background-color:black;  color:white;  width:9em;  cursor:pointer; }  #previous p, #next p, #xml p{  margin: 5% 13% 5% 11%;; }</style><script src=\"http://code.jquery.com/jquery-latest.min.js\"        type=\"text/javascript\"></script><script> function xml(){  var url = window.location.href;    if(url.indexOf(\"&type\") !== -1){   var parts = url.split(\"&type\");   url = parts[0];  }  url = url + \"&type=xml\";  window.location.replace(url);   }  function current(e){  if(e.which == 13){   var date = document.getElementById(\"dateInput\").value;    var url = window.location.href;    if(url.indexOf(\"?\") !== -1){   var parts = url.split(\"?\");   url = parts[0];  }    url = url + \"?date=\" + date + \"&type=current\";    window.location.replace(url);  }  } </script><title>Tempeture</title></head><body> <div id=\"wrapper\">  <div id=\"DateForm\" style=\"margin-left:39.45%; margin-bottom:2%;\" >   <p style=\"Color:white; padding-top:1%; margin-left:3%;\">Enter Date or Range</p>   <input id=\"dateInput\" onkeypress=\"current(event);\" type=\"text\" style=\"length:5em; height:1.1em;\"/>  </div>  <div id=\"xml\" style=\"margin:auto; padding-top:0.01%; padding-bottom:0.01%;\"  onclick=\"xml();\"><p>Download XML</p></div>  <table>   <tr>    <th>Date</th>    <th>Time</th>    <th>Temperature</th>   </tr>";

                while (reader.Read())
                {

                    answer += "<tr><th>" + reader.GetString(0) + "</th><th>" + reader.GetString(1) + "</th><th>" + reader.GetSqlDecimal(2) + "</th></tr>";
                }

                answer += "</table></div></body></html>";

            } 
          

            reader.Close();
            conn.Close();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return "ERROR: There are no whitespaces allowed in the date";
            }

            return answer;
        }
    }
}
