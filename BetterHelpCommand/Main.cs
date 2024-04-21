using Exiled.API.Features;
using HarmonyLib;
using System;
using static PlayerList;

namespace BetterHelpCommand
{
    public class Main : Plugin<Config>
    {
        public static Main Instance { get; set; }
        public static Harmony Harmony { get; set; }
        public override string Author { get; } = "yamato";

        public override void OnEnabled()
        {
            Instance = this;
            Harmony = new($"yamato.{Name}.{DateTime.Now}");
            Harmony.PatchAll();
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            Harmony.UnpatchAll(Harmony.Id);
            base.OnDisabled();
        }
    }
}
