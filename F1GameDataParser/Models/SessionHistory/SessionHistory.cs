namespace F1GameDataParser.Models.SessionHistory
{
    public class SessionHistory
    {
            public Header Header { get; set; }

            public byte CarIdx { get; set; }
            public byte NumTyreStints { get; set; }
            public byte BestLapTimeLapNum { get; set; }
            public byte BestSector1LapNum { get; set; }
            public byte BestSector2LapNum { get; set; }
            public byte BestSector3LapNum { get; set; }

            public IEnumerable<LapHistoryDetails> LapHistoryDetails { get; set; }
            public IEnumerable<TyreStintHistoryDetails> TyreStintHistoryDetails { get; set; }
    }
}
