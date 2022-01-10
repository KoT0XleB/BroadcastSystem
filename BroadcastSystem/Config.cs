using Qurre.API.Addons;
using System.Collections.Generic;
using System.ComponentModel;

namespace BroadcastSystem
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "BroadcastSystem";

        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;
        [Description("Сколько надо ждать минут после начала раунда? How many minutes should I wait after the start of the round?")]
        public int BroadcastStartWaiting { get; set; } = 4;
        [Description("Сколько надо ждать минут после очередного сообщения Broadcast? How many minutes do I have to wait after the next Broadcast message?")]
        public int BroadcastMinutes { get; set; } = 4;
        [Description("Сообщение Привет. Message Hello.")]
        public string Hello { get; set; } = "Hello";
        [Description("Сообщение Привет. Message Hello.")]
        public string Text { get; set; } = "are you playing on the server";
        [Description("Необходимо сюда написать сообщения Broadcast. It is necessary to write Broadcast messages here.")]
        public List<string> BroadcastMessages { get; set; } = new List<string>()
        {
            "<color=yellow>Hello</color>",
            "<color=yellow>How are you</color>",
            "<color=yellow>Join in my Discord</color>"
        };
    }
}
