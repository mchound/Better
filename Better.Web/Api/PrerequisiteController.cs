using Better.Data;
using Better.Data.Service;
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

namespace Better.Web.Api
{
    public class PrerequisiteController : ApiController
    {
        [HttpGet]
        [Route("api/prerequisites")]
        public object GetPrerequisites([ModelBinder(BinderType = typeof(JsonSerializedModelBinder<IEnumerable<SeasonKey>>), Name = "data")]IEnumerable<SeasonKey> seasonKeys)
        {
            BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());
            var matches = service.Get(seasonKeys);
            return new
            {
                matchCount = matches.Count(),
                teams = matches.Select(m => m.HomeTeam).Distinct()
            };
        }
    }

   

}
