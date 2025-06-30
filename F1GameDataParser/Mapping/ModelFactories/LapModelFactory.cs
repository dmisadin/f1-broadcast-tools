using F1GameDataParser.Models.Lap;
using F1GameDataParser.Packets.Lap;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.ModelFactories
{
    public class LapModelFactory : ModelFactoryBase<LapPacket, IEnumerable<LapDetails>>
    {
        public override Expression<Func<LapPacket, IEnumerable<LapDetails>>> ToModelExpression()
        {
            return packet => packet.lapDetails.Select(lap => new LapDetails
            {
                LastLapTimeInMS = lap.lastLapTimeInMS,
                CurrentLapTimeInMS = lap.currentLapTimeInMS,
                Sector1TimeInMS = lap.sector1TimeInMS,
                Sector1TimeMinutes = lap.sector1TimeMinutes,
                Sector2TimeInMS = lap.sector2TimeInMS,
                Sector2TimeMinutes = lap.sector2TimeMinutes,

                DeltaToCarInFrontInMS = lap.deltaToCarInFrontInMS,
                DeltaToRaceLeaderInMS = lap.deltaToRaceLeaderInMS,

                LapDistance = lap.lapDistance,
                TotalDistance = lap.totalDistance,
                SafetyCarDelta = lap.safetyCarDelta,

                CarPosition = lap.carPosition,
                CurrentLapNum = lap.currentLapNum,
                PitStatus = lap.pitStatus,
                NumPitStops = lap.numPitStops,
                Sector = lap.sector,
                CurrentLapInvalid = lap.currentLapInvalid,
                Penalties = lap.penalties,
                TotalWarnings = lap.totalWarnings,
                CornerCuttingWarnings = lap.cornerCuttingWarnings,
                NumUnservedDriveThroughPens = lap.numUnservedDriveThroughPens,
                NumUnservedStopGoPens = lap.numUnservedStopGoPens,
                GridPosition = lap.gridPosition,
                DriverStatus = lap.driverStatus,
                ResultStatus = lap.resultStatus,
                PitLaneTimeInLaneInMS = lap.pitLaneTimeInLaneInMS,
                PitLaneTimerActive = lap.pitLaneTimerActive,
                PitStopTimerInMS = lap.pitStopTimerInMS,
                PitStopShouldServePen = lap.pitStopShouldServePen,
            }).ToList();
        }
    }
}
