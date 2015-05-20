using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Service
{
    public class MatchData
    {
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int Level { get; set; }
        public string Season { get; set; }
        public int StartYear { get; set; }
        public TableRow HtTableRow { get; set; }
        public TableRow AtTableRow { get; set; }
        public int HtGoals { get; set; }
        public int AtGoals { get; set; }

        public MatchData()
        {

        }

        public MatchData(RawMatch match, TableRow homeRow, TableRow awayRow)
        {
            Date = match.Date;
            HomeTeam = match.HomeTeam;
            AwayTeam = match.AwayTeam;
            Level = match.Level;
            Season = match.Season;
            StartYear = this.GetStartYear(match.Season);
            HtTableRow = homeRow;
            AtTableRow = awayRow;
            HtGoals = match.HtGoals;
            AtGoals = match.AtGoals;
        }

        private int GetStartYear(string season)
        {
            int year = int.Parse(season.Substring(0, 2));
            return year >= 93 ? 1900 + year : 2000 + year;
        }
    }
}
