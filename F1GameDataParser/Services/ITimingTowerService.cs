using F1GameDataParser.Dtos.TimingTower;

namespace F1GameDataParser.Services
{
    public interface ITimingTowerService
    {
        TimingTower? GetTimingTower();
    }
}
