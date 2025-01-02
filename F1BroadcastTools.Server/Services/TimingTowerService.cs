using F1GameDataParser.Mapping.ViewModelBuilders;
using F1GameDataParser.ViewModels.TimingTower;

namespace F1BroadcastTools.Server.Services
{
    public class TimingTowerService
    {
        private readonly TimingTowerBuilder timingTowerBuilder;

        public TimingTowerService(TimingTowerBuilder timingTowerBuilder)
        {
            this.timingTowerBuilder = timingTowerBuilder;
        }

        public TimingTower? GetTimingTowerData()
        {
            return this.timingTowerBuilder.Generate();
        }
    }
}
