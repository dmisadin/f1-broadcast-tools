using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.CarStatus
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusData
    {
        public AssistTractionControl tractionControl;
        public Assist antiLockBrakes;
        public FuelMix fuelMix;
        public byte frontBrakeBias; // Front brake bias (percentage)
        public byte pitLimiterStatus; // 0 = off, 1 = on
        public float fuelInTank; // Current fuel mass
        public float fuelCapacity;
        public float fuelRemainingLaps; // Fuel remaining in terms of laps (value on MFD)
        public ushort maxRPM;
        public ushort idleRPM;
        public byte maxGears;
        public byte drsAllowed; // 0 = not allowed, 1 = allowed
        public ushort drsActivationDistance; // 0 = DRS not available, else DRS available in [x] meters
        public TyreCompoundActual actualTyreCompound;
        public TyreCompoundVisual visualTyreCompound;
        public byte tyresAgeLaps;
        public FIAFlag vehicleFiaFlags;
        public float enginePowerICE; // Engine power output of ICE (W)
        public float enginePowerMGUK; // Engine power output of MGU-K (W)
        public float ersStoreEnergy; // ERS energy store in Joules
        public ERSDeployMode ersDeployMode;
        public float ersHarvestedThisLapMGUK;
        public float ersHarvestedThisLapMGUH;
        public float ersDeployedThisLap;
        public byte networkPaused; // Whether the car is paused in a network game
    }
}
