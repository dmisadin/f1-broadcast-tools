using F1GameDataParser.Enums;
using F1GameDataParser.Enums.Event;

namespace F1GameDataParser.Models.Event
{
    public class FastestLap : IEvent
    {
        public byte VehicleIdx { get; set; }
        public float LapTime { get; set; }
    }

    public class Retirement : IEvent
    {
        public byte VehicleIdx { get; set; }
        public ResultReason? Reason { get; set; } // Added in F1 25
    }

    public class TeamMateInPits : IEvent
    {
        public byte VehicleIdx { get; set; }
    }

    public class RaceWinner : IEvent
    {
        public byte VehicleIdx { get; set; }
    }

    public class Penalty : IEvent
    {
        public PenaltyType PenaltyType { get; set; }
        public InfringementType InfringementType { get; set; }
        public byte VehicleIdx { get; set; }
        public byte OtherVehicleIdx { get; set; }
        public byte Time { get; set; }
        public byte LapNum { get; set; }
        public byte PlacesGained { get; set; }
    }

    public class SpeedTrap : IEvent
    {
        public byte VehicleIdx { get; set; }
        public float Speed { get; set; }
        public byte IsOverallFastestInSession { get; set; } // 0 = false, 1 = true
        public byte IsDriverFastestInSession { get; set; } // 0 = false, 1 = true
        public byte FastestVehicleIdxInSession { get; set; }
        public float FastestSpeedInSession { get; set; }
    }

    public class StartLights : IEvent
    {
        public byte NumLights { get; set; }
    }

    public class DriveThroughPenaltyServed : IEvent
    {
        public byte VehicleIdx { get; set; }
    }

    public class StopGoPenaltyServed : IEvent
    {
        public byte VehicleIdx { get; set; }
    }

    public class Flashback : IEvent
    {
        public uint FlashbackFrameIdentifier { get; set; }
        public float FlashbackSessionTime { get; set; }
    }

    public class Buttons : IEvent
    {
        public Button ButtonStatus { get; set; }
    }

    public class Overtake : IEvent
    {
        public byte OvertakingVehicleIdx { get; set; }
        public byte BeingOvertakenVehicleIdx { get; set; }
    }

    public class DrsDisabled : IEvent // Added in F1 25
    {
        public DRSDisabledReason DRSDisabledReason { get; set; }
    }

    public class SafetyCar : IEvent
    {
        public SafetyCarType SafetyCarType { get; set; }
        public SafetyCarEventType EventType { get; set; }
    }

    public class EventDetails<TEvent> where TEvent : IEvent
    {
        public TEvent? Details { get; set; }
    }

    public interface IEvent
    {

    }
}
