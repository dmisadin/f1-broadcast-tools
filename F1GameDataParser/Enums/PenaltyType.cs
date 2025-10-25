using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum PenaltyType : byte
    {
        [Label("Drive-through penalty")]
        DriveThrough = 0,

        [Label("Stop-go penalty")]
        StopGo = 1,

        [Label("Grid penalty")]
        GridPenalty = 2,

        [Label("Penalty reminder")]
        PenaltyReminder = 3,

        [Label("Time penalty")]
        TimePenalty = 4,

        [Label("Warning")]
        Warning = 5,

        [Label("Disqualified")]
        Disqualified = 6,

        [Label("Removed from formation lap")]
        RemovedFromFormationLap = 7,

        [Label("Parked too long timer")]
        ParkedTooLongTimer = 8,

        [Label("Tyre regulations violation")]
        TyreRegulations = 9,

        [Label("This lap invalidated")]
        ThisLapInvalidated = 10,

        [Label("This and next lap invalidated")]
        ThisAndNextLapInvalidated = 11,

        [Label("This lap invalidated (no reason given)")]
        ThisLapInvalidatedWithoutReason = 12,

        [Label("This and next lap invalidated (no reason given)")]
        ThisAndNextLapInvalidatedWithoutReason = 13,

        [Label("This and previous lap invalidated")]
        ThisAndPreviousLapInvalidated = 14,

        [Label("This and previous lap invalidated (no reason given)")]
        ThisAndPreviousLapInvalidatedWithoutReason = 15,

        [Label("Retired")]
        Retired = 16,

        [Label("Black flag timer")]
        BlackFlagTimer = 17
    }

}
