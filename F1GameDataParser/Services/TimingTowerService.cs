using F1GameDataParser.Mapping.DtoFactories;
using F1GameDataParser.Dtos.TimingTower;

namespace F1GameDataParser.Services
{
    public class TimingTowerService : ITimingTowerService
    {
        private readonly TimingTowerFactory timingTowerBuilder;
        public TimingTowerService(TimingTowerFactory timingTowerBuilder)
        {
            this.timingTowerBuilder = timingTowerBuilder;
        }

        public TimingTower? GetTimingTower()
        {
            return this.timingTowerBuilder.Generate();
        }
    }
}
