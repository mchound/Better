using Better.Data;
using Better.Data.Service;
using Better.Web.Models;
using Better.Web.Models.ModelBinders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Better.Data.Filters;
using System.IO;
using Better.Remote;

namespace Better.Web.Api
{
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("api/addMatch")]
        public RawMatch AddMatch(RawMatch match)
        {
            BetterContext db = new BetterContext();
            var added = db.RawMatches.Add(match);
            db.SaveChanges();
            return added;
        }

        [HttpPost]
        [Route("api/addMany")]
        public IEnumerable<RawMatch> AddMany(IEnumerable<RawMatch> matches)
        {
            List<RawMatch> added = new List<RawMatch>();
            BetterContext db = new BetterContext();
            foreach (var match in matches)
            {
                added.Add(db.RawMatches.Add(match));
            }
            db.SaveChanges();
            return added;
        }

        [HttpGet]
        [Route("api/fetchMatches/{code}/{country}/{level:int}/{startYear:int}")]
        public object FetchMatches(string code, string country, int level, int startYear)
        {
            BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());
            var existingMatches = db.RawMatches;
            List<object> matches = new List<object>();
            StreamReader sr = new StreamReader(MatchHelper.GetRemoteCSVStream(code, country, level, startYear));
            sr.ReadLine();
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                RawMatch rawMatch = MatchHelper.FromCSVLine(line, startYear);
                string id = string.Concat(rawMatch.Date.ToString("yyyy-MM-dd"), rawMatch.HomeTeam, rawMatch.AwayTeam);
                var match = new
                {
                    id = id,
                    date = rawMatch.Date.ToString("yyyy-MM-dd"),
                    homeTeam = rawMatch.HomeTeam,
                    awayTeam = rawMatch.AwayTeam,
                    htGoals = rawMatch.HtGoals,
                    atGoals = rawMatch.AtGoals,
                    level = rawMatch.Level,
                    country = rawMatch.Country,
                    season = rawMatch.Season,
                    exists = existingMatches.Any(m => m.Id == id)
                };
                matches.Add(match);
            }
            sr.Close();
            return matches;
        }
    }

   

}
