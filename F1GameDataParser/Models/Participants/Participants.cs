namespace F1GameDataParser.Models.Participants
{
    public class Participants : MergeableBase<Participants>
    {
        public Header Header { get; set; }
        public byte NumActiveCars { get; set; }
        public ParticipantDetails[] ParticipantList { get; set; } = new ParticipantDetails[Sizes.MaxPlayers];
    }
}
