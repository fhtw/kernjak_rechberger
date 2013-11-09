using System;
using System.Collections.Generic;
using System.Linq;
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

        public string handleRequest(string input)
        {

            string answer;
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=ThermoDB;integrated Security=SSPI");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 20 Datum, Temperatur from Messdaten", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            answer = "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><style>#wrapper{width:39%;height:100%;background-color:grey;margin:auto;}#DateForm{margin-left:40%;margin-bottom:1%;}th{width: 6em;height: 2em;border: 1px solid black;background-color:white;}table{margin:auto;margin-top:6%;}#previous, #next, #xml{background-color:black;color:white;width:9em;cursor:pointer;}#previous p, #next p, #xml p{margin: 5% 13% 5% 11%;;}</style><script src=\"http://code.jquery.com/jquery-latest.min.js\"type=\"text/javascript\"></script><script>function xml(){var url = window.location.href;if(url.indexOf(\"&type\") !== -1){var parts = url.split(\"&type\");url = parts[0];}url = url + \"&type=xml\";window.location.replace(url);}function previous(){var url = window.location.href;if(url.indexOf(\"&type\") !== -1){var parts = url.split(\"&type\");url = parts[0];}url = url + \"&type=previous\";window.location.replace(url);}function next(){var url = window.location.href;if(url.indexOf(\"&type\") !== -1){var parts = url.split(\"&type\");url = parts[0];}url = url + \"&type=next\";window.location.replace(url);}function current(e){if(e.which == 13){var date = document.getElementById(\"dateInput\").value;var url = window.location.href;if(url.indexOf(\"?\") !== -1){var parts = url.split(\"?\");url = parts[0];}url = url + \"?date=\" + date + \"&type=current\";window.location.replace(url);}}</script><title>Tempeture</title></head><body><div id=\"wrapper\"><div id=\"DateForm\"><p style=\"margin-left:1em; margin-bottom:0; Color:white; padding-top:1%;\">Enter Date or Range</p><input id=\"dateInput\" onkeypress=\"current(event);\" type=\"text\" style=\"length:5em; height:1.1em;\"/></div><div id=\"previous\" style=\"float:left;\" onclick=\"previous();\"><p>Previous Week</p></div><div id=\"xml\" style=\"position:absolute; left:46.5%\"  onclick=\"xml();\"><p>Download XML</p></div><div id=\"next\" style=\"float:right;\"  onclick=\"next();\"><p>Next Week</p></div><table><tr><th>Date</th><th>Time</th><th>Temperature</th></tr>";

            while (reader.Read())
            {

                answer += "<tr><th>" + reader.GetSqlDateTime(0) + "</th><th>" + reader.GetSqlDecimal(1) + "</th></tr>";
            }
            answer += "</table></div></body></html>";

            reader.Close();
            conn.Close();

            return answer;
        }
    }
}
