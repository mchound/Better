using Better.Data.Filters;
using Better.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Better.Web.Models
{
    public class StatsRequestModel
    {
        public MatchFilter MatchFilter { get; set; }
        public IEnumerable<SeasonKey> SeasonKeys { get; set; }
    }
}