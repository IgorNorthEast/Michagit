﻿using System.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace COV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/archived_data/archived_time_series/time_series_19-covid-Confirmed_archived_0325.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                yield return line.Replace("Korea,", "Korea -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

                foreach (var row in lines)
                {
                    var province = row[0].Trim();
                    var country_name = row[1].Trim(' ', '"');
                    var counts = row.Skip(4).Select(int.Parse).ToArray();
                    yield return (country_name, province, counts);
                }
        }
        public static void Main(string[] args)
        {
            // foreach (var data_line in GetDataLines())
            // {
            //     Console.WriteLine(data_line.ToString());
            // }

            //var dates = GetDates();
            //Array.ForEach(x=> dates[x].ToString(), Console.WriteLine);
            //Console.WriteLine(string.Join($"\r\n", dates));

            var russia_data = GetData().First(v => v.Country.Equals("Belarus", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date} - {count}")));
        }
    }
}
