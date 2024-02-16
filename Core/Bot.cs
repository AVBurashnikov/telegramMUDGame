using MUD.Utils;
using System.Text.Json;

namespace MUD.Core
{
    internal class Bot
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string? _token;

        public Bot(string token)
        {
            _token = token;
        }

        public async Task InfinityPolling()
        {
            Console.WriteLine("Start polling!");
            while (true)
            {
                await GetUpdates();
            }
        }

        private async Task GetUpdates()
        {
            int offset = 0;
            string method = "getUpdates";
            var parameters = new Dictionary<string, string>();
            parameters.Add("offset", Convert.ToString(offset));
            parameters.Add("timeout", Convert.ToString(60));

            string url = URLBuilder.GetUrl(_token, method, parameters);

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                Console.WriteLine(response);
                var updateResponse = JsonSerializer.Deserialize<UpdateResponse>(response);

                if (updateResponse != null && updateResponse.result.Length > 0)
                {
                    foreach (var update in updateResponse.result)
                    {
                        DateTime date = DateTimeOffset.FromUnixTimeSeconds(update.message.date).DateTime;

                        string sDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                        string sUserName = $"{update.message.from.first_name} {update.message.from.last_name}";
                        string sText = update.message.text;

                        // Handle the update (e.g., process the message)
                        if (!string.IsNullOrEmpty(sText))
                        {
                            Console.WriteLine($"[{sUserName}] {sDate}: '{sText}'");

                        }

                        offset = update.update_id + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
