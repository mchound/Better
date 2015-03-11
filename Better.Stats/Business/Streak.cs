using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Better.Stats.Models;

namespace Better.Stats.Business
{
    public class StreakStore : IDisposable
    {
        private Dictionary<string, Streak> _streaks;

        public StreakStore()
        {
            _streaks = new Dictionary<string, Streak>();
        }

        public Streak GetStreak(string team)
        {
            return this.GetWorkingStreak(team).Clone();
        }

        public void AddMatch(Match match)
        {
            Streak homeStreak = this.GetWorkingStreak(match.HomeTeam);
            Streak awayStreak = this.GetWorkingStreak(match.AwayTeam);
            homeStreak.Home = UpdateStreak(homeStreak.Home, match.HtGoals, match.AtGoals).ToList();
            homeStreak.Total = UpdateStreak(homeStreak.Total, match.HtGoals, match.AtGoals).ToList();
            awayStreak.Away = UpdateStreak(awayStreak.Away, match.AtGoals, match.HtGoals).ToList();
            awayStreak.Total = UpdateStreak(awayStreak.Total, match.AtGoals, match.HtGoals).ToList();
        }

        private Streak GetWorkingStreak(string team)
        {
            if (_streaks.ContainsKey(team)) return _streaks[team];

            Streak streak = new Streak();
            _streaks.Add(team, streak);
            return streak;
        }

        private IEnumerable<int> UpdateStreak(List<int> streak, int goalsFor, int goalsAgainst)
        {
            int points = goalsFor > goalsAgainst ? 3 : goalsFor == goalsAgainst ? 1 : 0;
            streak.Add(points);
            return streak.GetRange(Math.Max(0, streak.Count - 10), Math.Min(10, streak.Count));
        }

        public void Dispose()
        {
            _streaks.Clear();
        }
    }

    public class Streak
    {
        public List<int> Home { get; set; }
        public List<int> Away { get; set; }
        public List<int> Total { get; set; }

        public Streak()
        {
            this.Home = new List<int>();
            this.Away = new List<int>();
            this.Total = new List<int>();
        }

        public Streak Clone()
        {
            return new Streak
            {
                Home = new List<int>(this.Home),
                Away = new List<int>(this.Away),
                Total = new List<int>(this.Total)
            };
        }
    }


}
