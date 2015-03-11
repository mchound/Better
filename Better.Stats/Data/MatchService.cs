using Better.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Data
{
    public interface IMatchService
    {
        IEnumerable<Match> GetMatches(MatchKey key);
        IEnumerable<Match> GetMatches(IEnumerable<MatchKey> keys);
    }

    public class MatchService : IMatchService
    {
        private IMatchRepository _matchRepository;
        private IMatchCachingService _matchCache;

        public MatchService(IMatchRepository matchRepository, IMatchCachingService matchCache)
        {
            _matchRepository = matchRepository;
            _matchCache = matchCache;
        }

        public IEnumerable<Match> GetMatches(MatchKey key)
        {
            IEnumerable<Match> matches = _matchCache.GetMatches(key);

            if(matches == null)
            {
                matches = _matchRepository.GetMatches(key) ?? Enumerable.Empty<Match>();
                _matchCache.Add(key, matches);
            }

            return matches;
        }

        public IEnumerable<Match> GetMatches(IEnumerable<MatchKey> keys)
        {
            IEnumerable<Match> matches = Enumerable.Empty<Match>();
            foreach (var key in keys)
            {
                matches = matches.Concat(this.GetMatches(key));
            }

            return matches;
        }
    }
}
