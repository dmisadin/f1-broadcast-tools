namespace F1GameDataParser.Models.Participants
{
    public class Participants
    {
        public Header Header { get; set; }
        public byte NumActiveCars { get; set; }
        public ParticipantDetails[] ParticipantList { get; set; } = new ParticipantDetails[Sizes.MaxPlayers];
    }
}
