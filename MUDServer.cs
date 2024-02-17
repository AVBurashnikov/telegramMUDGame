using MUD.BotCore;
using MUD.Utils;

namespace MUD
{


    class MUDServer
    {
        public static async Task Main()
        {
            Env.Load(".env");
            string? token = Env.Item("token");
            ArgumentNullException.ThrowIfNullOrEmpty(token);

            var bot = new Bot(token);
            await bot.InfinityPolling();
        }
    }    
}
