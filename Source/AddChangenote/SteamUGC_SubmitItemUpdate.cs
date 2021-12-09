using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using HarmonyLib;
using Steamworks;
using Verse;

namespace AddChangenote;

[HarmonyPatch(typeof(SteamUGC), "SubmitItemUpdate")]
internal class SteamUGC_SubmitItemUpdate
{
    private static void Prefix(ref string pchChangeNote)
    {
        if (!pchChangeNote.StartsWith("[Auto-generated text]") || AddChangenote.currentMod == null)
        {
            return;
        }

        var modName = AddChangenote.currentMod.GetWorkshopName();
        var modDirectory = AddChangenote.currentMod.GetWorkshopUploadDirectory();
        var changelogFile = new FileInfo(modDirectory.FullName + "\\About\\Changelog.txt");
        if (!changelogFile.Exists)
        {
            Log.Message("Could not find changelog-file for mod " + modName + " at path " + changelogFile.FullName +
                        ", skipping modification of changenote.");
            return;
        }

        var manifestFile = new FileInfo(modDirectory.FullName + "\\About\\Manifest.xml");
        if (!manifestFile.Exists)
        {
            Log.Message("Could not find manifest-file for mod " + modName + " at path " + manifestFile.FullName +
                        ", skipping modification of changenote.");
            return;
        }

        string currentVersion = null;
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
            Log.Message("Could not find version in manifest-file for mod " + modName +
                        ", skipping modification of changenote.");
            return;
        }

        Log.Message(currentVersion);
        var isExtracting = false;
        var changelogArray = new List<string>();
        var versionRegex = new Regex(@"^\d+(?:\.\d+){1,3}");
        foreach (var line in File.ReadAllLines(changelogFile.FullName))
        {
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
            Log.Message("Could not find latest changenote in changelog-file for mod " + modName +
                        ", skipping modification of changenote.");
            return;
        }

        pchChangeNote = changelogMessage;
    }
}