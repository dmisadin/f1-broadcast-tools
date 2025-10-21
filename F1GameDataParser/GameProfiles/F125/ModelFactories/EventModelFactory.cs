using F1GameDataParser.GameProfiles.F125.Packets.Event;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Event;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F125.ModelFactories
{
    public class EventModelFactory : ModelFactoryBase<EventPacket, Event>
    {
        public override Expression<Func<EventPacket, Event>> ToModelExpression()
        {
            return packet => new Event
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                EventStringCode = NullTerminatedASCIIToString(packet.eventStringCode), // is this optimized ?
                EventDetails = EventDetailsExpression(NullTerminatedASCIIToString(packet.eventStringCode)).Compile().Invoke(packet.eventDetails),
            };
        }

        public Expression<Func<EventData, EventDetails<IEvent>>> EventDetailsExpression(string eventCode)
        {
            return packet => new EventDetails<IEvent>
            {
                Details =
                    eventCode == EventCodes.FastestLap
                        ? new Models.Event.FastestLap
                        {
                            VehicleIdx = packet.fastestLap.vehicleIdx,
                            LapTime = packet.fastestLap.lapTime
                        }
                    : eventCode == EventCodes.Retirement
                        ? new Models.Event.Retirement
                        {
                            VehicleIdx = packet.retirement.vehicleIdx,
                            Reason = packet.retirement.reason,
                        }
                    : eventCode == EventCodes.TeamMateInPits
                        ? new Models.Event.TeamMateInPits
                        {
                            VehicleIdx = packet.teamMateInPits.vehicleIdx
                        }
                    : eventCode == EventCodes.RaceWinner
                        ? new Models.Event.RaceWinner
                        {
                            VehicleIdx = packet.raceWinner.vehicleIdx
                        }
                    : eventCode == EventCodes.PenaltyIssued
                        ? new Models.Event.Penalty
                        {
                            PenaltyType = packet.penalty.penaltyType,
                            InfringementType = packet.penalty.infringementType,
                            VehicleIdx = packet.penalty.vehicleIdx,
                            OtherVehicleIdx = packet.penalty.otherVehicleIdx,
                            Time = packet.penalty.time,
                            LapNum = packet.penalty.lapNum,
                            PlacesGained = packet.penalty.placesGained
                        }
                    : eventCode == EventCodes.SpeedTrapTriggered
                        ? new Models.Event.SpeedTrap
                        {
                            VehicleIdx = packet.speedTrap.vehicleIdx,
                            Speed = packet.speedTrap.speed,
                            IsOverallFastestInSession = packet.speedTrap.isOverallFastestInSession,
                            IsDriverFastestInSession = packet.speedTrap.isDriverFastestInSession,
                            FastestVehicleIdxInSession = packet.speedTrap.fastestVehicleIdxInSession,
                            FastestSpeedInSession = packet.speedTrap.fastestSpeedInSession
                        }
                    : eventCode == EventCodes.StartLights
                        ? new Models.Event.StartLights
                        {
                            NumLights = packet.startLights.numLights
                        }
                    : eventCode == EventCodes.DriveThroughServed
                        ? new Models.Event.DriveThroughPenaltyServed
                        {
                            VehicleIdx = packet.driveThroughPenaltyServed.vehicleIdx
                        }
                    : eventCode == EventCodes.StopGoServed
                        ? new Models.Event.StopGoPenaltyServed
                        {
                            VehicleIdx = packet.stopGoPenaltyServed.vehicleIdx
                        }
                    : eventCode == EventCodes.Flashback
                        ? new Models.Event.Flashback
                        {
                            FlashbackFrameIdentifier = packet.flashback.flashbackFrameIdentifier,
                            FlashbackSessionTime = packet.flashback.flashbackSessionTime
                        }
                    : eventCode == EventCodes.ButtonStatus
                        ? new Models.Event.Buttons
                        {
                            ButtonStatus = packet.buttons.buttonStatus
                        }
                    : eventCode == EventCodes.Overtake
                        ? new Models.Event.Overtake
                        {
                            OvertakingVehicleIdx = packet.overtake.overtakingVehicleIdx,
                            BeingOvertakenVehicleIdx = packet.overtake.beingOvertakenVehicleIdx
                        }
                    : eventCode == EventCodes.DrsDisabled
                        ? new Models.Event.DrsDisabled
                        {
                            DRSDisabledReason = packet.drsDisabled.reason
                        }
                    : null
            };
        }
    }
}
