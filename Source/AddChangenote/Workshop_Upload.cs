using HarmonyLib;
using Verse.Steam;

namespace AddChangenote;

[HarmonyPatch(typeof(Workshop), "Upload")]
internal static class Workshop_Upload
{
    public static void Prefix(ref WorkshopUploadable item)
    {
        AddChangenote.CurrentMod = item;
    }
}