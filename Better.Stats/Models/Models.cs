using Better.Stats.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Models
{
    public interface IMatch : IEquatable<IMatch>
    {
        DateTime Date { get; set; }
        string HomeTeam { get; set; }
        string AwayTeam { get; set; }
        int Level { get; set; }
        string Country { get; set; }
        string Season { get; set; }
        int HtGoals { get; set; }
        int AtGoals { get; set; }
        MatchKey Key { get; }
    }

    public struct MatchKey
    {
        public string Country;
        public int Level;

        public override string ToString()
        {
            return string.Concat(this.Country, "-", this.Level.ToString());
        }
    }

    public class RawMatch : IMatch
    {
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int Level { get; set; }
        public string Country { get; set; }
        public int HtGoals { get; set; }
        public int AtGoals { get; set; }
        public string Season { get; set; }

        public MatchKey Key
        {
            get 
            {
                return new MatchKey { Country = this.Country, Level = this.Level }; 
            }
        }

        public bool Equals(IMatch other)
        {
            return this.Date == other.Date && this.HomeTeam == other.HomeTeam;
        }
    }

    public class Match : IMatch
    {
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Country { get; set; }
        public int Level { get; set; }
        public string Season { get; set; }
        public int HtGoals { get; set; }
        public int AtGoals { get; set; }
        public TableRow HtTable { get; set; }
        public TableRow AtTable { get; set; }
        public Streak HtStreak { get; set; }
        public Streak AtStreak { get; set; }

        public MatchKey Key
        {
            get
            {
                return new MatchKey { Country = this.Country, Level = this.Level };
            }
        }

        public bool Equals(IMatch other)
        {
            return this.Date == other.Date && this.HomeTeam == other.HomeTeam;
        }
    }
}
