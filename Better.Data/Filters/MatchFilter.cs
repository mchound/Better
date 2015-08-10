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
        public static bool Test(this MatchData match, IFilter filter, MatchSide side)
        {
            if (filter == null) return true;
            return filter.Test(match, side);
        }

        public static IEnumerable<MatchFilterResult> GetFilterResult(this IEnumerable<MatchData> matches, TeamFilter team1, TeamFilter team2)
        {
            foreach (var match in matches)
            {
                MatchFilterResult result = match.GetFilterResult(team1, team2);
                if (result != null) yield return result;
            }
        }

        public static MatchFilterResult GetFilterResult(this MatchData match, TeamFilter teamFilter1, TeamFilter teamFilter2)
        {
            switch (teamFilter1.Side)
            {
                case MatchSide.NotSet:
                    if(match.HtGoals == match.AtGoals)
                    {
                        return new MatchFilterResult
                        {
                            Draw = true,
                            Team1Goals = match.HtGoals,
                            Team2Goals = match.HtGoals,
                            HtGoals = match.HtGoals,
                            AtGoals = match.HtGoals
                        };
                    }
                    bool team1Home = teamFilter1.Test(match, MatchSide.Home);
                    bool team1Away = teamFilter1.Test(match, MatchSide.Away);
                    bool team2Home = teamFilter2.Test(match, MatchSide.Home);
                    bool team2Away = teamFilter2.Test(match, MatchSide.Away);
                    
                    if(team1Home && team1Away && team2Home && team2Away)
                    {
                        return new MatchFilterResult
                        {
                            Team1Win = match.HtGoals != match.AtGoals,
                            Team2Win = match.HtGoals != match.AtGoals,
                            Draw = match.HtGoals == match.AtGoals,
                            Team1Goals = (double)(match.HtGoals + match.AtGoals) / 2.0,
                            Team2Goals = (double)(match.HtGoals + match.AtGoals) / 2.0,
                            HtGoals = match.HtGoals,
                            AtGoals = match.AtGoals,
                            HtWin = match.HtGoals > match.AtGoals,
                            AtWin = match.HtGoals < match.AtGoals
                        };
                    }
                    else if ((team1Home && !team1Away && team2Away) || (!team2Home && team2Away && team1Home))
                    {
                        return new MatchFilterResult
                        {
                            Team1Win = match.HtGoals > match.AtGoals,
                            Team2Win = match.AtGoals > match.HtGoals,
                            Draw = match.HtGoals == match.AtGoals,
                            Team1Goals = match.HtGoals,
                            Team2Goals = match.AtGoals,
                            HtGoals = match.HtGoals,
                            AtGoals = match.AtGoals,
                            HtWin = match.HtGoals > match.AtGoals,
                            AtWin = match.HtGoals < match.AtGoals
                        };
                    }
                    else if((!team1Home && team1Away && team2Home) || (team2Home && !team2Away && team1Away))
                    {
                        return new MatchFilterResult
                        {
                            Team1Win = match.HtGoals < match.AtGoals,
                            Team2Win = match.AtGoals < match.HtGoals,
                            Draw = match.HtGoals == match.AtGoals,
                            Team1Goals = match.AtGoals,
                            Team2Goals = match.HtGoals,
                            HtGoals = match.HtGoals,
                            AtGoals = match.AtGoals,
                            HtWin = match.HtGoals > match.AtGoals,
                            AtWin = match.HtGoals < match.AtGoals
                        };
                    }
                    else
                    {
                        return null;
                    }
                case MatchSide.Home:
                    return new MatchFilterResult
                    {
                        Team1Win = match.HtGoals > match.AtGoals,
                        Team2Win = match.AtGoals > match.HtGoals,
                        Draw = match.HtGoals == match.AtGoals,
                        Team1Goals = match.HtGoals,
                        Team2Goals = match.AtGoals,
                        HtGoals = match.HtGoals,
                        AtGoals = match.AtGoals,
                        HtWin = match.HtGoals > match.AtGoals,
                        AtWin = match.HtGoals < match.AtGoals
                    };
                case MatchSide.Away:
                    return new MatchFilterResult
                    {
                        Team1Win = match.HtGoals < match.AtGoals,
                        Team2Win = match.AtGoals < match.HtGoals,
                        Draw = match.HtGoals == match.AtGoals,
                        Team1Goals = match.AtGoals,
                        Team2Goals = match.HtGoals,
                        HtGoals = match.HtGoals,
                        AtGoals = match.AtGoals,
                        HtWin = match.HtGoals > match.AtGoals,
                        AtWin = match.HtGoals < match.AtGoals
                    };
                default:
                    return null;
            }
        } 
    }

    public class MatchFilterResult
    {
        public bool Team1Win { get; set; }
        public bool Team2Win { get; set; }
        public bool HtWin { get; set; }
        public bool AtWin { get; set; }
        public bool Draw { get; set; }
        public double Team1Goals { get; set; }
        public double Team2Goals { get; set; }
        public int HtGoals { get; set; }
        public int AtGoals { get; set; }
    }

    public interface IFilter
    {
        bool Test(MatchData match, MatchSide side);
        bool Test(MatchData match);
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

        public bool Test(MatchData match)
        {
            if (string.IsNullOrWhiteSpace(this.TeamName)) return true;
            return match.HomeTeam == this.TeamName || match.AwayTeam == this.TeamName;
        }
    }

    public abstract class SpanFilter : IFilter
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool WithinSpan(int testValue)
        {
            return testValue >= this.Min && testValue <= this.Max; 
        }

        public bool Test(MatchData match)
        {
            return this.Test(match, MatchSide.Home) || this.Test(match, MatchSide.Away);
        }

        public abstract bool Test(MatchData match, MatchSide side);
    }

    public class MatchesFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Matches : match.AtTableRow.Matches);
        }
    }

    public class GoalsMadeFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.GoalsMade : match.AtTableRow.GoalsMade);
        }
    }

    public class GoalsConcededFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.GoalsConceded : match.AtTableRow.GoalsConceded);
        }
    }

    public class GoalsDiffFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.GoalsDiff : match.AtTableRow.GoalsDiff);
        }
    }

    public class WinsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Wins : match.AtTableRow.Wins);
        }
    }

    public class DrawsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Draws : match.AtTableRow.Draws);
        }
    }

    public class LossesFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Losses : match.AtTableRow.Losses);
        }
    }

    public class PointsFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Points : match.AtTableRow.Points);
        }
    }

    public class PositionFilter : SpanFilter
    {
        public override bool Test(MatchData match, MatchSide side)
        {
            return this.WithinSpan(side == MatchSide.Home ? match.HtTableRow.Position : match.AtTableRow.Position);
        }
    }

    public class MatchFilter
    {
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
                    return Enumerable.Empty<MatchData>();
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
