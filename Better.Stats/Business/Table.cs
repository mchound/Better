using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Better.Stats.Models;
using Better.Stats.Enums;

namespace Better.Stats.Business
{
    public class TableStore : IDisposable
    {
        private List<Table> _tables;

        public TableStore()
        {
            _tables = new List<Table>();
        }

        public void AddMatch(Match match)
        {
            Table table = this.GetTable(match);
            table.AddMatch(match);
        }

        public TableRow GetTableRow(Match match, MatchSide side)
        {
            Table table = this.GetTable(match);

            if (side == MatchSide.Home) return table.RowByTeam(match.HomeTeam);
            return table.RowByTeam(match.AwayTeam);
        }

        private Table GetTable(Match match)
        {
            Table table = _tables.FirstOrDefault(t => t.Country == match.Country && t.Level == match.Level && t.Season == match.Season);

            if (table == null)
            {
                table = new Table(match);
                _tables.Add(table);
            }

            return table;
        }

        public void Dispose()
        {
            this._tables.Clear();
        }
    }

    public class TableSet
    {
        public string Team { get; set; }
        public int Matches { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDiff { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }

        public TableSet(string team)
        {
            this.Team = team;
            this.Matches = 0;
            this.GoalsFor = 0;
            this.GoalsAgainst = 0;
            this.GoalDiff = 0;
            this.Wins = 0;
            this.Losses = 0;
            this.Draws = 0;
            this.Points = 0;
            this.Position = 0;
        }

        public void AddMatch(int goalsFor, int goalsAgainst)
        {
            this.Matches++;
            this.GoalsFor += goalsFor;
            this.GoalsAgainst += goalsAgainst;
            this.GoalDiff = this.GoalsFor - this.GoalsAgainst;
            this.Wins += goalsFor > goalsAgainst ? 1 : 0;
            this.Losses += goalsFor < goalsAgainst ? 1 : 0;
            this.Draws += goalsFor == goalsAgainst ? 1 : 0;
            this.Points += (goalsFor > goalsAgainst) ? 3 : (goalsFor == goalsAgainst ? 1 : 0);
        }

        public TableSet Clone()
        {
            return new TableSet(this.Team)
            {
                Matches = this.Matches,
                GoalsFor = this.GoalsFor,
                GoalsAgainst = this.GoalsAgainst,
                GoalDiff = this.GoalDiff,
                Wins = this.Wins,
                Losses = this.Losses,
                Draws = this.Draws,
                Points = this.Points,
                Position = this.Position
            };
        }
    }

    public class Table
    {
        private List<Match> _matchQue;

        public string Country { get; set; }
        public string Season { get; set; }
        public int Level { get; set; }
        public List<TableSet> Home { get; set; }
        public List<TableSet> Away { get; set; }
        public List<TableSet> Total { get; set; }

        public Table()
        {
            this.Home = new List<TableSet>();
            this.Away = new List<TableSet>();
            this.Total = new List<TableSet>();
            this._matchQue = new List<Match>();
        }

        public Table(Match match) : this()
        {
            this.Country = match.Country;
            this.Season = match.Season;
            this.Level = match.Level;
        }

        public TableRow RowByTeam(string team)
        {
            TableSet home = this.Home.FirstOrDefault(s => s.Team == team);
            TableSet away = this.Away.FirstOrDefault(s => s.Team == team);
            TableSet total = this.Total.FirstOrDefault(s => s.Team == team);
            return new TableRow(home, away, total);
        }

        public void AddMatch(Match match)
        {
            if (_matchQue.Count == 0 || _matchQue.First().Date == match.Date)
            {
                _matchQue.Add(match);
            }
            else
            {
                AddQue();
                this._matchQue.Clear();
                _matchQue.Add(match);
                OrderSets();
                SetPositions();
            }
        }

        private void SetPositions()
        {
            SetPositions(this.Home);
            SetPositions(this.Away);
            SetPositions(this.Total);
        }

        private void SetPositions(List<TableSet> sets)
        {
            for (int i = 0; i < sets.Count; i++)
            {
                sets[i].Position = i + 1;
            }
        }

        private void AddQue()
        {
            foreach (var match in _matchQue)
            {
                AddQuedMatch(match);
            }
        }

        private void AddQuedMatch(Match match)
        {
            TableSet homeSet = this.GetSet(this.Home, match.HomeTeam);
            homeSet.AddMatch(match.HtGoals, match.AtGoals);
            TableSet awaySet = this.GetSet(this.Away, match.AwayTeam);
            awaySet.AddMatch(match.AtGoals, match.HtGoals);
            TableSet homeTotalSet = this.GetSet(this.Total, match.HomeTeam);
            homeTotalSet.AddMatch(match.HtGoals, match.AtGoals);
            TableSet awayTotalSet = this.GetSet(this.Total, match.AwayTeam);
            awayTotalSet.AddMatch(match.AtGoals, match.HtGoals);
        }

        private void OrderSets()
        {
            this.Home = this.Home.OrderByDescending(s => s.Points).ThenByDescending(s => s.GoalDiff).ThenByDescending(s => s.GoalsFor).ThenBy(s => s.GoalsAgainst).ThenBy(s => s.Team).ToList();
            this.Away = this.Away.OrderByDescending(s => s.Points).ThenByDescending(s => s.GoalDiff).ThenByDescending(s => s.GoalsFor).ThenBy(s => s.GoalsAgainst).ThenBy(s => s.Team).ToList();
            this.Total = this.Total.OrderByDescending(s => s.Points).ThenByDescending(s => s.GoalDiff).ThenByDescending(s => s.GoalsFor).ThenBy(s => s.GoalsAgainst).ThenBy(s => s.Team).ToList();
        }

        private TableSet GetSet(List<TableSet> list, string team)
        {
            TableSet set = list.FirstOrDefault(s => s.Team == team);
            if (set == null)
            {
                set = new TableSet(team);
                list.Add(set);
            }
            return set;
        }
    }

    public class TableRow
    {
        public int[] Home { get; set; }
        public int[] Away { get; set; }
        public int[] Total { get; set; }

        public TableRow()
        {

        }

        public TableRow(TableSet home, TableSet away, TableSet total)
        {
            this.Home = new int[9];
            this.Away = new int[9];
            this.Total = new int[9];

            if(home != null) InitArray(this.Home, home);
            if(away != null) InitArray(this.Away, away);
            if(total != null) InitArray(this.Total, total);
        }

        private void InitArray(int[] array, TableSet set)
        {   
            array[0] = set.Position;
            array[1] = set.Matches;
            array[2] = set.Wins;
            array[3] = set.Draws;
            array[4] = set.Losses;
            array[5] = set.GoalsFor;
            array[6] = set.GoalsAgainst;
            array[7] = set.GoalDiff;
            array[8] = set.Points;
        }
    }
}
