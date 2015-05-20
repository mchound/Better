using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Service
{
    public class Table
    {
        private List<TableRow> _rows;
        private List<RawMatch> _pendingMatches;
        private DateTime _lastDate = DateTime.MinValue;

        public string Country { get; set; }
        public string Season { get; set; }
        public int Level { get; set; }

        public Table()
        {
            _rows = new List<TableRow>();
            _pendingMatches = new List<RawMatch>();
        }

        public void AddMatch(RawMatch match)
        {
            if (_lastDate == DateTime.MinValue) _lastDate = match.Date;

            if (_lastDate != match.Date)
            {
                this.ProcessPendingMatches();
                _lastDate = match.Date;
            }

            _pendingMatches.Add(match);
        }

        public TableRow GetRow(RawMatch match, MatchSide side)
        {
            string team = side == MatchSide.Home ? match.HomeTeam : match.AwayTeam;
            TableRow row = _rows.FirstOrDefault(r => r.Team == team);

            if(row == null)
            {
                row = new TableRow(team);
                _rows.Add(row);
            }

            return row;            
        }

        public void ProcessPendingMatches()
        {
            foreach (var match in _pendingMatches)
            {
                this.UpdateRow(match, MatchSide.Home);
                this.UpdateRow(match, MatchSide.Away);
            }

            this.UpdatePositions();
            this._pendingMatches.Clear();
        }

        private void UpdatePositions()
        {
            _rows = _rows.OrderByDescending(r => r.Points).ThenByDescending(r => r.GoalsDiff).ThenByDescending(r => r.GoalsMade).ThenBy(r => r.GoalsConceded).ThenBy(r => r.Team).ToList();

            for (int i = 0; i < _rows.Count; i++)
            {
                _rows[i].Position = i + 1;
            }
        }

        private void UpdateRow(RawMatch match, MatchSide side)
        {
            string team = side == MatchSide.Home ? match.HomeTeam : match.AwayTeam;
            int goalsFor = side == MatchSide.Home ? match.HtGoals : match.AtGoals;
            int goalsAgainst = side == MatchSide.Home ? match.AtGoals : match.HtGoals;
            TableRow row = _rows.First(r => r.Team == team);
            row.Matches++;
            row.Draws += goalsFor == goalsAgainst ? 1 : 0;
            row.Wins += goalsFor > goalsAgainst ? 1 : 0;
            row.Losses += goalsFor < goalsAgainst ? 1 : 0;
            row.GoalsMade += goalsFor;
            row.GoalsConceded += goalsAgainst;
            row.GoalsDiff = row.GoalsMade - row.GoalsConceded;
            row.Points += goalsFor > goalsAgainst ? 3 : goalsFor == goalsAgainst ? 1 : 0;
        }
    }
}
