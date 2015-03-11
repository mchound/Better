using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Enums
{
    public enum MatchSide
    {
        Undefined = 0,
        Home = 1,
        Away = 2
    }

    public enum TableDefinition
    {
        Position = 0,
        Wins = 1,
        Draws = 2,
        Losses = 3,
        GoalsFor = 4,
        GoalsAgainst = 5,
        GoalDiff = 6,
        Points = 7
    }
}
