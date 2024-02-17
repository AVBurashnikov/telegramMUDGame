using System.Text.Json;

namespace MUD.BotCore
{
    internal class Bot
    {
        private readonly HttpClient _httpClient = new();
        private string _token;
        private int _offset = 0;

        public Bot(string token)
        {
            _token = token;
        }

        public async Task InfinityPolling()
        {
            while (true)
            {
                await GetUpdates();
            }
        }

        public async Task SendMessage(int chatId, 
                                      string text, 
                                      int messageThreadId = 0,
                                      string parseMode = "HTML", 
                                      MessageEntity[]? entities = null,
                                      LinkPreviewOptions? linkPreviewOptions = null, 
                                      bool disableNotification = false,
                                      bool protectContent = false, 
                                      ReplyParameters? replyParameters = null,
                                      object? replyMarkup = null)
        {
            string method = "sendMessage";
            Dictionary<string, string> parameters = new();
            parameters.Add("chat_id", Convert.ToString(chatId));
            parameters.Add("text", text);
            parameters.Add("parse_mode", parseMode);

            string url = URLBuilder.Build(_token, method, parameters);
            var response = await _httpClient.GetStringAsync(url);
            Console.WriteLine(response);
        }

        public async Task GetMe(int chatId)
        {
            string method = "getMe";
            Dictionary<string, string> parameters = new();
            parameters.Add("chat_id", Convert.ToString(chatId));
            string url = URLBuilder.Build(_token, method, parameters);

            var response = await _httpClient.GetStringAsync(url);
            Console.WriteLine(response);
        }

        private async Task GetUpdates()
        {
            string method = "getUpdates";
            Dictionary<string, string> parameters = new()
            {
                { "offset", Convert.ToString(_offset) },
                { "timeout", Convert.ToString(60) }
            };

            string url = URLBuilder.Build(_token, method, parameters);

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var updateResponse = JsonSerializer.Deserialize<UpdateResponse>(response);

                if (updateResponse != null && updateResponse.result.Length > 0)
                {
                    foreach (var update in updateResponse.result)
                    {
                        DateTime date = DateTimeOffset.FromUnixTimeSeconds(update.message.date).DateTime;

                        string sDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                        string sUserName = $"{update.message.from.first_name} {update.message.from.last_name}";
                        string sText = update.message.text;

                        if (!string.IsNullOrEmpty(sText))
                        {
                            await SendMessage(update.message.chat.id, sText);
                            Console.WriteLine($"[{sUserName}] {sDate}: '{sText}'");
                        }

                        _offset = update.update_id + 1;
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
