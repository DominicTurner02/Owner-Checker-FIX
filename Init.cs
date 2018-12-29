using Rocket.Core.Plugins;
using SDG.Unturned;
using SDPlugins;
using Steamworks;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using Rocket.Unturned;

namespace SDPlugins
{
    public class Init : RocketPlugin<OwnerCheckConfig>
    {
        public static Init Instance;

        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.LogWarning("[SDPlugins] Owner check loaded!");
            if (Configuration.Instance.usePlayerInfoLib)
            {
                Rocket.Core.Logging.Logger.LogWarning("[SDPlugins] Player Info Lib will be used!");
            }
            else
            {
                Rocket.Core.Logging.Logger.LogWarning("[SDPlugins] Player Info Lib will not be used!");
            }
        }
        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("[SDPlugins] Owner Check unloaded!");
        }
    }
}
