using F1GameDataParser.Models.DriverOverride;
using F1GameDataParser.Utility;

namespace F1GameDataParser.State
{
    public class DriverOverrideState : DictionaryStateBase<DriverOverride>
    {
        // UPDATE() is missing logic when override is removed (press X in ng-select)
        public override void Update(IEnumerable<DriverOverride> newState)
        {
            var modelDict = DictionaryUtility.FromModelToDictionary(newState);

            lock (_lock)
            {
                foreach (var (key, newModel) in modelDict)
                {
                    if (State.TryGetValue(key, out var existingModel))
                    {
                        existingModel.MergeFrom(newModel);
                        OnModelMerged(key, existingModel, newModel);
                    }
                    else
                    {
                        State[key] = newModel;
                        OnModelAdded(key, newModel);
                    }
                }
            }
        }
        public override List<DriverOverride> GetAll()
        {
            return this.State.Select((model, key) => new DriverOverride
            {
                Id = key,
                PlayerId = model.Value.PlayerId,
                Player = model.Value.Player
            }).ToList(); // TODO: triba napravit dohvacanje svih entryja 
        }
    }
}
