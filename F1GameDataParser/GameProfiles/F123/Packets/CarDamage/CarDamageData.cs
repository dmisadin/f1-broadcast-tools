using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.CarDamage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarDamageData
    {
        // Everything is percentage except drsFault, ErsFault, EngineBlown, EngineSeized; 0 = OK, 1 = Fault
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyresWear;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyresDamage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] brakesDamage;

        public byte frontLeftWingDamage;
        public byte frontRightWingDamage;
        public byte rearWingDamage;
        public byte floorDamage;
        public byte diffuserDamage;
        public byte sidepodDamage;
        public byte drsFault;
        public byte ersFault;
        public byte gearBoxDamage;
        public byte engineDamage;
        public byte engineMGUHWear;
        public byte engineESWear;
        public byte engineCEWear;
        public byte engineICEWear;
        public byte engineMGUKWear;
        public byte engineTCWear;
        public byte engineBlown;
        public byte engineSeized;
    }
}

/*
 * Restricted data (default value = 0):
 * 
 * tyresWear (All four wheels)
 * tyresDamage (All four wheels)
 * brakesDamage (All four wheels)
 * 
 * frontLeftWingDamage
 * frontRightWingDamage
 * rearWingDamage
 * floorDamage
 * diffuserDamage
 * sidepodDamage
 * engineDamage
 * gearBoxDamage
 * drsFault
 * engineMGUHWear
 * engineESWear
 * engineCEWear
 * engineICEWear
 * engineMGUKWear
 * engineTCWear
 */