using Qurre.API;
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
        [Description("How many minutes should I wait after the start of the round?")]
        public int BroadcastStartWaiting { get; set; } = 4;
        [Description("How many minutes do I have to wait after the next Broadcast message?")]
        public int BroadcastMinutes { get; set; } = 4;
        [Description("How long in seconds will the message be displayed to the player?")]
        public ushort BroadcastLongSeconds { get; set; } = 10;
        [Description("Text for the player when he logged in to the server.")]
        public string Text { get; set; } = $"<color=yellow>Hello <color=red>%player%</color> are you playing on the server</color>\n<b><color=yellow>My Server Name</color></b>";
        [Description("It is necessary to write Broadcast messages here.")]
        public List<string> BroadcastMessages { get; set; } = new List<string>()
        {
            "<color=yellow>Hello</color>",
            "<color=yellow>How are you</color>",
            "<color=yellow>Join in my Discord</color>"
        };
    }
}
