using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HarmonyLib;
using Steamworks;
using Verse;

namespace AddChangenote;

[HarmonyPatch(typeof(SteamUGC), nameof(SteamUGC.SubmitItemUpdate))]
internal class SteamUGC_SubmitItemUpdate
{
    private static void Prefix(ref string pchChangeNote)
    {
        if (!pchChangeNote.StartsWith("[Auto-generated text]") || AddChangenote.CurrentMod == null)
        {
            return;
        }

        var modName = AddChangenote.CurrentMod.GetWorkshopName();
        var modDirectory = AddChangenote.CurrentMod.GetWorkshopUploadDirectory();
        var changelogFile = new FileInfo($"{modDirectory.FullName}\\About\\Changelog.txt");
        if (!changelogFile.Exists)
        {
            Log.Message(
                $"Could not find changelog-file for mod {modName} at path {changelogFile.FullName}, skipping modification of changenote.");
            return;
        }

        var manifestFile = new FileInfo($"{modDirectory.FullName}\\About\\Manifest.xml");
        string currentVersion = null;
        if (manifestFile.Exists)
        {
            foreach (var line in File.ReadAllLines(manifestFile.FullName))
            {
                if (!line.Contains("<version>"))
                {
                    continue;
                }

                currentVersion = line.Replace("<version>", "|").Split('|')[1].Split('<')[0];
            }

            if (string.IsNullOrEmpty(currentVersion))
            {
                Log.Message(
                    $"Could not find version in manifest-file for mod {modName}, checking mod-version.");
            }
        }

        if (currentVersion == null)
        {
            var updatingMod =
                ModLister.AllInstalledMods.FirstOrDefault(data =>
                    data.Name == AddChangenote.CurrentMod.GetWorkshopName());
            if (updatingMod != null)
            {
                currentVersion = updatingMod.ModVersion;
            }
        }

        if (currentVersion == null)
        {
            Log.Message(
                $"Could not find version for mod {modName} by ModVersion or Manifest-file, skipping modification of changenote.");
            return;
        }

        Log.Message(currentVersion);
        var isExtracting = false;
        var changelogArray = new List<string>();
        var versionRegex = new Regex(@"^\d+(?:\.\d+){1,3}");
        foreach (var line in File.ReadAllLines(changelogFile.FullName))
        {
            if (line.StartsWith("#"))
            {
                continue;
            }

            if (line.StartsWith(currentVersion))
            {
                isExtracting = true;
                changelogArray.Add(line);
                continue;
            }

            var match = versionRegex.Match(line);
            if (!isExtracting)
            {
                continue;
            }

            if (match.Success)
            {
                break;
            }

            changelogArray.Add(line);
        }

        var changelogMessage = string.Join(Environment.NewLine, changelogArray).Trim();

        if (string.IsNullOrEmpty(changelogMessage))
        {
            Log.Message(
                $"Could not find latest changenote in changelog-file for mod {modName}, skipping modification of changenote.");
            return;
        }

        pchChangeNote = changelogMessage;
    }
}