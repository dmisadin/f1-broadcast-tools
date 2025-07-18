namespace F1GameDataParser.ViewModels.Minimap
{
    public class MinimapCar
    {
        public float X { get; set; } // in pixels
        public float Y { get; set; } // in pixels
        public float Rotation { get; set; } // in degrees
        public int VehicleIdx { get; set; }
        public string TeamColor { get; set; }
    }
}
