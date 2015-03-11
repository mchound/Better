using Better.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Better.Stats.Data
{

    public interface IMatchCachingService
    {
        IEnumerable<Match> GetMatches(MatchKey key);
        void Add(MatchKey key, IEnumerable<Match> matches);
    }

    public class MatchCachingService : IMatchCachingService
    {
        public IEnumerable<Match> GetMatches(MatchKey key)
        {
            object obj = HttpRuntime.Cache.Get(key.ToString());
            
            if(obj == null)
            {
                return null;
            }

            return obj as IEnumerable<Match>;
        }

        public void Add(MatchKey key, IEnumerable<Match> matches)
        {
            HttpRuntime.Cache.Add(key.ToString(), matches, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
        }
    }
}
