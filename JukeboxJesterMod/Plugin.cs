using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JukeboxJesterMod.Patches;
using LCSoundTool;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JukeboxJesterMod
{
    [BepInPlugin(modGuid, modName, modVersion)]
    public class JukeboxJesterBase : BaseUnityPlugin
    {
        private const string modGuid = "overlayFox.JukeboxJesterGuid";
        private const string modName = "Jukebox Jester";
        private const string modVersion = "1.0.0";

        public readonly Harmony harmony = new Harmony(modGuid);
        public static JukeboxJesterBase Instance;
        internal ManualLogSource mls;

        public string Location;
        public ConfigEntry<bool> Toggle { get; private set; }
        public ConfigEntry<int> Volume { get; private set; }
        public AudioClip StartFreeBird { get; private set; }
        public AudioClip End1FreeBird { get; private set; }
        public AudioClip End2FreeBird { get; private set; }
        public AudioClip StartChipi { get; private set; }
        public AudioClip EndChipi { get; private set; }
        public AudioClip StartINeedAHero { get; private set; }
        public AudioClip EndINeedAHero { get; private set; }
        public AudioClip StartMyWay { get; private set; }
        public AudioClip EndMyWay { get; private set; }
        // public AudioClip StartNyanCat { get; private set; }
        // public AudioClip EndNyanCat { get; private set; }
        public AudioClip StartWeAreTheRats { get; private set; }
        public AudioClip EndWeAreTheRats { get; private set; }
        public AudioClip StartToothless { get; private set; }
        public AudioClip EndToothless { get; private set; }


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
            StartFreeBird = SoundTool.GetAudioClip(Location, "FreeBird_Start.wav");
            End1FreeBird = SoundTool.GetAudioClip(Location, "FreeBird_End01.wav");
            End2FreeBird = SoundTool.GetAudioClip(Location, "FreeBird_End02.wav");
            StartChipi = SoundTool.GetAudioClip(Location, "ChipiChipi_Start.wav");
            EndChipi = SoundTool.GetAudioClip(Location, "ChipiChipi_End.wav");
            StartINeedAHero = SoundTool.GetAudioClip(Location, "INeedAHero_Start.wav");
            EndINeedAHero = SoundTool.GetAudioClip(Location, "INeedAHero_End.wav");
            StartMyWay = SoundTool.GetAudioClip(Location, "MyWay_Start.wav");
            EndMyWay = SoundTool.GetAudioClip(Location, "MyWay_End.wav");
            // StartNyanCat = SoundTool.GetAudioClip(Location, "NyanCat_Start.wav");
            // EndNyanCat = SoundTool.GetAudioClip(Location, "NyanCat_End.wav");
            StartWeAreTheRats = SoundTool.GetAudioClip(Location, "Rats_Start.wav");
            EndWeAreTheRats = SoundTool.GetAudioClip(Location, "Rats_End.wav");
            StartToothless = SoundTool.GetAudioClip(Location, "Toothless_Start.wav");
            EndToothless = SoundTool.GetAudioClip(Location, "Toothless_End.wav");
            mls.LogInfo("Loaded all songs");

            _ = Config.Bind("Mod", "EnableMod", true, "Enables the Mod");
            Toggle = Config.Bind("Sound", "EnableJukeboxJester", true, "Enables the Jukebox Jester");
            Volume = Config.Bind("Sound", "VolumeControl", 500, new ConfigDescription("Changes the Volume of the Jukebox Jester", (AcceptableValueBase)(object)new AcceptableValueRange<int>(0,1000), Array.Empty<object>()));

            harmony.PatchAll(typeof(JesterAiPatch));
            harmony.PatchAll(typeof(JukeboxJesterBase));
            mls.LogInfo("Jukebox Jester initialized");
        }
    }
}
