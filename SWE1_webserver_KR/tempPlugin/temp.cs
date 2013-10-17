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

            answer = "<table>";

            while (reader.Read())
            {

                answer += "<tr><th>" + reader.GetSqlDateTime(0) + "</th><th>" + reader.GetSqlDecimal(1) + "</th></tr>";
            }
            answer += "</table>";

            reader.Close();
            conn.Close();

            return answer;
        }
    }
}
