namespace F1GameDataParser.Models.CarDamage
{
    public class CarDamageDetails
    {
        // Everything is percentage except drsFault, ErsFault, EngineBlown, EngineSeized; 0 = OK, 1 = Fault

        public float[] TyresWear { get; set; } = new float[4];
        public byte[] TyresDamage { get; set; } = new byte[4];
        public byte[] BrakesDamage { get; set; } = new byte[4];

        public byte FrontLeftWingDamage { get; set; }
        public byte FrontRightWingDamage { get; set; }
        public byte RearWingDamage { get; set; }
        public byte FloorDamage { get; set; }
        public byte DiffuserDamage { get; set; }
        public byte SidepodDamage { get; set; }
        public byte DrsFault { get; set; }
        public byte ErsFault { get; set; }
        public byte GearBoxDamage { get; set; }
        public byte EngineDamage { get; set; }
        public byte EngineMGUHWear { get; set; }
        public byte EngineESWear { get; set; }
        public byte EngineCEWear { get; set; }
        public byte EngineICEWear { get; set; }
        public byte EngineMGUKWear { get; set; }
        public byte EngineTCWear { get; set; }
        public byte EngineBlown { get; set; }
        public byte EngineSeized { get; set; }
    }
}
