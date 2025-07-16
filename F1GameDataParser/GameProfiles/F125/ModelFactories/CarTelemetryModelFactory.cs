using F1GameDataParser.GameProfiles.F125.Packets.CarTelemetry;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.CarTelemetry;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F125.ModelFactories
{
    public class CarTelemetryModelFactory : ModelFactoryBase<CarTelemetryPacket, CarTelemetry>
    {
        public override Expression<Func<CarTelemetryPacket, CarTelemetry>> ToModelExpression()
        {
            return packet => new CarTelemetry
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                CarTelemetryDetails = packet.carTelemetryDetails.Select(car => new CarTelemetryDetails 
                {
                    Speed = car.speed,
                    Throttle = car.throttle,
                    Steer = car.steer,
                    Clutch = car.clutch,
                    Gear = car.gear,
                    EngineRPM = car.engineRPM,
                    DRS = car.drs,
                    RevLightsPercent = car.revLightsPercent,
                    RevLightsBitValue = car.revLightsBitValue,
                    BrakesTemperature = car.brakesTemperature, // will it work, array to ienumerable ?
                    TyresSurfaceTemperature = car.tyresSurfaceTemperature, // will it work, array to ienumerable ?
                    TyresInnerTemperature = car.tyresInnerTemperature, // will it work, array to ienumerable ?
                    EngineTemperature = car.engineTemperature,
                    TyresPressure = car.tyresPressure, // will it work, array to ienumerable ?
                    SurfaceType = car.surfaceType // will it work, array to ienumerable ?
                }).ToArray(),
                MFDPanelIndex = packet.mfdPanelIndex,
                MFDPanelIndexSecondaryPlayer = packet.mfdPanelIndexSecondaryPlayer,
                SuggestedGear = packet.suggestedGear
            };
        }
    }
}
