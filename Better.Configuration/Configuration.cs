using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Configuration
{
    public class Configurations
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
                if(Configurations._countryCodes == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._countryCodes;
            }
        }

        public static Dictionary<string, int> LeagueLevels
        {
            get
            {
                if (Configurations._leagueLevels == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._leagueLevels;
            }
        }

        public static Dictionary<string, string> LeagueNames
        {
            get
            {
                if (Configurations._leagueNames == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._leagueNames;
            }
        }

        public static IEnumerable<string> Seasons
        {
            get
            {
                if (Configurations._seasons == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._seasons;
            }
        }
        
        public static IEnumerable<string> ExceptionUrls
        {
            get
            {
                if (Configurations._exceptionUrls == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._exceptionUrls;
            }
        }

        public static string DataUrl
        {
            get
            {
                if (Configurations._dataUrl == null)
                {
                    Configurations.InitializeConfig();
                }

                return Configurations._dataUrl;
            }
        }

        public static string RemoteMatchUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RemoteMatchUrl"];
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

            Configurations._countryCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(countryCodes.ToString());
            Configurations._leagueNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(leagueNames.ToString());
            Configurations._leagueLevels = JsonConvert.DeserializeObject<Dictionary<string, int>>(leagueLevels.ToString());
            Configurations._seasons = JsonConvert.DeserializeObject<IEnumerable<string>>(seasons.ToString());
            Configurations._exceptionUrls = JsonConvert.DeserializeObject<IEnumerable<string>>(exceptionUrls.ToString());
            Configurations._dataUrl = dataUrl.ToString();
        }
    }
}
