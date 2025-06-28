namespace F1GameDataParser.Models.Lap
{
    public class Lap : MergeableBase<Lap>
    {
        public Header Header { get; set; }

        public LapDetails[] LapDetails{ get; set; } = new LapDetails[Sizes.MaxPlayers];

        public byte TimeTrialPBCarIdx { get; set; } // Index of Personal Best car in time trial (255 if invalid)
        public byte TimeTrialRivalCarIdx { get; set; } // Index of Rival car in time trial (255 if invalid)
    }
}
