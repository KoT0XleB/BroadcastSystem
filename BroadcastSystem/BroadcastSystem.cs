using MEC;
using Qurre;
using Qurre.API;
using Qurre.Events;
using Qurre.API.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Round = Qurre.API.Round;
using Map = Qurre.API.Map;
using Random = UnityEngine.Random;
using Server = Qurre.API.Server;

namespace BroadcastSystem
{
    public class BroadcastSystem : Plugin
    {
        public override string Developer => "KoToXleB#4663";
        public override string Name => "BroadcastSystem";
        public override Version Version => new Version(1, 0, 1);
        public override int Priority => int.MinValue;
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public static Config CustomConfig { get; private set; }
        public void RegisterEvents()
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start += RoundStarted;
            Qurre.Events.Player.Join += PlayerJoin;
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start -= RoundStarted;
            Qurre.Events.Player.Join -= PlayerJoin;
        }
        public static void RoundStarted()
        {
            CustomConfig.Reload();
            Timing.CallDelayed(CustomConfig.BroadcastStartWaiting * 60f, () =>
            {
                Timing.RunCoroutine(BroadcastTiming(), "Broadcast");
            });
        }
        public static IEnumerator<float> BroadcastTiming()
        {
            while (!Round.Ended || CustomConfig.BroadcastMessages.Count > 0)
            {
                int randomValue = Random.Range(0, CustomConfig.BroadcastMessages.Count);
                Map.Broadcast($"{CustomConfig.BroadcastMessages[randomValue]}", CustomConfig.BroadcastLongSeconds);
                CustomConfig.BroadcastMessages.Remove(CustomConfig.BroadcastMessages[randomValue]);

                yield return Timing.WaitForSeconds(CustomConfig.BroadcastMinutes * 60f);
            }
            yield break;
        }
        public void PlayerJoin(JoinEvent ev)
        {
            string Message = CustomConfig.Text;
            Message = Message.Replace("%player%", ev.Player.Nickname);
            ev.Player.Broadcast(Message, CustomConfig.BroadcastLongSeconds);
        }
    }
}
