using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Service
{
    public class TableRow
    {
        public string Team { get; set; }
        public int Position { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsMade { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsDiff { get; set; }
        public int Points { get; set; }

        public TableRow()
        {

        }

        public TableRow(string team)
        {
            Position = 0;
            Team = team;
            Draws = 0;
            GoalsConceded = 0;
            GoalsDiff = 0;
            GoalsMade = 0;
            Losses = 0;
            Matches = 0;
            Points = 0;
            Wins = 0;
        }

        public TableRow Clone()
        {
            return new TableRow
            {
                Team = this.Team,
                Draws = this.Draws,
                GoalsConceded = this.GoalsConceded,
                GoalsDiff = this.GoalsDiff,
                GoalsMade = this.GoalsMade,
                Losses = this.Losses,
                Matches = this.Matches,
                Points = this.Points,
                Position = this.Position,
                Wins = this.Wins
            };
        }
    }
}
