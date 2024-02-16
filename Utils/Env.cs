namespace MUD.Utils
{
    internal class Env
    {
        private static Dictionary<string, string> dict = [];

        public static string? Item(string key)
        {
            if (dict.TryGetValue(key, out string? value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static void Load(string path)
        {
            string? line;
            string[] pair;
            StreamReader sr = new StreamReader(path);

            while ((line = sr.ReadLine()) != null)
            {
                pair = line.Split('=');
                dict.Add(pair[0], pair[1]);
            }
        }
    }
}
