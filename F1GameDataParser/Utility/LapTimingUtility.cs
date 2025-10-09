using F1GameDataParser.ViewModels.Enums;

namespace F1GameDataParser.Utility
{
    public static class LapTimingUtility
    {
        public static SectorTimeStatus? CompareSectorTimes(uint sectorTime, uint leaderSectorTime, uint personalBestSectorTime)
        {
            if (sectorTime <= 0)
                return null;
            if (sectorTime <= leaderSectorTime)
                return SectorTimeStatus.FasterThanLeader;
            if (sectorTime <= personalBestSectorTime)
                return SectorTimeStatus.PersonalBest;
            return SectorTimeStatus.NoImprovement;
        }

        public static SectorTimeStatus? CompareSectorTimes(uint driverSectorTime, uint driverAheadSectorTime)
        {
            if (driverSectorTime <= 0)
                return null;
            if (driverSectorTime <= driverAheadSectorTime)
                return SectorTimeStatus.PersonalBest;
            return SectorTimeStatus.NoImprovement;
        }
    }
}
