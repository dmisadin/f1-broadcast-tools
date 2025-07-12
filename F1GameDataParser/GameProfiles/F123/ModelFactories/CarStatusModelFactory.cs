using F1GameDataParser.GameProfiles.F123.Packets.CarStatus;
using F1GameDataParser.Models.CarStatus;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F123.ModelFactories
{
    public class CarStatusModelFactory : ModelFactoryBase<CarStatusPacket, CarStatus>
    {
        public override Expression<Func<CarStatusPacket, CarStatus>> ToModelExpression()
        {
            return packet => new CarStatus
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                Details = packet.carStatusDetails.Select(detail => new CarStatusDetails
                    {
                        TractionControl = detail.tractionControl,
                        AntiLockBrakes = detail.antiLockBrakes,
                        FuelMix = detail.fuelMix,
                        FrontBrakeBias = detail.frontBrakeBias,
                        PitLimiterStatus = detail.pitLimiterStatus,
                        FuelInTank = detail.fuelInTank,
                        FuelCapacity = detail.fuelCapacity,
                        FuelRemainingLaps = detail.fuelRemainingLaps,
                        MaxRPM = detail.maxRPM,
                        IdleRPM = detail.idleRPM,
                        MaxGears = detail.maxGears,
                        DrsAllowed = detail.drsAllowed,
                        DrsActivationDistance = detail.drsActivationDistance,
                        ActualTyreCompound = detail.actualTyreCompound,
                        VisualTyreCompound = detail.visualTyreCompound,
                        TyresAgeLaps = detail.tyresAgeLaps,
                        VehicleFiaFlags = detail.vehicleFiaFlags,
                        EnginePowerICE = detail.enginePowerICE,
                        EnginePowerMGUK = detail.enginePowerMGUK,
                        ErsStoreEnergy = detail.ersStoreEnergy,
                        ErsDeployMode = detail.ersDeployMode,
                        ErsHarvestedThisLapMGUK = detail.ersHarvestedThisLapMGUK,
                        ErsHarvestedThisLapMGUH = detail.ersHarvestedThisLapMGUH,
                        ErsDeployedThisLap = detail.ersDeployedThisLap,
                        NetworkPaused = detail.networkPaused
                    }).ToArray()
            };
        }
    }
}
