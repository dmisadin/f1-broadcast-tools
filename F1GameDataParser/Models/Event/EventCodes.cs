namespace F1GameDataParser.Models.Event
{
    public static class EventCodes
    {
        public const string SessionStarted = "SSTA";
        public const string SessionEnded = "SEND";
        public const string FastestLap = "FTLP";
        public const string Retirement = "RTMT";
        public const string DrsEnabled = "DRSE";
        public const string DrsDisabled = "DRSD";
        public const string TeamMateInPits = "TMPT";
        public const string ChequeredFlag = "CHQF";
        public const string RaceWinner = "RCWN";
        public const string PenaltyIssued = "PENA";
        public const string SpeedTrapTriggered = "SPTP";
        public const string StartLights = "STLG";
        public const string LightsOut = "LGOT";
        public const string DriveThroughServed = "DTSV";
        public const string StopGoServed = "SGSV";
        public const string Flashback = "FLBK";
        public const string ButtonStatus = "BUTN";
        public const string Overtake = "OVTK";

        public static readonly List<string> EnabledEvents = new List<string>
        {
            FastestLap,
            Retirement,
            DrsEnabled,
            DrsDisabled,
            ChequeredFlag,
            RaceWinner,
            PenaltyIssued,
            StartLights,
            LightsOut,
            DriveThroughServed,
            StopGoServed,
            Overtake
        };
    }
}
