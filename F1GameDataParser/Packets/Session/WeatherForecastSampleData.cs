﻿using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.Packets.Session
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WeatherForecastSampleData
    {
        public SessionType sessionType;
        public byte timeOffset;
        public Weather weather;
        public sbyte trackTemperature;
        public ChangeType trackTemperatureChange;
        public sbyte airTemperature;
        public ChangeType airTemperatureChange;
        public byte rainPercentage;
    }
}
