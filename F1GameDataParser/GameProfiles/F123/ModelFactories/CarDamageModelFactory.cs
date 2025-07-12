using F1GameDataParser.GameProfiles.F123.Packets.CarDamage;
using F1GameDataParser.Models.CarDamage;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F123.ModelFactories
{
    public class CarDamageModelFactory : ModelFactoryBase<CarDamagePacket, CarDamage>
    {
        public override Expression<Func<CarDamagePacket, CarDamage>> ToModelExpression()
        {
            return packet => new CarDamage
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                CarDamageDetails = packet.carDamageDetails.Select(damage => new CarDamageDetails
                {
                    TyresWear = damage.tyresWear,
                    TyresDamage = damage.tyresDamage,
                    BrakesDamage = damage.brakesDamage,
                    FrontLeftWingDamage = damage.frontLeftWingDamage,
                    FrontRightWingDamage = damage.frontRightWingDamage,
                    RearWingDamage = damage.rearWingDamage,
                    FloorDamage = damage.floorDamage,
                    DiffuserDamage = damage.diffuserDamage,
                    SidepodDamage = damage.sidepodDamage,
                    DrsFault = damage.drsFault,
                    ErsFault = damage.ersFault,
                    GearBoxDamage = damage.gearBoxDamage,
                    EngineDamage = damage.engineDamage,
                    EngineMGUHWear = damage.engineMGUHWear,
                    EngineESWear = damage.engineESWear,
                    EngineCEWear = damage.engineCEWear,
                    EngineICEWear = damage.engineICEWear,
                    EngineMGUKWear = damage.engineMGUKWear,
                    EngineTCWear = damage.engineTCWear,
                    EngineBlown = damage.engineBlown,
                    EngineSeized = damage.engineSeized
                }).ToArray()
            };
        }
    }
}
