using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;

namespace F1GameDataParser.ViewModels
{
    public class DriverBasicDetails
    {
        public int VehicleIdx { get; set; }
        public Team TeamId { get; set; }
        public TeamDetails? TeamDetails { get; set; }
        public string Name { get; set; }
    }
}
