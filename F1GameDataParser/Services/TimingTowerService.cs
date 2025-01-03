using F1GameDataParser.Mapping.ViewModelBuilders;
using F1GameDataParser.ViewModels.TimingTower;

namespace F1GameDataParser.Services
{
    public class TimingTowerService : ITimingTowerService
    {
        private readonly TimingTowerBuilder timingTowerBuilder;
        public TimingTowerService(TimingTowerBuilder timingTowerBuilder)
        {
            this.timingTowerBuilder = timingTowerBuilder;
        }

        public TimingTower? GetTimingTower()
        {
            return this.timingTowerBuilder.Generate();
        }
    }
}
