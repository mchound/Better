using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data
{
    public class RawMatch
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int Level { get; set; }
        public string Country { get; set; }
        public int HtGoals { get; set; }
        public int AtGoals { get; set; }
        public string Season { get; set; }
    }
}
