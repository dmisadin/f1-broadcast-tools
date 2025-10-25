using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum InfringementType : byte
    {
        [Label("Blocking by slow driving")]
        BlockingBySlowDriving = 0,

        [Label("Blocking by wrong-way driving")]
        BlockingByWrongWayDriving = 1,

        [Label("Reversing off the start line")]
        ReversingOffTheStartLine = 2,

        [Label("Big collision")]
        BigCollision = 3,

        [Label("Small collision")]
        SmallCollision = 4,

        [Label("Collision and failed to hand back a position")]
        CollisionFailedToHandBackPositionSingle = 5,

        [Label("Collision and failed to hand back multiple positions")]
        CollisionFailedToHandBackPositionMultiple = 6,

        [Label("Corner cutting gained time")]
        CornerCuttingGainedTime = 7,

        [Label("Corner cutting and an overtake")]
        CornerCuttingOvertakeSingle = 8,

        [Label("Corner cutting and multiple overtakes")]
        CornerCuttingOvertakeMultiple = 9,

        [Label("Crossed pit exit lane")]
        CrossedPitExitLane = 10,

        [Label("Ignoring blue flags")]
        IgnoringBlueFlags = 11,

        [Label("Ignoring yellow flags")]
        IgnoringYellowFlags = 12,

        [Label("Ignoring drive-through penalty")]
        IgnoringDriveThrough = 13,

        [Label("Too many drive-throughs")]
        TooManyDriveThroughs = 14,

        [Label("Drive-through reminder: serve within N laps")]
        DriveThroughReminderServeWithinNLaps = 15,

        [Label("Drive-through reminder: serve this lap")]
        DriveThroughReminderServeThisLap = 16,

        [Label("Pit lane speeding")]
        PitLaneSpeeding = 17,

        [Label("Parked for too long")]
        ParkedForTooLong = 18,

        [Label("Ignoring tyre regulations")]
        IgnoringTyreRegulations = 19,

        [Label("Too many penalties")]
        TooManyPenalties = 20,

        [Label("Multiple warnings")]
        MultipleWarnings = 21,

        [Label("Approaching disqualification")]
        ApproachingDisqualification = 22,

        [Label("Tyre regulations: select single")]
        TyreRegulationsSelectSingle = 23,

        [Label("Tyre regulations: select multiple")]
        TyreRegulationsSelectMultiple = 24,

        [Label("Lap invalidated for corner cutting")]
        LapInvalidatedCornerCutting = 25,

        [Label("Lap invalidated for running wide")]
        LapInvalidatedRunningWide = 26,

        [Label("Corner cutting and gained minor time")]
        CornerCuttingRanWideGainedTimeMinor = 27,

        [Label("Corner cutting and gained significant time")]
        CornerCuttingRanWideGainedTimeSignificant = 28,

        [Label("Corner cutting and gained extreme time")]
        CornerCuttingRanWideGainedTimeExtreme = 29,

        [Label("Lap invalidated for wall riding")]
        LapInvalidatedWallRiding = 30,

        [Label("Lap invalidated for using flashback")]
        LapInvalidatedFlashbackUsed = 31,

        [Label("Lap invalidated for reseting to track")]
        LapInvalidatedResetToTrack = 32,

        [Label("Blocking the pit lane")]
        BlockingThePitlane = 33,

        [Label("Jump start")]
        JumpStart = 34,

        [Label("Collision with the safety car")]
        SafetyCarToCarCollision = 35,

        [Label("Illegal safety car overtake")]
        SafetyCarIllegalOvertake = 36,

        [Label("Exceeding allowed pace under the safety car")]
        SafetyCarExceedingAllowedPace = 37,

        [Label("Exceeding allowed pace under the virtual safety car")]
        VirtualSafetyCarExceedingAllowedPace = 38,

        [Label("Driving below allowed speed in formation lap")]
        FormationLapBelowAllowedSpeed = 39,

        [Label("Formation lap parking")]
        FormationLapParking = 40,

        [Label("Retired: mechanical failure")]
        RetiredMechanicalFailure = 41,

        [Label("Retired: terminal damage")]
        RetiredTerminallyDamaged = 42,

        [Label("Safety car falling too far back")]
        SafetyCarFallingTooFarBack = 43,

        [Label("Black flag timer")]
        BlackFlagTimer = 44,

        [Label("Unserved stop-go penalty")]
        UnservedStopGoPenalty = 45,

        [Label("Unserved drive-through penalty")]
        UnservedDriveThroughPenalty = 46,

        [Label("Engine component change")]
        EngineComponentChange = 47,

        [Label("Gearbox change")]
        GearboxChange = 48,

        [Label("Parc fermé change")]
        ParcFermeChange = 49,

        [Label("League grid penalty")]
        LeagueGridPenalty = 50,

        [Label("Retry penalty")]
        RetryPenalty = 51,

        [Label("Illegal time gain")]
        IllegalTimeGain = 52,

        [Label("Mandatory pit stop")]
        MandatoryPitstop = 53,

        [Label("Attribute assigned")]
        AttributeAssigned = 54
    }
}
