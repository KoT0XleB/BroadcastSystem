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

namespace BroadcastSystem
{
    public class BroadcastSystem : Plugin
    {
        public override string Developer => "KoToXleB#4663";
        public override string Name => "BroadcastSystem";
        public override Version Version => new Version(1, 0, 0);
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
                int randomValue = UnityEngine.Random.Range(0, CustomConfig.BroadcastMessages.Count);
                Map.Broadcast($"{CustomConfig.BroadcastMessages[randomValue]}", 10);
                CustomConfig.BroadcastMessages.Remove(CustomConfig.BroadcastMessages[randomValue]);

                yield return Timing.WaitForSeconds(CustomConfig.BroadcastMinutes * 60f);
            }
            yield break;
        }
        public static void PlayerJoin(JoinEvent ev)
        {
            ev.Player.Broadcast($"<color=yellow>{CustomConfig.Hello}</color> <color=red>{ev.Player.Nickname}</color> <color=yellow>{CustomConfig.Text}</color>\n<b>{Qurre.API.Server.Name}</b>", 7);
        }
    }
}
