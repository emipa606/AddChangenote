using HarmonyLib;
using Verse;
using Verse.Steam;

namespace AddChangenote
{
    [StaticConstructorOnStartup]
    public class AddChangenote
    {
        static AddChangenote()
        {
            new Harmony("Mlie.AddChangenote").PatchAll();
        }

        public static WorkshopUploadable currentMod { get; set; }
    }
}