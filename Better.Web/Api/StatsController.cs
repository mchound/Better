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

namespace Better.Web.Api
{
    public class StatsController : ApiController
    {
        [HttpGet]
        [Route("api/stats")]
        public object GetStats([ModelBinder(BinderType = typeof(JsonSerializedModelBinder<StatsRequestModel>), Name = "data")]StatsRequestModel statsRequest)
        {
            BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());
            var matches = service.Get(statsRequest.SeasonKeys);
            var filteredMatches = statsRequest.MatchFilter.FilterMatches(matches);
            var filterResults = filteredMatches.GetFilterResult(statsRequest.MatchFilter.Team1, statsRequest.MatchFilter.Team2);
            double matchCount = filterResults.Count();
            return new
            {
                matchCount = matchCount,
                team1Wins = (double)filterResults.Count(f => f.Team1Win) / matchCount,
                team2Wins = filterResults.Count(f => f.Team2Win) / matchCount,
                draws = filterResults.Count(f => f.Draw) / matchCount,
                matches = filteredMatches
            };
        }
    }

   

}
