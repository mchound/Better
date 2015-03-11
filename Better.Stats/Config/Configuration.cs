using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Stats.Config
{
    public class Configuration
    {
        private static Dictionary<string, string> _countryCodes;
        private static Dictionary<string, int> _leagueLevels;
        private static Dictionary<string, string> _leagueNames;
        private static IEnumerable<string> _seasons;
        private static IEnumerable<string> _exceptionUrls;
        private static string _dataUrl;

        public static Dictionary<string, string> CountryCodes
        {
            get
            {
                if(Configuration._countryCodes == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._countryCodes;
            }
        }

        public static Dictionary<string, int> LeagueLevels
        {
            get
            {
                if (Configuration._leagueLevels == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._leagueLevels;
            }
        }

        public static Dictionary<string, string> LeagueNames
        {
            get
            {
                if (Configuration._leagueNames == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._leagueNames;
            }
        }

        public static IEnumerable<string> Seasons
        {
            get
            {
                if (Configuration._seasons == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._seasons;
            }
        }
        
        public static IEnumerable<string> ExceptionUrls
        {
            get
            {
                if (Configuration._exceptionUrls == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._exceptionUrls;
            }
        }

        public static string DataUrl
        {
            get
            {
                if (Configuration._dataUrl == null)
                {
                    Configuration.InitializeConfig();
                }

                return Configuration._dataUrl;
            }
        }

        private static void InitializeConfig()
        {
            string configFileName = ConfigurationManager.AppSettings["ConfigFile"];
            StreamReader sr = new StreamReader(configFileName);
            dynamic config = JsonConvert.DeserializeObject(sr.ReadToEnd());

            JObject countryCodes = config.countryCodes;
            JObject leagueNames = config.leagueNames;
            JObject leagueLevels = config.leagueLevels;
            JArray seasons = config.seasons;
            JArray exceptionUrls = config.exceptionUrls;
            JValue dataUrl = config.dataUrl;

            Configuration._countryCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(countryCodes.ToString());
            Configuration._leagueNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(leagueNames.ToString());
            Configuration._leagueLevels = JsonConvert.DeserializeObject<Dictionary<string, int>>(leagueLevels.ToString());
            Configuration._seasons = JsonConvert.DeserializeObject<IEnumerable<string>>(seasons.ToString());
            Configuration._exceptionUrls = JsonConvert.DeserializeObject<IEnumerable<string>>(exceptionUrls.ToString());
            Configuration._dataUrl = dataUrl.ToString();
        }
    }
}
