using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public class ConfigReader
    {
        public static ConfigReader configReader;
        
        private ConfigReader()
        {

        }
        public static ConfigReader GetInstance()
        {
            if (configReader == null)
            {
                object o = new object();
                lock (o)
                {
                    if (configReader == null)
                    {
                        configReader = new ConfigReader();
                    }
                }
            }
            return configReader;
        }

        public string GetAppValue(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                return ConfigurationManager.AppSettings[key];
            else
                return string.Empty;
        }

        public bool TryGetAppValue(string key,out string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                value = ConfigurationManager.AppSettings[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
