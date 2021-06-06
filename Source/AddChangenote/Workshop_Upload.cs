using HarmonyLib;
using Verse.Steam;

namespace AddChangenote
{
    [HarmonyPatch(typeof(Workshop), "Upload")]
    internal class Workshop_Upload
    {
        private static void Prefix(ref WorkshopUploadable item)
        {
            AddChangenote.currentMod = item;
        }
    }
}