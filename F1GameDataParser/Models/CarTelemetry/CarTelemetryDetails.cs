using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.CarTelemetry
{
    public class CarTelemetryDetails
    {
        public ushort Speed { get; set; } // km/h
        public float Throttle { get; set; } // [0.0, 1.0]
        public float Steer { get; set; } // [-1.0, 1.0]
        public float Brake { get; set; } // [0.0, 1.0]
        public byte Clutch { get; set; } // [0, 100]
        public sbyte Gear { get; set; } // [1, 8], N=0, R=-1
        public ushort EngineRPM { get; set; }
        public bool DRS { get; set; }
        public byte RevLightsPercent { get; set; }
        public ushort RevLightsBitValue { get; set; } // Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED)
        public IEnumerable<ushort> BrakesTemperature { get; set; } // Celsius
        public IEnumerable<byte> TyresSurfaceTemperature { get; set; } // Celsius
        public IEnumerable<byte> TyresInnerTemperature { get; set; } // Celsius
        public ushort EngineTemperature { get; set; } // Celsius
        public IEnumerable<float> TyresPressure { get; set; } // PSI
        public IEnumerable<Surface> SurfaceType { get; set; }
    }
}
