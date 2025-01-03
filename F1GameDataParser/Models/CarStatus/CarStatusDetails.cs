using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.CarStatus
{
    public class CarStatusDetails
    {
        public AssistTractionControl TractionControl { get; set; }
        public Assist AntiLockBrakes { get; set; }
        public FuelMix FuelMix { get; set; }
        public byte FrontBrakeBias { get; set; } // Front brake bias (percentage)
        public byte PitLimiterStatus { get; set; } // 0 = off, 1 = on
        public float FuelInTank { get; set; } // Current fuel mass
        public float FuelCapacity { get; set; }
        public float FuelRemainingLaps { get; set; } // Fuel remaining in terms of laps (value on MFD)
        public ushort MaxRPM { get; set; }
        public ushort IdleRPM { get; set; }
        public byte MaxGears { get; set; }
        public byte DrsAllowed { get; set; } // 0 = not allowed, 1 = allowed
        public ushort DrsActivationDistance { get; set; } // 0 = DRS not available, else DRS available in [x] meters
        public TyreCompoundActual ActualTyreCompound { get; set; }
        public TyreCompoundVisual VisualTyreCompound { get; set; }
        public byte TyresAgeLaps { get; set; }
        public FIAFlag VehicleFiaFlags { get; set; }
        public float EnginePowerICE { get; set; } // Engine power output of ICE (W)
        public float EnginePowerMGUK { get; set; } // Engine power output of MGU-K (W)
        public float ErsStoreEnergy { get; set; } // ERS energy store in Joules
        public ERSDeployMode ErsDeployMode { get; set; }
        public float ErsHarvestedThisLapMGUK { get; set; }
        public float ErsHarvestedThisLapMGUH { get; set; }
        public float ErsDeployedThisLap { get; set; }
        public byte NetworkPaused { get; set; } // Whether the car is paused in a network game
    }
}

/*
 * Restricted data (default value = 0): 
 * 
 * fuelInTank
 * fuelCapacity
 * fuelMix
 * fuelRemainingLaps
 * frontBrakeBias
 * ersDeployMode
 * ersStoreEnergy
 * ersDeployedThisLap
 * ersHarvestedThisLapMGUK
 * ersHarvestedThisLapMGUH
 * enginePowerICE
 * enginePowerMGUK
 */