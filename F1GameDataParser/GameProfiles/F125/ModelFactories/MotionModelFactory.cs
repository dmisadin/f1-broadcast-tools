using F1GameDataParser.GameProfiles.F125.Packets.Motion;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Motion;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F125.ModelFactories
{
    public class MotionModelFactory : ModelFactoryBase<MotionPacket, IEnumerable<CarMotionDetails>>
    {
        public override Expression<Func<MotionPacket, IEnumerable<CarMotionDetails>>> ToModelExpression()
        {
            return packet => packet.carMotionDetails.Select(car => new CarMotionDetails
            {
                WorldPositionX = car.worldPositionX,
                WorldPositionY = car.worldPositionY,
                WorldPositionZ = car.worldPositionZ,
                WorldVelocityX = car.worldVelocityX,
                WorldVelocityY = car.worldVelocityY,
                WorldVelocityZ = car.worldVelocityZ,
                WorldForwardDirX = car.worldForwardDirX,
                WorldForwardDirY = car.worldForwardDirY,
                WorldForwardDirZ = car.worldForwardDirZ,
                WorldRightDirX = car.worldForwardDirX,
                WorldRightDirY = car.worldForwardDirY,
                WorldRightDirZ = car.worldForwardDirZ,
                GForceLateral = car.gForceLateral,
                GForceLongitudinal = car.gForceLongitudinal,
                GForceVertical = car.gForceVertical,
                Yaw = car.yaw,
                Pitch = car.pitch,
                Roll = car.roll
            }).ToList();
        }
    }
}
