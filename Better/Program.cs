using Better.Stats;
using Better.Stats.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Better.Stats.Models;
using Better.Stats.Data;
using Better.Stats.Business;
using Newtonsoft.Json;
using System.Diagnostics;
using Better.Stats.Business.Filter;
using Better.Stats.Enums;

namespace Better.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            IMatchRepository repo = new FileSystemMatchRepository(@"C:\Temp\Better");
            IMatchCachingService cacheService = new MatchCachingService();
            IMatchService matchService = new MatchService(repo, cacheService);
            MatchFilterService filterService = new MatchFilterService(matchService);
            MatchFilterSettings settings = new MatchFilterSettings();
            settings.Keys.Add(new MatchKey { Country = "England", Level = 1 });

            TeamFilter teamFilter1 = new TeamFilter();
            teamFilter1.Add(new TeamSelector { TeamName = "Liverpool", Side = MatchSide.Home });
            teamFilter1.Add(new TeamSelector { TeamName = "Everton", Side = MatchSide.Away });
            teamFilter1.Add(new TeamSelector { TeamName = "Aston Villa", Side = MatchSide.Undefined });

            TeamFilter teamFilter2 = new TeamFilter();
            teamFilter2.Add(new TeamSelector { TeamName = "Coventry", Side = MatchSide.Home });
            teamFilter2.Add(new TeamSelector { TeamName = "Arsenal", Side = MatchSide.Away });
            teamFilter2.Add(new TeamSelector { TeamName = "West Ham", Side = MatchSide.Undefined });

            settings.AddTeamFilter(teamFilter1);
            settings.AddTeamFilter(teamFilter2);
            FilterResult result = filterService.FilterMatches(settings);
        }

        //static void Main(string[] args)
        //{
        //    StreamReader sr = new StreamReader(@"C:\Temp\SourceMatches.json");
        //    string json = sr.ReadToEnd();

        //    JsonSerializerSettings settings = new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.All
        //    };
        //    HashSet<IMatch> matches = JsonConvert.DeserializeObject<HashSet<IMatch>>(json, settings);

        //    IMatchRepository repo = new FileSystemMatchRepository2();

        //    double count = (double)matches.Count;
        //    double counter = 0;

        //    foreach (var match in matches)
        //    {
        //        repo.AddRawMatch(match);
        //        counter++;
        //        Console.WriteLine((Math.Round(counter / count, 2) * 100).ToString() + "%");
        //        //if (counter > 3) break;
        //    }

        //    repo.Save();

        //}

        //static void Main(string[] args)
        //{
        //    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //    sw.Start();
        //    StreamReader sr = new StreamReader(@"C:\Temp\SourceMatches.json");
        //    string json = sr.ReadToEnd();

        //    Console.WriteLine("Read full file: {0}", sw.Elapsed);
        //    sw.Restart();

        //    JsonSerializerSettings settings = new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.All
        //    };

        //    HashSet<IMatch> matches = JsonConvert.DeserializeObject<HashSet<IMatch>>(json, settings);

        //    Console.WriteLine("Deserialize: {0}", sw.Elapsed);
        //    sw.Restart();
        //    matches.Add(new RawMatch
        //    {
        //        HomeTeam = "Test",
        //        AwayTeam = "Test",
        //        Date = DateTime.Now,
        //        Country = "England",
        //        Season = "1516",
        //        Level = 0
        //    });

        //    Console.WriteLine("Add match: {0}", sw.Elapsed);
        //    Console.ReadLine();
        //}

        //static void Main(string[] args)
        //{
        //    IEnumerable<string> seasons = Better.Stats.Config.Configuration.Seasons;
        //    IEnumerable<string> exceptionUrls = Better.Stats.Config.Configuration.ExceptionUrls;
        //    Dictionary<string, string> countryCodes = Better.Stats.Config.Configuration.CountryCodes;
        //    string urlFormat = Better.Stats.Config.Configuration.DataUrl;
        //    WebClient client = new WebClient();
        //    IMatchRepository matchRepo = new FileSystemMatchRepository();

        //    foreach (var season in seasons)
        //    {
        //        foreach (var league in countryCodes)
        //        {

        //            Console.WriteLine("Downloading data for season: {0}, league: {1}", season, league.Key);
        //            string url = string.Format(urlFormat, season, league.Key);

        //            if (exceptionUrls.Any(u => u == url)) continue;

        //            string data = string.Empty;
        //            try
        //            {
        //                data = client.DownloadString(url);
        //            }
        //            catch
        //            {
        //                Console.WriteLine("!!! Warning: Data missing for season: {0}, league: {1}", season, league.Key);
        //                continue;
        //            }
        //            Console.WriteLine("Download finished for season: {0}, league: {1}", season, league.Key);
        //            string[] rows = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        //            for (int i = 1; i < rows.Length; i++)
        //            {
        //                if (rows[i].StartsWith(",")) continue;

        //                RawMatch match = MatchHelpers.FromCSVLine(rows[i]);

        //                match.Season = season;
        //                matchRepo.AddRawMatch(match);
        //            }

        //            Console.WriteLine("Matches added for season: {0}, league: {1}", season, league.Key);

        //        }
        //    }

        //    client.Dispose();
        //    matchRepo.Save();

        //}
    }
}
