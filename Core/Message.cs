using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD.Core
{
    internal class Message
    {
        public int message_id { get; set; }
        public User from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public Entity[] entities { get; set; }
    }
}
