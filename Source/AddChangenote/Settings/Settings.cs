using Verse;

namespace ModName
{
    /// <summary>
    /// Definition of the settings for the mod
    /// </summary>
    internal class ModNameSettings : ModSettings
    {
        public bool CheckboxValue = true;
        public int IntValue = 3;
        public IntRange IntRangeValue = new IntRange(10, 20);
        public float FloatValue = 5f;

        /// <summary>
        /// Saving and loading the values
        /// </summary>
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref CheckboxValue, "CheckboxValue", true, false);
            Scribe_Values.Look(ref IntValue, "IntValue", 3, false);
            Scribe_Values.Look(ref IntRangeValue, "IntRangeValue", new IntRange(10, 20), false);
            Scribe_Values.Look(ref FloatValue, "FloatValue", 5f, false);
        }
    }
}