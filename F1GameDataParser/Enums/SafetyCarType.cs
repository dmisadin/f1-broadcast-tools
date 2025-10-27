using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum SafetyCarType : byte
    {
        None,
        [Label("Safety Car")]
        Full,
        [Label("Virtual Safety Car")]
        Virtual,
        [Label("Formation Lap")]
        FormationLap
    }
}
