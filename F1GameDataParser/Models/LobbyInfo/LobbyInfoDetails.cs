using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.LobbyInfo
{
    public class LobbyInfoDetails
    {
        public bool AiControlled { get; set; } // Whether the vehicle is AI (1) or Human (0) controlled
        public Team TeamId { get; set; }
        public Nationality Nationality { get; set; }
        public Platform Platform { get; set; }
        public string Name { get; set; }
        public byte CarNumber { get; set; }
        public ReadyStatus ReadyStatus { get; set; }
    }
}
