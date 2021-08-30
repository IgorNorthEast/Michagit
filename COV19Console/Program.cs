using System;
using System.Net;
using System.Net.Http;

namespace COV19Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string data_url = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/archived_data/archived_time_series/time_series_19-covid-Confirmed_archived_0325.csv";
            
            var client = new HttpClient();
            var response = client.GetAsync(data_url).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;
            Console.ReadLine();
            Console.WriteLine("s");
        }
    }
}
