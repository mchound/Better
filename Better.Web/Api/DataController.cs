using Better.Data;
using Better.Data.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Better.Web.Api
{
    public class DataController : ApiController
    {
        [HttpGet]
        public object Get()
        {
            BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());

            var matches = service.Get(new SeasonKey[] { 
                new SeasonKey("England", 2014, 1),
                new SeasonKey("England", 2013, 1),
                new SeasonKey("England", 2012, 1),
                new SeasonKey("England", 2011, 1),
                new SeasonKey("England", 2010, 1),
                new SeasonKey("England", 2009, 1),
                new SeasonKey("England", 2008, 1),
                new SeasonKey("England", 2007, 1),
                new SeasonKey("England", 2006, 1),
                new SeasonKey("England", 2005, 1),
                new SeasonKey("England", 2004, 1),
                new SeasonKey("England", 2003, 1),
                new SeasonKey("England", 2002, 1),
                new SeasonKey("England", 2001, 1),
                new SeasonKey("England", 2000, 1),
                new SeasonKey("England", 1999, 1),
                new SeasonKey("England", 1998, 1),
                new SeasonKey("England", 1997, 1),
                new SeasonKey("England", 1996, 1),
                new SeasonKey("England", 1995, 1),
                new SeasonKey("England", 1994, 1),
                new SeasonKey("England", 1993, 1),
                //new SeasonKey("England", 2014, 2),
                //new SeasonKey("England", 2013, 2),
                //new SeasonKey("England", 2012, 2),
                //new SeasonKey("England", 2011, 2),
                //new SeasonKey("England", 2010, 2),
                //new SeasonKey("England", 2009, 2),
                //new SeasonKey("England", 2008, 2),
                //new SeasonKey("England", 2007, 2),
                //new SeasonKey("England", 2006, 2),
                //new SeasonKey("England", 2005, 2),
                //new SeasonKey("England", 2004, 2),
                //new SeasonKey("England", 2003, 2),
                //new SeasonKey("England", 2002, 2),
                //new SeasonKey("England", 2001, 2),
                //new SeasonKey("England", 2000, 2),
                //new SeasonKey("England", 1999, 2),
                //new SeasonKey("England", 1998, 2),
                //new SeasonKey("England", 1997, 2),
                //new SeasonKey("England", 1996, 2),
                //new SeasonKey("England", 1995, 2),
                //new SeasonKey("England", 1994, 2),
                //new SeasonKey("England", 1993, 2),
                //new SeasonKey("England", 2014, 3),
                //new SeasonKey("England", 2013, 3),
                //new SeasonKey("England", 2012, 3),
                //new SeasonKey("England", 2011, 3),
                //new SeasonKey("England", 2010, 3),
                //new SeasonKey("England", 2009, 3),
                //new SeasonKey("England", 2008, 3),
                //new SeasonKey("England", 2007, 3),
                //new SeasonKey("England", 2006, 3),
                //new SeasonKey("England", 2005, 3),
                //new SeasonKey("England", 2004, 3),
                //new SeasonKey("England", 2003, 3),
                //new SeasonKey("England", 2002, 3),
                //new SeasonKey("England", 2001, 3),
                //new SeasonKey("England", 2000, 3),
                //new SeasonKey("England", 1999, 3),
                //new SeasonKey("England", 1998, 3),
                //new SeasonKey("England", 1997, 3),
                //new SeasonKey("England", 1996, 3),
                //new SeasonKey("England", 1995, 3),
                //new SeasonKey("England", 1994, 3),
                //new SeasonKey("England", 1993, 3)
            });

            return matches;
        }

        //[HttpGet]
        //public object Get()
        //{
        //    List<KeyValuePair<int, Diff>> stats = this.GetStats();

        //    float count = stats.Count;
        //    float sumX = stats.Sum(s => s.Key);
        //    float sumY = stats.Sum(s => s.Value.HomeProb);
        //    float sumXY = stats.Sum(s => ((float)s.Key * (float)s.Value.HomeProb));
        //    float sumX2 = stats.Sum(s => ((float)s.Key * (float)s.Key));
        //    float m = (count * sumXY - sumX * sumY) / (count * sumX2 - (sumX * sumX));
        //    float b = (sumY - m * sumX) / count;

        //    return new
        //    {
        //        data = stats,
        //        trendline = string.Format("{0}x {1} {2}", m, b >= 0 ? "+" : "", b)
        //    };
        //}

        private List<KeyValuePair<int, Diff>> GetStats()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            int minRound = int.Parse(nvc["minRound"]);
            int maxRound = int.Parse(nvc["maxRound"]);

            BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());

            var matches = service.Get(new SeasonKey[] { 
                new SeasonKey("England", 2014, 1),
                new SeasonKey("England", 2013, 1),
                new SeasonKey("England", 2012, 1),
                new SeasonKey("England", 2011, 1),
                new SeasonKey("England", 2010, 1),
                new SeasonKey("England", 2009, 1),
                new SeasonKey("England", 2008, 1),
                new SeasonKey("England", 2007, 1),
                new SeasonKey("England", 2006, 1),
                new SeasonKey("England", 2005, 1),
                new SeasonKey("England", 2004, 1),
                new SeasonKey("England", 2003, 1),
                new SeasonKey("England", 2002, 1),
                new SeasonKey("England", 2001, 1),
                new SeasonKey("England", 2000, 1),
                new SeasonKey("England", 1999, 1),
                new SeasonKey("England", 1998, 1),
                new SeasonKey("England", 1997, 1),
                new SeasonKey("England", 1996, 1),
                new SeasonKey("England", 1995, 1),
                new SeasonKey("England", 1994, 1),
                new SeasonKey("England", 1993, 1)
            });

            Dictionary<int, Diff> dic = new Dictionary<int, Diff>();

            foreach (var match in matches)
            {
                if (match.HtTableRow.Matches < minRound || match.HtTableRow.Matches > maxRound) continue;

                int diff = (match.HtTableRow.Position - match.AtTableRow.Position) * -1;

                Diff d;

                if (!dic.ContainsKey(diff))
                {
                    d = new Diff();
                    dic.Add(diff, d);
                }
                else
                {
                    d = dic[diff];
                }

                if (match.HtGoals > match.AtGoals) d.Home++;
                else if (match.AtGoals > match.HtGoals) d.Away++;
                else d.Draw++;

                int sum = d.Home + d.Away + d.Draw;

                d.HomeProb = d.Home > 0 ? (float)d.Home / (float)sum : 0;
                d.AwayProb = d.Away > 0 ? (float)d.Away / (float)sum : 0;
                d.DrawProb = d.Draw > 0 ? (float)d.Draw / (float)sum : 0;
            }

            return dic.OrderBy(d => d.Key).ToList();
        }
    }

    public class Diff
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Draw { get; set; }
        public float HomeProb { get; set; }
        public float AwayProb { get; set; }
        public float DrawProb { get; set; }
    }
}
