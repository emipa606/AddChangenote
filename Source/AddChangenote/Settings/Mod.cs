using UnityEngine;
using Verse;

namespace ModName
{
    [StaticConstructorOnStartup]
    internal class ModNameMod : Mod
    {
        /// <summary>
        /// Cunstructor
        /// </summary>
        /// <param name="content"></param>
        public ModNameMod(ModContentPack content) : base(content)
        {
            instance = this;
        }

        /// <summary>
        /// The instance-settings for the mod
        /// </summary>
        internal ModNameSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = GetSettings<ModNameSettings>();
                }
                return settings;
            }
            set
            {
                settings = value;
            }
        }

        /// <summary>
        /// The title for the mod-settings
        /// </summary>
        /// <returns></returns>
        public override string SettingsCategory()
        {
            return "ModName";
        }

        /// <summary>
        /// The settings-window
        /// For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
        /// </summary>
        /// <param name="rect"></param>
        public override void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(rect);
            listing_Standard.Gap();
            listing_Standard.CheckboxLabeled("Checkbox label", ref Settings.CheckboxValue, "Checkbox tooltip");
            listing_Standard.Label("Int value: " + Settings.IntValue, -1, "Int tooltip");
            listing_Standard.IntAdjuster(ref Settings.IntValue, 1, 0);
            listing_Standard.IntRange(ref Settings.IntRangeValue, 0, 100);
            listing_Standard.Label("Float value: " + Settings.FloatValue, -1, "Float tooltip");
            Settings.FloatValue = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.FloatValue, 0, 100f, false, "Float label", null, null, 1);
            listing_Standard.End();
            Settings.Write();
        }

        /// <summary>
        /// The instance of the settings to be read by the mod
        /// </summary>
        public static ModNameMod instance;

        /// <summary>
        /// The private settings
        /// </summary>
        private ModNameSettings settings;

    }
}
