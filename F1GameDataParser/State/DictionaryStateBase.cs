using F1GameDataParser.Models;

namespace F1GameDataParser.State
{
    public abstract class DictionaryStateBase<TModel>
        where TModel : class, IMergeable<TModel>
    {
        protected readonly object _lock = new();
        public Dictionary<int, TModel> State { get; private set; } = new();
        protected virtual int? GetModelKey(TModel model) { return null; }

        public virtual void Update(IEnumerable<TModel> newState)
        {
            if (newState.Count() <= 0) return;
            lock (_lock)
            {
                BeforeEnumerableMerged(newState);

                int index = 0;
                foreach (var newModel in newState)
                {
                    int key = GetModelKey(newModel) ?? index;

                    if (State.TryGetValue(key, out var existingModel))
                    {
                        OnModelMerged(key, existingModel, newModel);
                        existingModel.MergeFrom(newModel);
                    }
                    else
                    {
                        State[key] = newModel;
                        OnModelAdded(key, newModel);
                    }

                    index++;
                }
            }
        }

        public virtual void Update(TModel newState)
        {
            Update([newState]);
        }

        protected virtual void BeforeEnumerableMerged(IEnumerable<TModel> newState) { }
        protected virtual void OnModelMerged(int key, TModel existingModel, TModel newModel) { }
        protected virtual void OnModelAdded(int key, TModel newModel) { }

        public virtual TModel? GetModel(int id)
        {
            if (this.State.TryGetValue(id, out var model))
                return model;

            return null;
        }

        public virtual IEnumerable<TModel> GetModels(IEnumerable<int> keys)
        {
            var results = new List<TModel>();

            foreach (var key in keys)
            {
                if (State.TryGetValue(key, out var value))
                {
                    results.Add(value);
                }
            }
            return results;
        }

        public virtual List<TModel> GetAll() => State.Select(m => m.Value).ToList();
    }
}
