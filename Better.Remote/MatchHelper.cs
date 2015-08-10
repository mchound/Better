using Better.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Better.Configuration;

namespace Better.Remote
{
    public static class MatchHelper
    {
        public static Stream GetRemoteCSVStream(string code, string country, int level, int startYear)
        {
            int year = int.Parse(startYear.ToString().Substring(2, 2));
            string baseUrl = "http://www.football-data.co.uk/{0}/{1}/{2}{3}.csv";
            string url = string.Format(baseUrl, code, string.Concat(year, year + 1), country, level);
            WebClient client = new WebClient();
            return new MemoryStream(client.DownloadData(url));
        }

        public static RawMatch FromCSVLine(string line, int startYear)
        {
            string[] cols = line.Split(',');
            DateTime date = DateTime.Parse(cols[1], CultureInfo.CreateSpecificCulture("EN-gb"));
            RawMatch match = new RawMatch();
            match.Level = Configurations.LeagueLevels[cols[0]];
            match.Date = date;
            match.HomeTeam = cols[2];
            match.AwayTeam = cols[3];
            match.HtGoals = int.Parse(cols[4]);
            match.AtGoals = int.Parse(cols[5]);
            match.Country = Configurations.CountryCodes[cols[0]];
            match.Season = GetSeasonFromStartYear(startYear);
            return match;
        }

        private static string GetSeasonFromStartYear(int startYear)
        {
            string start = startYear.ToString().Substring(2, 2);
            string end = (startYear + 1).ToString().Substring(2, 2);
            return string.Concat(start, end);
        }
    }
}
