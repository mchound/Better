using Better.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data.Filters
{
    public static class MatchFilterExtensions
    {
        public static IEnumerable<MatchData> Filter(this IEnumerable<MatchData> matches, MatchFilter filter)
        {
            return filter.FilterMatches(matches);
        }

        //public static IEnumerable<MatchData> Filter(this IEnumerable<MatchData> matches, TeamFilter filter)
        //{
        //    return filter.Filter(matches);
        //}

        public static bool Test(this MatchData match, IFilter filter, MatchSide side)
        {
            if (filter == null) return true;
            return filter.Test(match, side);
        }
    }

    public interface IFilter
    {
        bool Test(MatchData match, MatchSide side);
    }

    public class TeamNameFilter : IFilter
    {
        public string TeamName { get; set; }

        public bool Test(MatchData match, MatchSide side)
        {
            if (string.IsNullOrWhiteSpace(this.TeamName)) return true;

            string teamName = side == MatchSide.Home ? match.HomeTeam : match.AwayTeam;
            return teamName == this.TeamName;
        }
    }

    public abstract class SpanFilter : IFilter
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool Test(int testValue)
        {
            return testValue >= this.Min && testValue <= this.Max; 
        }

        public abstract bool Test(MatchData match, MatchSide side);
    }

    public class MatchesFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Matches : match.AtTableRow.Matches);
        }
    }

    public class GoalsMadeFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.GoalsMade : match.AtTableRow.GoalsMade);
        }
    }

    public class GoalsConcededFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.GoalsConceded : match.AtTableRow.GoalsConceded);
        }
    }

    public class GoalsDiffFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.GoalsDiff : match.AtTableRow.GoalsDiff);
        }
    }

    public class WinsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Wins : match.AtTableRow.Wins);
        }
    }

    public class DrawsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Draws : match.AtTableRow.Draws);
        }
    }

    public class LossesFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Losses : match.AtTableRow.Losses);
        }
    }

    public class PointsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Points : match.AtTableRow.Points);
        }
    }

    public class PositionFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.Test(side == MatchSide.Home ? match.HtTableRow.Position : match.AtTableRow.Position);
        }
    }

    public class MatchFilter
    {
        public string Country { get; set; }
        public IEnumerable<int> Seasons { get; set; }
        public IEnumerable<int> Levels { get; set; }
        public TeamFilter Team1 { get; set; }
        public TeamFilter Team2 { get; set; }

        public MatchFilter()
        {
            this.Team1 = new TeamFilter();
            this.Team2 = new TeamFilter();
        }

        public IEnumerable<MatchData> FilterMatches(IEnumerable<MatchData> matches)
        {
            switch (this.Team1.Side)
            {
                case MatchSide.NotSet:
                    return matches.Where(m => (this.Team1.Test(m, MatchSide.Home) && this.Team2.Test(m, MatchSide.Away)) || (this.Team1.Test(m, MatchSide.Away) && this.Team2.Test(m, MatchSide.Home)));
                case MatchSide.Home:
                    return matches.Where(m => (this.Team1.Test(m, MatchSide.Home) && this.Team2.Test(m, MatchSide.Away)));
                case MatchSide.Away:
                    return matches.Where(m => (this.Team1.Test(m, MatchSide.Away) && this.Team2.Test(m, MatchSide.Home)));
                default:
                    return matches;
            }
        }
    }

    public class TeamFilter
    {
        public MatchSide Side { get; set; }
        public TeamNameFilter TeamNameFilter { get; set; }
        public PositionFilter PositionFilter { get; set; }
        public MatchesFilter MatchesFilter { get; set; }
        public GoalsMadeFilter GoalsMadeFilter { get; set; }
        public GoalsConcededFilter GoalsConcededFilter { get; set; }
        public GoalsDiffFilter GoalsDiffFilter { get; set; }
        public WinsFilter WinsFilter { get; set; }
        public DrawsFilter DrawsFilter { get; set; }
        public LossesFilter LossesFilter { get; set; }
        public PointsFilter PointsFilter { get; set; }

        public bool Test(MatchData match, MatchSide side)
        {
            return 
                match.Test(TeamNameFilter, side) &&
                match.Test(MatchesFilter, side) &&
                match.Test(PositionFilter, side) &&
                match.Test(MatchesFilter, side) &&
                match.Test(GoalsMadeFilter, side) &&
                match.Test(GoalsConcededFilter, side) &&
                match.Test(GoalsDiffFilter, side) &&
                match.Test(WinsFilter, side) &&
                match.Test(DrawsFilter, side) &&
                match.Test(LossesFilter, side) &&
                match.Test(PointsFilter, side);
        }
    }

}
