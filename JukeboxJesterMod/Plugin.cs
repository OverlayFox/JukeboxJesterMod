using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JukeboxJesterMod.Patches;
using LCSoundTool;
using System;
using System.IO;
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
        public AudioClip StartFreeBird { get; private set; }
        public AudioClip End1FreeBird { get; private set; }
        public AudioClip End2FreeBird { get; private set; }
        public AudioClip StartChipi { get; private set; }
        public AudioClip EndChipi { get; private set; }
        public AudioClip StartINeedAHero { get; private set; }
        public AudioClip EndINeedAHero { get; private set; }
        public AudioClip StartMyWay { get; private set; }
        public AudioClip EndMyWay { get; private set; }
        public AudioClip StartNyanCat { get; private set; }
        public AudioClip EndNyanCat { get; private set; }
        public AudioClip StartWeAreTheRats { get; private set; }
        public AudioClip EndWeAreTheRats { get; private set; }
        public AudioClip StartToothless { get; private set; }
        public AudioClip EndToothless { get; private set; }
        public AudioClip StartGirlsClub { get; private set; }
        public AudioClip EndGirlsClub { get; private set; }
        public AudioClip StartJojo { get; private set; }
        public AudioClip EndJojo { get; private set; }
        public AudioClip EndRicky { get; private set; }
        public AudioClip StartWano { get; private set; }
        public AudioClip End1Wano { get; private set; }
        public AudioClip End2Wano { get; private set; }



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
            StartFreeBird = SoundTool.GetAudioClip(Location, "FreeBird_Start.mp3");
            End1FreeBird = SoundTool.GetAudioClip(Location, "FreeBird_End01.mp3");
            End2FreeBird = SoundTool.GetAudioClip(Location, "FreeBird_End02.mp3");
            StartChipi = SoundTool.GetAudioClip(Location, "ChipiChipi_Start.mp3");
            EndChipi = SoundTool.GetAudioClip(Location, "ChipiChipi_End.mp3");
            StartINeedAHero = SoundTool.GetAudioClip(Location, "INeedAHero_Start.mp3");
            EndINeedAHero = SoundTool.GetAudioClip(Location, "INeedAHero_End.mp3");
            StartMyWay = SoundTool.GetAudioClip(Location, "MyWay_Start.mp3");
            EndMyWay = SoundTool.GetAudioClip(Location, "MyWay_End.mp3");
            StartNyanCat = SoundTool.GetAudioClip(Location, "NyanCat_Start.mp3");
            EndNyanCat = SoundTool.GetAudioClip(Location, "NyanCat_End.mp3");
            StartWeAreTheRats = SoundTool.GetAudioClip(Location, "Rats_Start.mp3");
            EndWeAreTheRats = SoundTool.GetAudioClip(Location, "Rats_End.mp3");
            StartToothless = SoundTool.GetAudioClip(Location, "Toothless_Start.mp3");
            EndToothless = SoundTool.GetAudioClip(Location, "Toothless_End.mp3");
            StartGirlsClub = SoundTool.GetAudioClip(Location, "GirlsClub_Start.mp3");
            EndGirlsClub = SoundTool.GetAudioClip(Location, "GirlsClub_End.mp3");
            StartJojo = SoundTool.GetAudioClip(Location, "Jojo_Start.mp3");
            EndJojo = SoundTool.GetAudioClip(Location, "Jojo_End.mp3");
            EndRicky = SoundTool.GetAudioClip(Location, "Ricky.mp3");
            StartWano = SoundTool.GetAudioClip(Location, "WeAreNumberOne_Start.mp3");
            End1Wano = SoundTool.GetAudioClip(Location, "WeAreNumberOne_End01.mp3");
            End2Wano = SoundTool.GetAudioClip(Location, "WeAreNumberOne_End02.mp3");
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
