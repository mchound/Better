using Better.Stats.Business;
using Better.Stats.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Data
{
    //public interface IMatchRepository
    //{
    //    IEnumerable<Match> Matches {get;}
    //    void AddRawMatch(IMatch match);
    //    void Save();
    //    void ReBuildMatches(IMatchAnalyzer matchAnalyzer);
    //    void BuildMatches(IMatchAnalyzer matchAnalyzer, Func<IMatch, bool> predicate);
    //}

    //public abstract class BaseMatchRepository : IMatchRepository
    //{
    //    #region Public Properties

    //    public abstract IEnumerable<Match> Matches {get;}

    //    #endregion

    //    #region Protected Properties

    //    protected bool Initialized { get; private set; }

    //    #endregion

    //    #region Constructors

    //    public BaseMatchRepository()
    //    {
    //        this.Init();
    //    }

    //    #endregion

    //    #region Public

    //    public void AddRawMatch(IMatch match)
    //    {
    //        this.InitializeCheck();
    //        if (this.MatchExists(match)) throw new MatchRepositoryException("Match already exists");
    //        this.AddRawMatchInternal(match);
    //    }

    //    #endregion

    //    #region Abstract

    //    public abstract void Save();

    //    public abstract void ReBuildMatches(IMatchAnalyzer matchAnalyzer);

    //    public abstract void BuildMatches(IMatchAnalyzer matchAnalyzer, Func<IMatch, bool> predicate);

    //    protected abstract bool MatchExists(IMatch match);

    //    protected abstract void AddRawMatchInternal(IMatch match);

    //    #endregion

    //    #region Virtual

    //    protected virtual void Init()
    //    {
    //        this.Initialized = true;
    //    }

    //    #endregion
        
    //    #region Protected

    //    protected void InitializeCheck()
    //    {
    //        if (!this.Initialized) throw new MatchRepositoryException("Match Repository is not initialized");
    //    }
 
    //    #endregion
    //}

    ////public class FileSystemMatchRepository : BaseMatchRepository
    ////{

    ////    #region Private Properties

    ////    private HashSet<IMatch> _rawMatches;
    ////    private List<Match> _matches;
    ////    private string _storePath;
    ////    private const string RAW_MATCHES_FILE_NAME = "RawMatches.json";
    ////    private const string MATCHES_FILE_NAME = "Matches.json";

    ////    #endregion

    ////    #region Public Properties

    ////    public override IEnumerable<Match> Matches
    ////    {
    ////        get { return _matches; }
    ////    }

    ////    #endregion

    ////    #region Constructors

    ////    public FileSystemMatchRepository() : base()
    ////    {
    ////    }

    ////    #endregion

    ////    #region Override abstract base methods

    ////    protected override void Init()
    ////    {
    ////        this.SetStorePath();
    ////        this.LoadMatches();
    ////        base.Init();
    ////    }

    ////    protected override bool MatchExists(IMatch match)
    ////    {
    ////        return _rawMatches.Any(m => m.HomeTeam.Equals(match.HomeTeam) && m.AwayTeam.Equals(match.AwayTeam) && m.Date.Equals(match.Date));
    ////    }

    ////    protected override void AddRawMatchInternal(IMatch match)
    ////    {
    ////        _rawMatches.Add(match);
    ////    }

    ////    public override void ReBuildMatches(IMatchAnalyzer matchAnalyzer)
    ////    {
    ////        this.InitializeCheck();

    ////        _matches = matchAnalyzer.Analyze(_rawMatches);
    ////    }

    ////    public override void BuildMatches(IMatchAnalyzer matchAnalyzer, Func<IMatch, bool> predicate)
    ////    {
    ////        this.InitializeCheck();

    ////        _matches = _matches.Cast<IMatch>().Except(_matches.Cast<IMatch>().Where(predicate)).Cast<Match>().ToList();
    ////        _matches.AddRange(matchAnalyzer.Analyze(_rawMatches.Where(predicate)));
    ////    }

    ////    public override void Save()
    ////    {
    ////        this.InitializeCheck();

    ////        if (string.IsNullOrWhiteSpace(_storePath)) throw new MatchRepositoryException("Store path not specified");

    ////        this.SaveMatches();
    ////    }

    ////    #endregion

    ////    #region Private

    ////    private void SaveMatches()
    ////    {
    ////        this.SaveFile(Path.Combine(_storePath, RAW_MATCHES_FILE_NAME), _rawMatches, TypeNameHandling.All);
    ////        this.SaveFile(Path.Combine(_storePath, MATCHES_FILE_NAME), _matches, TypeNameHandling.None);
    ////    }

    ////    private void SaveFile(string fileName, object obj, TypeNameHandling nameHandling)
    ////    {
    ////        string json = Serialize(obj, nameHandling);
    ////        StreamWriter sw = new StreamWriter(fileName);
    ////        sw.Write(json);
    ////        sw.Close();
    ////    }

    ////    private void SetStorePath()
    ////    {
    ////        _storePath = ConfigurationManager.AppSettings["StorePath"] ?? string.Empty;
    ////    }

    ////    private void LoadMatches()
    ////    {
    ////        _rawMatches = this.LoadData<HashSet<IMatch>>(RAW_MATCHES_FILE_NAME) ?? new HashSet<IMatch>();
    ////        _matches = this.LoadData<List<Match>>(MATCHES_FILE_NAME) ?? new List<Match>();
    ////    }

    ////    private T LoadData<T>(string fileName)
    ////    {
    ////        if (string.IsNullOrWhiteSpace(_storePath)) return default(T);

    ////        string json = this.LoadFile(Path.Combine(_storePath, fileName));

    ////        if (string.IsNullOrWhiteSpace(json)) return default(T);

    ////        return this.DeSerialize<T>(json);

    ////    }

    ////    private string LoadFile(string fileName)
    ////    {
    ////        if (!File.Exists(fileName)) return string.Empty;

    ////        StreamReader sr = new StreamReader(fileName);
    ////        string json = sr.ReadToEnd();
    ////        sr.Close();
    ////        return json;
    ////    }

    ////    private string Serialize(object obj, TypeNameHandling nameHandling)
    ////    {
    ////        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { TypeNameHandling = nameHandling });
    ////    }

    ////    private T DeSerialize<T>(string json)
    ////    {
    ////        return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All});
    ////    }

    ////    #endregion
    ////}

    //public class FileSystemMatchRepository2 : BaseMatchRepository
    //{

    //    #region Private Properties

    //    private MatchDirectory<RawMatch> _rawMatches;
    //    private MatchDirectory<Match> _matches;
    //    private string _storePath;
    //    private const string RAW_MATCHES_FOLDER_NAME = "RawMatches";
    //    private const string MATCHES_FOLDER_NAME = "Matches";

    //    #endregion

    //    #region Public Properties

    //    public override IEnumerable<Match> Matches
    //    {
    //        get { return _matches.Matches; }
    //    }

    //    #endregion

    //    #region Constructors

    //    public FileSystemMatchRepository2() : base()
    //    {
    //    }

    //    #endregion

    //    #region Override abstract base methods

    //    protected override void Init()
    //    {
    //        this.SetStorePath();
    //        this.LoadMatches();
    //        base.Init();
    //    }

    //    protected override bool MatchExists(IMatch match)
    //    {
    //        return _rawMatches.Contains(match);
    //    }

    //    protected override void AddRawMatchInternal(IMatch match)
    //    {
    //        _rawMatches.Add(match as RawMatch);
    //    }

    //    public override void ReBuildMatches(IMatchAnalyzer matchAnalyzer)
    //    {
    //        this.InitializeCheck();

    //        _rawMatches = new MatchDirectory<RawMatch>(Path.Combine(_storePath, RAW_MATCHES_FOLDER_NAME));
    //        _matches = matchAnalyzer.Analyze(_rawMatches.Matches);
    //    }

    //    public override void BuildMatches(IMatchAnalyzer matchAnalyzer, Func<IMatch, bool> predicate)
    //    {
    //        this.InitializeCheck();

    //        //_matches = _matches.Cast<IMatch>().Except(_matches.Cast<IMatch>().Where(predicate)).Cast<Match>().ToList();
    //        //_matches.AddRange(matchAnalyzer.Analyze(_rawMatches.Where(predicate)));
    //    }

    //    public override void Save()
    //    {
    //        this.InitializeCheck();

    //        if (string.IsNullOrWhiteSpace(_storePath)) throw new MatchRepositoryException("Store path not specified");

    //        this.SaveMatches();
    //    }

    //    #endregion

    //    #region Private

    //    private void SaveMatches()
    //    {
    //        _rawMatches.Save(Path.Combine(_storePath, RAW_MATCHES_FOLDER_NAME));
    //        _matches.Save(Path.Combine(_storePath, MATCHES_FOLDER_NAME));
    //    }

    //    private void SetStorePath()
    //    {
    //        _storePath = ConfigurationManager.AppSettings["StorePath"] ?? string.Empty;
    //    }

    //    private void LoadMatches()
    //    {
    //        _matches = new MatchDirectory<Match>(Path.Combine(_storePath, MATCHES_FOLDER_NAME));
    //    }

    //    #endregion
    //}

    //public class MatchDirectory<T> where T : IMatch
    //{
    //    private Dictionary<MatchKey, HashSet<T>> _matches;

    //    private int _count = 0;
    //    public int Count
    //    {
    //        get
    //        {
    //            return _count;
    //        }
    //    }

    //    public IEnumerable<T> Matches
    //    {
    //        get
    //        {
    //            return _matches.SelectMany(c => c.Value);
    //        }
    //    }

    //    public MatchDirectory()
    //    {
    //        _matches = new Dictionary<MatchKey, HashSet<T>>();
    //    }

    //    public MatchDirectory(string path)
    //    {
    //        _matches = new Dictionary<MatchKey, HashSet<T>>();
    //        this.Load(path);
    //    }

    //    public void Add(T item)
    //    {
    //        HashSet<T> matches = this.GetMatchHashSet(item);
    //        matches.Add(item);
    //        _count++;
    //    }

    //    public bool Contains(IMatch item)
    //    {
    //        return _matches.ContainsKey(item.Key) && _matches[item.Key].Any(m => m.Equals(item));
    //    }

    //    public void Save(string path)
    //    {
    //        DirectoryInfo root = Directory.CreateDirectory(path);

    //        foreach (var hashSet in _matches)
    //        {
    //            Directory.CreateDirectory(Path.Combine(root.FullName, hashSet.Key.Country));
    //            string fileName = Path.Combine(path, hashSet.Key.Country, string.Concat(hashSet.Key.Level, ".json"));
    //            string json = this.Serialize(hashSet.Value, TypeNameHandling.All);
    //            StreamWriter sw = new StreamWriter(fileName);
    //            sw.Write(json);
    //            sw.Close();
    //        }
    //    }

    //    private void Load(string path)
    //    {
    //        if (!Directory.Exists(path)) return;

    //        IEnumerable<DirectoryInfo> directories = new DirectoryInfo(path).GetDirectories();

    //        foreach (var dir in directories)
    //        {
    //            foreach (var file in dir.GetFiles())
    //            {
    //                StreamReader sr = new StreamReader(file.FullName);
    //                string json = sr.ReadToEnd();
    //                HashSet<T> hashSet = this.DeSerialize<HashSet<T>>(json);
    //                _matches.Add(new MatchKey { Country = dir.Name, Level = int.Parse(file.Name.Replace(file.Extension, "")) }, hashSet);
    //            }
    //        }
    //    }

    //    private string Serialize(object obj, TypeNameHandling nameHandling)
    //    {
    //        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { TypeNameHandling = nameHandling });
    //    }

    //    private T DeSerialize<T>(string json)
    //    {
    //        return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    //    }

    //    private HashSet<T> GetMatchHashSet(T match)
    //    {
    //        HashSet<T> hashSet;

    //        if(!_matches.ContainsKey(match.Key))
    //        {
    //            hashSet = new HashSet<T>();
    //            _matches.Add(match.Key, hashSet);
    //        }
    //        else
    //        {
    //            hashSet = _matches[match.Key];
    //        }

    //        return hashSet;
    //    }

    //}

    //// Exceptions

    //public class MatchRepositoryException : Exception
    //{
    //    public MatchRepositoryException(string message) : base(message){}
    //}
}
