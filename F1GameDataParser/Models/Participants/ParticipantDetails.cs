using F1GameDataParser.Database.Entities;
using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.Participants
{
    public class ParticipantDetails
    {
        public Toggle AiControlled { get; set; } // Whether the vehicle is AI (1) or Human (0) controlled
        public byte DriverId { get; set; } // Driver id - see appendix, 255 if network human
        public byte NetworkId { get; set; } // Network id – unique identifier for network players
        public Team TeamId { get; set; } // Team id - see appendix
        public Toggle MyTeam { get; set; } // My team flag – 1 = My Team, 0 = otherwise
        public byte RaceNumber { get; set; } // Race number of the car
        public Nationality Nationality { get; set; } // Nationality of the driver
        public string Name { get; set; } // Name of participant in UTF-8 format – null terminated. Will be truncated with … (U+2026) if too long
        public Toggle YourTelemetry { get; set; } // The player's UDP setting, 0 = restricted, 1 = public
        public Toggle ShowOnlineNames { get; set; } // The player's show online names setting, 0 = off, 1 = on
        public Platform Platform { get; set; } // 1 = Steam, 3 = PlayStation, 4 = Xbox, 6 = Origin, 255 = unknown
    }
}
