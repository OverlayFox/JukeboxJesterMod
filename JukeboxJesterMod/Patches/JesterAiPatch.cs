﻿using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;


namespace JukeboxJesterMod.Patches
{
    [HarmonyPatch(typeof(JesterAI))]
    public static class JesterAiPatch
    {
        public static int randomNumber;
        public static int rickyChance;
        internal static ManualLogSource mls;

        [HarmonyPatch("SetJesterInitialValues")]
        [HarmonyPostfix]
        public static void SetSong(ref float ___popUpTimer)
        {
            mls = JukeboxJesterBase.Instance.mls;
            System.Random random = new System.Random();
            randomNumber = random.Next(1, 13);
            rickyChance = random.Next(1, 101);

            switch (randomNumber)
            {
                case 1: //FreeBird
                case 2:
                    ___popUpTimer = 62.5f;
                    mls.LogInfo("JukeInTheBox will load with FreeBird Version 1");
                    break;

                case 3: // ChipChipiChapaChapa
                    ___popUpTimer = 40.03f;
                    mls.LogInfo("JukeInTheBox will load with ChipChipiChapaChapa");
                    break;

                case 4: // I need a hero
                    ___popUpTimer = 31.58f;
                    mls.LogInfo("JukeInTheBox will load with I need a hero");
                    break;

                case 5: // My Way
                    ___popUpTimer = 50.47f;
                    mls.LogInfo("JukeInTheBox will load with My Way");
                    break;

                case 6: // We are the rats
                    ___popUpTimer = 37.13f;
                    mls.LogInfo("JukeInTheBox will load with We are the rats");
                    break;

                case 7: // Toothless
                    ___popUpTimer = 25.58f;
                    mls.LogInfo("JukeInTheBox will load with Toothless");
                    break;

                case 8: // Nyan Cat (Might be broken)
                    ___popUpTimer = 33.7f;
                    mls.LogInfo("JukeInTheBox will load with Nyan Cat");
                    break;

                case 9: // Girls Club
                    ___popUpTimer = 35.26f;
                    mls.LogInfo("JukeInTheBox will load with Girls Club");
                    break;

                case 10: // Jojo
                    ___popUpTimer = 38.74f;
                    mls.LogInfo("JukeInTheBox will load with Jojo");
                    break;

                case 11: // We are number one
                case 12:
                    ___popUpTimer = 26.95f;
                    mls.LogInfo("JukeInTheBox will load with We are Number one");
                    break;
            }
        }


        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void MusicPicker(ref AudioClip ___popGoesTheWeaselTheme, ref AudioClip ___screamingSFX, ref AudioSource ___creatureVoice, ref AudioSource ___popUpSFX, ref AudioSource ___farAudio)
        {
            if (!JukeboxJesterBase.Instance.Toggle.Value) 
            {
                return;
            }
            ___popUpSFX = null;
            ___farAudio.volume = (float)JukeboxJesterBase.Instance.Volume.Value / 100f;
            ___creatureVoice.volume = (float)JukeboxJesterBase.Instance.Volume.Value / 100f;

            switch(randomNumber)
            {
                case 1: //FreeBird Version 1
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartFreeBird;
                    ___screamingSFX = JukeboxJesterBase.Instance.End1FreeBird;
                    break;

                case 2: //FreeBird Version 2
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartFreeBird;
                    ___screamingSFX = JukeboxJesterBase.Instance.End2FreeBird;
                    break;

                case 3: // ChipChipiChapaChapa
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartChipi;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndChipi;
                    break;

                case 4: // I need a hero
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartINeedAHero;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndINeedAHero;
                    break;

                case 5: // My Way
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartMyWay;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndMyWay;
                    break;

                case 6: // We are the rats
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartWeAreTheRats;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndWeAreTheRats;
                    break;

                case 7: // Toothless
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartToothless;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndToothless;
                    break;

                case 8: // Nyan Cat (Might be broken)
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartNyanCat;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndNyanCat;
                    break;

                case 9: // Girls Club
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartGirlsClub;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndGirlsClub;
                    break;

                case 10: // Jojo
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartJojo;
                    ___screamingSFX = JukeboxJesterBase.Instance.EndJojo;
                    break;

                case 11: // We are number one Version 1
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartWano;
                    ___screamingSFX = JukeboxJesterBase.Instance.End1Wano;
                    break;

                case 12: // We are number one Version 2
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.StartWano;
                    ___screamingSFX = JukeboxJesterBase.Instance.End2Wano;
                    break;
            }

            if (rickyChance < 2)
            {
                ___screamingSFX = JukeboxJesterBase.Instance.EndRicky;
            }
        }
    }
}
