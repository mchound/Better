using Better.Stats.Data;
using Better.Stats.Enums;
using Better.Stats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Business.Filter
{
    public class MatchFilterService
    {
        private IMatchService _matchService;

        public MatchFilterService(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public FilterResult FilterMatches(MatchFilterSettings settings)
        {
            IEnumerable<Match> matches = _matchService.GetMatches(settings.Keys);

            foreach (var filter in settings.TeamFilters)
            {
                matches = matches.Filter(filter);
            }

            return matches.AsFilterResult();
        }
    }

    public class FilterResult
    {
        public int HomeWins { get; set; }
        public int AwayWins { get; set; }
        public int Draws { get; set; }
        public int Matches { get; set; }
        public double HomeShare { get { return Math.Round((double)HomeWins * 100.0 / (double)Matches, 2); } }
        public double AwayShare { get { return Math.Round((double)AwayWins * 100.0 / (double)Matches, 2); } }
        public double DrawShare { get { return Math.Round((double)Draws * 100.0 / (double)Matches, 2); } }
    }

    public static class FilterExtensions
    {
        public static FilterResult AsFilterResult(this IEnumerable<Match> matches)
        {
            return new FilterResult
            {
                Matches = matches.Count(),
                HomeWins = matches.Count(m => m.HtGoals > m.AtGoals),
                AwayWins = matches.Count(m => m.HtGoals < m.AtGoals),
                Draws = matches.Count(m => m.HtGoals == m.AtGoals)
            };
        }

        public static IEnumerable<Match> Filter(this IEnumerable<Match> matches, IMatchFilter filter)
        {
            if (filter == null) return matches;

            return matches.Where(filter.Predicate);
        }
    }

    public class MatchFilterSettings
    {
        public List<MatchKey> Keys { get; set; }
        public List<TeamFilter> TeamFilters { get; private set; }

        public MatchFilterSettings()
        {
            Keys = new List<MatchKey>();
        }

        public void AddTeamFilter(TeamFilter teamFilter)
        {
            if (this.TeamFilters == null) this.TeamFilters = new List<TeamFilter>();
            this.TeamFilters.Add(teamFilter);
        }
    }

    public interface IMatchFilter
    {
        Func<Match, bool> Predicate {get;}
    }

    public class TeamFilter : IMatchFilter
    {
        private List<TeamSelector> _selectors;
        public IEnumerable<TeamSelector> Selectors { get { return _selectors; } }
        public Func<Match, bool> Predicate 
        { 
            get
            {
                return m =>
                    Selectors.Count() == 0 ||
                    Selectors.Any(s => s.Side == MatchSide.Home && s.TeamName == m.HomeTeam) ||
                    Selectors.Any(s => s.Side == MatchSide.Away && s.TeamName == m.AwayTeam) ||
                    Selectors.Any(s => s.Side == MatchSide.Undefined && (s.TeamName == m.HomeTeam || s.TeamName == m.AwayTeam));
            }
        }

        public TeamFilter()
        {
            _selectors = new List<TeamSelector>();
        }

        public void Add(TeamSelector selector)
        {
            _selectors.Add(selector);
        }
    }

    public abstract class IntegerFilter : IMatchFilter
    {
        public abstract Func<Match, bool> Predicate {get;}

        public int Max { get; set; }
        public int Min { get; set; }
        public MatchSide Side { get; set; }
    }


    //public class PositionFilter : IntegerFilter
    //{
    //    public Func<Match, bool> Predicate
    //    {
    //        get 
    //        { 
    //            return m =>
    //                m.HtTable.
    //        }
    //    }
    //}


    public class TeamSelector
    {
        public string TeamName { get; set; }
        public MatchSide Side { get; set; }
    }
}
