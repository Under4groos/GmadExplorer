using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmadExplorer.Extensions
{
    public static class ExtDictionary
    {
        public static string GetValueStr(this Dictionary<string, string> keyValuePairs, string key)
        {
            key += ".png";
            if (keyValuePairs.ContainsKey(key))
                return Path.GetFullPath(keyValuePairs[key]);
            return string.Empty;
        }
    }
}
