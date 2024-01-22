using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;
using System;


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
            
            switch (randomNumber)
            {
                case 1: //FreeBird
                case 2:
                    ___popUpTimer = 62.5f;
                    mls.LogInfo($"JukeInTheBox will load with FreeBird and a popUpTimer of {___popUpTimer}");
                    break;

                case 3: // ChipChipiChapaChapa
                    ___popUpTimer = 40.03f;
                    mls.LogInfo($"JukeInTheBox will load with ChipChipiChapaChapa and a popUpTimer of {___popUpTimer}");
                    break;

                case 4: // I need a hero
                    ___popUpTimer = 31.58f;
                    mls.LogInfo($"JukeInTheBox will load with I need a hero and a popUpTimer of {___popUpTimer}");
                    break;

                case 5: // My Way
                    ___popUpTimer = 50.47f;
                    mls.LogInfo($"JukeInTheBox will load with My Way and a popUpTimer of {___popUpTimer}");
                    break;

                case 6: // We are the rats
                    ___popUpTimer = 37.13f;
                    mls.LogInfo($"JukeInTheBox will load with We are the rats and a popUpTimer of {___popUpTimer}");
                    break;

                case 7: // Toothless
                    ___popUpTimer = 25.58f;
                    mls.LogInfo($"JukeInTheBox will load with Toothless and a popUpTimer of {___popUpTimer}");
                    break;

                case 8: // Girls Club
                    ___popUpTimer = 35.26f;
                    mls.LogInfo($"JukeInTheBox will load with Girls Club and a popUpTimer of {___popUpTimer}");
                    break;

                case 9: // We are number one
                case 10:
                    ___popUpTimer = 26.95f;
                    mls.LogInfo($"JukeInTheBox will load with We are Number one and a popUpTimer of {___popUpTimer}");
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
            System.Random random = new System.Random(Environment.TickCount);
            randomNumber = random.Next(1, 11);
            rickyChance = random.Next(1, 101);
            ___popUpSFX = null;
            ___farAudio.volume = (float)JukeboxJesterBase.Instance.Volume.Value / 10f;
            ___creatureVoice.volume = (float)JukeboxJesterBase.Instance.Volume.Value / 10f;

            switch(randomNumber)
            {
                case 1: //FreeBird Version 1
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["FreeBird_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["FreeBird_End01"];
                    break;

                case 2: //FreeBird Version 2
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["FreeBird_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["FreeBird_End02"];
                    break;

                case 3: // ChipChipiChapaChapa
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["ChipiChipi_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["ChipiChipi_End"];
                    break;

                case 4: // I need a hero
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["INeedAHero_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["INeedAHero_End"];
                    break;

                case 5: // My Way
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["MyWay_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["MyWay_End"];
                    break;

                case 6: // We are the rats
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["Rats_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["Rats_End"];
                    break;

                case 7: // Toothless
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["Toothless_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["Toothless_End"];
                    break;

                case 8: // Girls Club
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["GirlsClub_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["GirlsClub_End"];
                    break;

                case 9: // We are number one Version 1
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["WeAreNumberOne_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["WeAreNumberOne_End01"];
                    break;

                case 10: // We are number one Version 2
                    ___popGoesTheWeaselTheme = JukeboxJesterBase.Instance.AudioFiles["WeAreNumberOne_Start"];
                    ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["WeAreNumberOne_End02"];
                    break;
            }

            if (rickyChance <= 1)
            {
                ___screamingSFX = JukeboxJesterBase.Instance.AudioFiles["Ricky"];
            }
        }
    }
}
