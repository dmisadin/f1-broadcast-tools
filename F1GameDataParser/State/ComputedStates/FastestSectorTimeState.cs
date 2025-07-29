using F1GameDataParser.Enums;
using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class FastestSectorTimeState : DictionaryStateBase<FastestSectorTime>
    {
        public FastestSectorTimeState()
        {
            var defaultFastestSectors = new List<FastestSectorTime> 
            { 
                new FastestSectorTime { Sector = Sector.Sector1, VehicleIdx = 255 },
                new FastestSectorTime { Sector = Sector.Sector2, VehicleIdx = 255 },
                new FastestSectorTime { Sector = Sector.Sector3, VehicleIdx = 255 }
            };
            this.Update(defaultFastestSectors);
        }
        protected override int? GetModelKey(FastestSectorTime model) => (int)model.Sector;
    }
}
