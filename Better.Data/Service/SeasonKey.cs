using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Service
{
    public class SeasonKey
    {
        public string Country { get; set; }
        public int StartYear { get; set; }
        public int Level { get; set; }

        public string Season
        {
            get 
            {
                int start = this.StartYear;
                int end = start + 1;
                return string.Concat(start.ToString().Substring(2, 2), end.ToString().Substring(2, 2));
            }
        }

        public SeasonKey(string country, int startYear, int level)
        {
            this.Country = country;
            this.StartYear = startYear;
            this.Level = level;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Country) || StartYear <= 0 || Level <= 0) throw new Exception("Invalid key arguments");

            return string.Concat(Country, "-", StartYear, "-", Level);
        }
    }
}
