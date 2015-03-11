using Better.Stats.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Data
{
    public interface IMatchRepository
    {
        IEnumerable<RawMatch> GetRawMatches();
        void AddRawMatch(RawMatch match);
        void AddMatch(Match match);
        void Save();
        IEnumerable<Match> GetMatches(MatchKey key);
        IEnumerable<Match> GetMatches(IEnumerable<MatchKey> keys);
    }

    public class FileSystemMatchRepository : IMatchRepository
    {
        private string _rootPath;
        private string _rawMatchesFolderName;
        private string _analyzedMatchesFolderName;
        private Dictionary<MatchKey, HashSet<RawMatch>> _rawMatches;
        private Dictionary<MatchKey, HashSet<Match>> _analyzedMatches;


        // ------------------ Public ------------------

        public FileSystemMatchRepository(string rootPath)
        {
            _rootPath = rootPath;
            this.SetDirectoryNames();
        }

        public IEnumerable<RawMatch> GetRawMatches()
        {
            return _rawMatches.SelectMany(k => k.Value);
        }

        public void AddRawMatch(RawMatch match)
        {
            if(!_rawMatches.ContainsKey(match.Key))
            {
                HashSet<RawMatch> keySet = new HashSet<RawMatch>();
                keySet.Add(match);
                _rawMatches.Add(match.Key, keySet);
            }
            else
            {
                _rawMatches[match.Key].Add(match);
            }
        }

        public void AddMatch(Match match)
        {
            if (!_analyzedMatches.ContainsKey(match.Key))
            {
                HashSet<Match> keySet = new HashSet<Match>();
                keySet.Add(match);
                _analyzedMatches.Add(match.Key, keySet);
            }
            else
            {
                _analyzedMatches[match.Key].Add(match);
            }
        }

        public void Save()
        {
            this.CreateRootPath(_rootPath);
            this.SaveSet<RawMatch>(Path.Combine(_rootPath, _rawMatchesFolderName), _rawMatches);
            this.SaveSet<Match>(Path.Combine(_rootPath, _analyzedMatchesFolderName), _analyzedMatches);
            //this.SaveRawMatches(Path.Combine(_rootPath, _rawMatchesFolderName), _rawMatches);
            //this.SaveAnalyzedMatches(Path.Combine(_rootPath, _analyzedMatchesFolderName), _analyzedMatches);
        }

        public IEnumerable<Match> GetMatches(MatchKey key)
        {
            string fileName = Path.Combine(_rootPath, _analyzedMatchesFolderName, key.Country, string.Concat(key.Level, ".json"));
            return this.LoadObject<HashSet<Match>>(fileName);
        }

        public IEnumerable<Match> GetMatches(IEnumerable<MatchKey> keys)
        {
            List<Match> matches = new List<Match>();
            foreach (var key in keys)
            {
                string fileName = Path.Combine(_rootPath, _analyzedMatchesFolderName, key.Country, string.Concat(key.Level, ".json"));
                matches.AddRange(this.LoadObject<HashSet<Match>>(fileName));
            }
            return matches;
        }


        // ------------------ Private ------------------

        private void CreateRootPath(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void SaveSet<T>(string rootPath, Dictionary<MatchKey, HashSet<T>> matches) where T : IMatch
        {
            this.CreateDirectories(rootPath, matches.Select(c => c.Key));

            foreach (var key in matches)
            {
                string fileName = Path.Combine(rootPath, key.Key.Country, string.Concat(key.Key.Level, ".json"));
                this.SaveObject(fileName, matches);
            }
        }

        private void SaveObject(string fileName, object obj)
        {
            string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write(json);
            sw.Close();
        }

        private void CreateDirectories(string rootPath, IEnumerable<MatchKey> keys)
        {
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);

            foreach (var key in keys)
            {
                Directory.CreateDirectory(Path.Combine(rootPath, key.Country));
            }
        }

        private void SetDirectoryNames()
        {
            _rawMatchesFolderName = ConfigurationManager.AppSettings["RawMatchesFolderName"];
            _analyzedMatchesFolderName = ConfigurationManager.AppSettings["AnalyzedMatchesFolderName"];
        }

        private T LoadObject<T>(string fileName)
        {
            if (!File.Exists(fileName)) return default(T);

            StreamReader sr = new StreamReader(fileName);
            string json = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void Load()
        {
            _rawMatches = this.LoadMatches<RawMatch>(Path.Combine(_rootPath, _rawMatchesFolderName));
            _analyzedMatches = this.LoadMatches<Match>(Path.Combine(_rootPath, _analyzedMatchesFolderName));
        }

        private Dictionary<MatchKey, HashSet<T>> LoadMatches<T>(string path) where T : IMatch
        {
            DirectoryInfo di = new DirectoryInfo(path);
            IEnumerable<DirectoryInfo> dirs = di.GetDirectories();
            Dictionary<MatchKey, HashSet<T>> matches = new Dictionary<MatchKey, HashSet<T>>();

            foreach (var dir in dirs)
            {
                IEnumerable<FileInfo> files = dir.GetFiles("*.json");
                foreach (var file in files)
                {
                    MatchKey key = new MatchKey
                    {
                        Country = dir.Name,
                        Level = int.Parse(file.Name.Replace(file.Extension, ""))
                    };
                    matches.Add(key, this.LoadObject<HashSet<T>>(file.FullName));
                }
            }
            return matches;
        }
    }
}
