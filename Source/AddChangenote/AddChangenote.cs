using System.Reflection;
using HarmonyLib;
using Verse;
using Verse.Steam;

namespace AddChangenote;

[StaticConstructorOnStartup]
public class AddChangenote
{
    static AddChangenote()
    {
        new Harmony("Mlie.AddChangenote").PatchAll(Assembly.GetExecutingAssembly());
    }

    public static WorkshopUploadable CurrentMod { get; set; }
}