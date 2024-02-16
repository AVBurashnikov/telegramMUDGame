using MUD.Core;
using MUD.Utils;

namespace MUD
{


    class MUDServer
    {
        public static async Task Main()
        {
            string token;

            Env.Load(".env");
            if ((token = Env.Item("token")) == null)
            {
                throw new Exception("Token not found.");
            }

            var bot = new Bot(token);
            await bot.InfinityPolling();
        }
    }    
}
