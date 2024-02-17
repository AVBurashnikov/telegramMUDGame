using System.Text;

namespace MUD.BotCore
{
    internal class URLBuilder
    {
        private static readonly string _tgApiUrl = "https://api.telegram.org/";

        public static string Build(string token, string method, Dictionary<string, string>? parameters = null)
        {

            var sb = new StringBuilder(_tgApiUrl);

            sb.Append("bot" + token + "/" + method);

            if (parameters != null)
            {
                sb.Append('?');

                foreach (var kvp in parameters)
                {
                    sb.Append(kvp.Key + '=' + kvp.Value + '&');
                }
            }

            return sb.ToString();
        }
    }
}
