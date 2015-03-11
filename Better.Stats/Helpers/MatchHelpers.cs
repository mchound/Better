using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Better.Stats.Models;
using Better.Stats.Config;

namespace Better.Stats.Helpers
{
    public static class MatchHelpers
    {
        public static Match FromRaw(this IMatch match)
        {
            Match _match = new Match();
            _match.Date = match.Date;
            _match.HomeTeam = match.HomeTeam;
            _match.AwayTeam = match.AwayTeam;
            _match.Country = match.Country;
            _match.Level = match.Level;
            _match.HtGoals = match.HtGoals;
            _match.AtGoals = match.AtGoals;
            _match.Season = match.Season;
            return _match;
        }

        public static RawMatch FromCSVLine(string line)
        {
            RawMatch match = new RawMatch();
            string[] cols = line.Split(',');
            match.Level = Configuration.LeagueLevels[cols[0]];
            match.Date = DateTime.Parse(cols[1], CultureInfo.CreateSpecificCulture("EN-gb"));
            match.HomeTeam = cols[2];
            match.AwayTeam = cols[3];
            match.HtGoals = int.Parse(cols[4]);
            match.AtGoals = int.Parse(cols[5]);
            match.Country = Configuration.CountryCodes[cols[0]];
            return match;
        }

        public static string ToString(this MatchKey key)
        {
            return string.Concat(key.Country, "-", key.Level.ToString());
        }
    }
}
