using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.Configuration
{
    public static class ConfigHelper
    {
        public static T GetConfigValue<T>(string key)
        {
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)bool.Parse(GetAppSetting(key));
            }

            if (typeof(T) == typeof(int))
            {
                return (T)(object)int.Parse(GetAppSetting(key));
            }

            if (typeof(T) == typeof(double))
            {
                return (T)(object)double.Parse(GetAppSetting(key));
            }

            throw new NotSupportedException(typeof(T).Name);
        }

        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
