using Better.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Better.Stats.Helpers;
using Better.Stats.Enums;
using Better.Stats.Data;

namespace Better.Stats.Business
{
    public interface IMatchAnalyzer : IDisposable
    {
        IEnumerable<Match> Analyze(IEnumerable<IMatch> rawMatches);
    }

    public class MatchAnalyzer : IMatchAnalyzer
    {
        private TableStore _tableStore;
        private StreakStore _streakStore;

        public IEnumerable<Match> Analyze(IEnumerable<IMatch> rawMatches)
        {
            _tableStore = new TableStore();
            _streakStore = new StreakStore();

            foreach (var match in rawMatches)
            {
                yield return this.AnalyzeMatch(match);
            }
        }

        private Match AnalyzeMatch(IMatch match)
        {
            Match _match = match.FromRaw();
            _match.HtTable = _tableStore.GetTableRow(_match, MatchSide.Home);
            _match.AtTable = _tableStore.GetTableRow(_match, MatchSide.Away);
            _match.HtStreak = _streakStore.GetStreak(_match.HomeTeam);
            _match.AtStreak = _streakStore.GetStreak(_match.AwayTeam);
            _tableStore.AddMatch(_match);
            _streakStore.AddMatch(_match);
            return _match;
        }

        public void Dispose()
        {
            _tableStore.Dispose();
            _streakStore.Dispose();
        }
    }
}
