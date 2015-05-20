using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Service
{
    public class MatchService
    {
        private List<Table> _tables;
        private BetterContext _db;
        private ICacheService _cacheService;

        public MatchService(BetterContext db, ICacheService cacheService)
        {
            _db = db;
            _tables = new List<Table>();
            _cacheService = cacheService;
        }

        public IEnumerable<MatchData> Get(string country, int startYear, int level)
        {
            return this.Get(new SeasonKey(country, startYear, level));
        }

        public IEnumerable<MatchData> Get(SeasonKey key)
        {
            var matches = this.GetFromCache(key);

            if (matches == null)
            {
                matches = new HashSet<MatchData>(this.GetFromDatabase(key));
                _cacheService.Add(matches, key);
            }

            return matches;
        }

        public IEnumerable<MatchData> Get(IEnumerable<SeasonKey> keys)
        {
            IEnumerable<MatchData> matches = Enumerable.Empty<MatchData>();

            foreach (var key in keys)
            {
                matches = matches.Concat(this.Get(key));
            }

            return matches;
        }

        private IEnumerable<MatchData> GetFromDatabase(SeasonKey key)
        {
            var matches = _db.RawMatches.Where(m => m.Country == key.Country && m.Season == key.Season && m.Level == key.Level);
            matches = matches.OrderBy(m => m.Date);

            foreach (var match in matches)
            {
                yield return this.AddMatch(match);
            }
        }

        private IEnumerable<MatchData> GetFromCache(SeasonKey key)
        {
            return _cacheService.Get<IEnumerable<MatchData>>(key);
        }

        private MatchData AddMatch(RawMatch match)
        {
            Table table = this.GetTable(match);
            TableRow htRow = table.GetRow(match, MatchSide.Home).Clone();
            TableRow atRow = table.GetRow(match, MatchSide.Away).Clone();
            table.AddMatch(match);
            return new MatchData(match, htRow, atRow);
        }

        private Table GetTable(RawMatch match)
        {
            Table table = _tables.FirstOrDefault(t => t.Country == match.Country && t.Season == match.Season && t.Level == match.Level);

            if(table == null){
                table = new Table
                {
                    Country = match.Country,
                    Level = match.Level,
                    Season = match.Season
                };
                _tables.Add(table);
            }

            return table;
        }
    }
}
