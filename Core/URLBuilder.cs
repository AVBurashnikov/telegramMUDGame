using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD.Core
{
    internal class URLBuilder
    {
        private static readonly string _apiUrlFormat = "https://api.telegram.org/bot{" + "{0}" + "}/{1}&{2}";

        public static string GetUrl(string token, string method, Dictionary<string, string>? parameters = null)
        {

            var sb = new StringBuilder();

            if (parameters != null)
            {
                foreach (var kvp in parameters)
                {
                    sb.Append($"{kvp.Key}={kvp.Value}?");
                }
            }

            return string.Format(_apiUrlFormat, token, method, sb.ToString());
        }
    }
}
