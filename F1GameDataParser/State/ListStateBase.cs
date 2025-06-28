using F1GameDataParser.Models;
using F1GameDataParser.Utility;

namespace F1GameDataParser.State
{
    public abstract class ListStateBase<TModel>
        where TModel : class, IMergeable<TModel>
    {
        private readonly object _lock = new();
        public Dictionary<int, TModel> State { get; private set; } = new();

        public virtual void Update(IEnumerable<TModel> newState)
        {
            var modelDict = DictionaryUtility.FromModelToDictionary(newState);

            lock (_lock)
            {
                foreach (var (key, newModel) in modelDict)
                {
                    if (State.TryGetValue(key, out var existingModel))
                    {
                        existingModel.MergeFrom(newModel);
                    }
                    else
                    {
                        State[key] = newModel;
                    }
                }
            }
        }


        public virtual TModel? GetModel(int id)
        {
            if (this.State.TryGetValue(id, out var model))
                return model;

            return null;
        }

        public virtual List<TModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
