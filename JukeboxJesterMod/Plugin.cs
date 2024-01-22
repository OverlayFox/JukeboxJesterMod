using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JukeboxJesterMod.Patches;
using LCSoundTool;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JukeboxJesterMod
{
    [BepInPlugin(modGuid, modName, modVersion)]
    public class JukeboxJesterBase : BaseUnityPlugin
    {
        private const string modGuid = "overlayFox.JukeboxJesterGuid";
        private const string modName = "Jukebox Jester";
        private const string modVersion = "1.1.0";

        public readonly Harmony harmony = new Harmony(modGuid);
        public static JukeboxJesterBase Instance;
        internal ManualLogSource mls;

        public string Location;
        public ConfigEntry<bool> Toggle { get; private set; }
        public ConfigEntry<int> Volume { get; private set; }
        public Dictionary<string, AudioClip> AudioFiles = new Dictionary<string, AudioClip>();
        private string[] AudioStrings = {
            "FreeBird_Start.mp3",
            "FreeBird_End01.mp3",
            "FreeBird_End02.mp3",
            "ChipiChipi_Start.mp3",
            "ChipiChipi_End.mp3",
            "INeedAHero_Start.mp3",
            "INeedAHero_End.mp3",
            "MyWay_Start.mp3",
            "MyWay_End.mp3",
            "Rats_Start.mp3",
            "Rats_End.mp3",
            "Toothless_Start.mp3",
            "Toothless_End.mp3",
            "GirlsClub_Start.mp3",
            "GirlsClub_End.mp3",
            "Ricky.mp3",
            "WeAreNumberOne_Start.mp3",
            "WeAreNumberOne_End01.mp3",
            "WeAreNumberOne_End02.mp3",
        };

        void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGuid);
            if (Instance == null)
            {
                mls.LogInfo("Starting Instance of Jukebox Jester");
                Instance = this;
            }

            mls.LogInfo("Getting Music directory");
            Location = Path.Combine(Path.GetDirectoryName(Info.Location), "music");
            if (!Directory.Exists(Location))
            {
                mls.LogError("Music folder doesn't exist. Can't load mod");
                return;
            }
            mls.LogInfo($"Music directory found under '{Location}'");
            
            mls.LogInfo("Loading Music....");
            foreach (string audio in AudioStrings)
            {
                string filePath = Path.Combine(Location, audio);
                if (!File.Exists(filePath))
                {
                    mls.LogWarning($"{filePath} does not exists and can thus not be loaded");
                    continue;
                }
                AudioFiles[audio.Substring(0, audio.Length - 4)] = SoundTool.GetAudioClip(Location, audio);
            }
            mls.LogInfo("Loaded all songs");

            _ = Config.Bind("Mod", "EnableMod", true, "Enables the Mod");
            Toggle = Config.Bind("Sound", "EnableJukeboxJester", true, "Enables the Jukebox Jester");
            Volume = Config.Bind("Sound", "VolumeControl", 50, new ConfigDescription("Changes the Volume of the Jukebox Jester", (AcceptableValueBase)(object)new AcceptableValueRange<int>(0,100), Array.Empty<object>()));

            harmony.PatchAll(typeof(JesterAiPatch));
            harmony.PatchAll(typeof(JukeboxJesterBase));
            mls.LogInfo("Jukebox Jester initialized");
        }
    }
}
