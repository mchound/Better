using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Better.Data;
using Better.Stats.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Better.Data.Service;
using Better.Data.Filters;

namespace Better.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Better.Data.BetterContext db = new BetterContext();
            MatchService service = new MatchService(db, new DefaultCacheService());

            var matches = service.Get(new SeasonKey[] { 
                new SeasonKey("England", 2014, 1),
                new SeasonKey("England", 2013, 1),
                new SeasonKey("England", 2012, 1),
                new SeasonKey("England", 2011, 1),
                new SeasonKey("England", 2010, 1),
                new SeasonKey("England", 2009, 1),
                new SeasonKey("England", 2008, 1),
                new SeasonKey("England", 2007, 1),
                new SeasonKey("England", 2006, 1),
                new SeasonKey("England", 2005, 1),
                new SeasonKey("England", 2004, 1),
                new SeasonKey("England", 2003, 1),
                new SeasonKey("England", 2002, 1),
                new SeasonKey("England", 2001, 1),
                new SeasonKey("England", 2000, 1),
                new SeasonKey("England", 1999, 1),
                new SeasonKey("England", 1998, 1),
                new SeasonKey("England", 1997, 1),
                new SeasonKey("England", 1996, 1),
                new SeasonKey("England", 1995, 1),
                new SeasonKey("England", 1994, 1),
                new SeasonKey("England", 1993, 1)
            });

            MatchFilter matchFilter = new MatchFilter();
            
            matchFilter.Team1.Side = MatchSide.Home;
            matchFilter.Team1.MatchesFilter = new MatchesFilter { Min = 34, Max = 36 };
            matchFilter.Team1.PositionFilter = new PositionFilter { Min = 5, Max = 8 };
            matchFilter.Team1.PointsFilter = new PointsFilter { Min = 60, Max = 64 };

            matchFilter.Team2.MatchesFilter = new MatchesFilter { Min = 34, Max = 36 };
            matchFilter.Team2.PositionFilter = new PositionFilter { Min = 10, Max = 14 };
            matchFilter.Team2.PointsFilter = new PointsFilter { Min = 38, Max = 46 };


            var filtered = matches.Filter(matchFilter).ToList();
            

            //var matches = service.Get(new SeasonKey[] {
            //    new SeasonKey("England", 2000, 1),
            //    new SeasonKey("England", 2001, 1),
            //    new SeasonKey("England", 2002, 1),
            //    new SeasonKey("England", 2003, 1),
            //    new SeasonKey("England", 2004, 1),
            //    new SeasonKey("England", 2005, 1),
            //    new SeasonKey("England", 2006, 1),
            //    new SeasonKey("England", 2007, 1),
            //    new SeasonKey("England", 2008, 1),
            //    new SeasonKey("England", 2009, 1),
            //    new SeasonKey("England", 2010, 1),
            //    new SeasonKey("England", 2012, 1),
            //    new SeasonKey("England", 2013, 1),
            //    new SeasonKey("England", 2014, 1)
            //});
            
            
        }

        //static void Main(string[] args)
        //{
        //    BetterContext context = new BetterContext();
        //    //Better.Data.BetterContext context = new Data.BetterContext();

        //    IEnumerable<int> nums = new int[] {1,2};

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    var result = context.RawMatches.Select(r => r.AwayTeam != "Everton" && (r.HomeTeam == "Liverpool" || r.HtGoals >= 3)).IsIn(r => r.Level, nums).Execute().ToList();
        //    //var result = context.RawMatches.Where(r => r.Level == 1 && r.HomeTeam == "Liverpool" || r.HtGoals >= 3).ToList();
        //    int count = result.Count;
        //    sw.Stop();
        //    Console.WriteLine("{0} objects on {1}ms", count, sw.ElapsedMilliseconds);
        //    Console.ReadLine();
        //}

        //public class BetterContext : DbContext
        //{
        //    public IDbTable<Better.Data.RawMatch> RawMatches {get; set;}

        //    public BetterContext() : base(@"Data Source=WIN-HS00N1EGH3F\SQLEXPRESS;Initial Catalog=Better;Persist Security Info=True;User ID=developer;Password=d3v3l0p3r")
        //    {

        //    }
        //}

        //static void Main(string[] args)
        //{
        //    StreamReader sr = new StreamReader(@"C:\Temp\Better\SourceMatches.json");
        //    string json = sr.ReadToEnd();

        //    JsonSerializerSettings settings = new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.All
        //    };
        //    HashSet<IMatch> matches = JsonConvert.DeserializeObject<HashSet<IMatch>>(json, settings);

        //    string lastCountry = matches.First().Country;
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    DataTable table = new DataTable("RawMatches");
        //    DataColumn id = new DataColumn("Id", typeof(string));
        //    DataColumn datetime = new DataColumn("Date", typeof(DateTime));
        //    DataColumn homeTeam = new DataColumn("HomeTeam", typeof(string));
        //    DataColumn awayTeam = new DataColumn("AwayTeam", typeof(string));
        //    DataColumn country = new DataColumn("Country", typeof(string));
        //    DataColumn season = new DataColumn("Season", typeof(string));
        //    DataColumn htGoals = new DataColumn("HtGoals", typeof(Int32));
        //    DataColumn atGoals = new DataColumn("AtGoals", typeof(Int32));
        //    DataColumn level = new DataColumn("Level", typeof(Int32));
        //    table.Columns.Add(id);
        //    table.Columns.Add(datetime);
        //    table.Columns.Add(homeTeam);
        //    table.Columns.Add(awayTeam);
        //    table.Columns.Add(country);
        //    table.Columns.Add(season);
        //    table.Columns.Add(htGoals);
        //    table.Columns.Add(atGoals);
        //    table.Columns.Add(level);
        //    BetterContext db = new BetterContext();

        //    var bulk = new SqlBulkCopy(db.Database.Connection.ConnectionString);

        //    bulk.DestinationTableName = table.TableName;

        //    foreach (var col in table.Columns)
        //        bulk.ColumnMappings.Add(col.ToString(), col.ToString());

        //    foreach (var match in matches)
        //    {
        //        DataRow row = table.NewRow();
        //        row["Id"] = string.Concat(match.Date.ToString("yyyy-MM-dd"), match.HomeTeam, match.AwayTeam);
        //        row["Date"] = match.Date;
        //        row["HomeTeam"] = match.HomeTeam;
        //        row["AwayTeam"] = match.AwayTeam;
        //        row["Country"] = match.Country;
        //        row["Season"] = match.Season;
        //        row["HtGoals"] = match.HtGoals;
        //        row["AtGoals"] = match.AtGoals;
        //        row["Level"] = match.Level;
        //        table.Rows.Add(row);

        //        if (lastCountry != match.Country)
        //        {
        //            bulk.WriteToServer(table);
        //            table.Rows.Clear();
        //            Console.WriteLine(string.Format("{0} - {1}: {2} - {3}", match.Date, match.Country, match.HomeTeam, match.AwayTeam));
        //            lastCountry = match.Country;
        //            Console.WriteLine("Elapsed time: {0}s", sw.Elapsed.Seconds);
        //            sw.Restart();
        //        }

        //    }
        //}

        //static void Main(string[] args)
        //{
        //    IMatchRepository repo = new FileSystemMatchRepository(@"C:\Temp\Better");
        //    IMatchCachingService cacheService = new MatchCachingService();
        //    IMatchService matchService = new MatchService(repo, cacheService);
        //    MatchFilterService filterService = new MatchFilterService(matchService);
        //    MatchFilterSettings settings = new MatchFilterSettings();
        //    settings.Keys.Add(new MatchKey { Country = "England", Level = 1 });

        //    TeamFilter teamFilter1 = new TeamFilter();
        //    teamFilter1.Add(new TeamSelector { TeamName = "Liverpool", Side = MatchSide.Home });
        //    teamFilter1.Add(new TeamSelector { TeamName = "Everton", Side = MatchSide.Away });
        //    teamFilter1.Add(new TeamSelector { TeamName = "Aston Villa", Side = MatchSide.Undefined });

        //    TeamFilter teamFilter2 = new TeamFilter();
        //    teamFilter2.Add(new TeamSelector { TeamName = "Coventry", Side = MatchSide.Home });
        //    teamFilter2.Add(new TeamSelector { TeamName = "Arsenal", Side = MatchSide.Away });
        //    teamFilter2.Add(new TeamSelector { TeamName = "West Ham", Side = MatchSide.Undefined });

        //    settings.AddTeamFilter(teamFilter1);
        //    settings.AddTeamFilter(teamFilter2);
        //    FilterResult result = filterService.FilterMatches(settings);
        //}

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

    public static class Extensions
    {
        public static bool IsIn<T>(this IEnumerable<T> items, Expression<Func<T, bool>> predicate)
        {
            return true;
        }
    }
}
