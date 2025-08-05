namespace F1GameDataParser.State
{
    public abstract class PrimitiveDictionaryStateBase<T>
        where T : struct
    {
        protected readonly object _lock = new();
        public Dictionary<int, T> State { get; private set; } = new();
        protected virtual int? GetModelKey(T model) { return null; }

        public virtual void Update(IDictionary<int, T> newState)
        {
            if (newState.Count() <= 0) return;
            lock (_lock)
            {
                foreach (var newModel in newState)
                {
                    int key = newModel.Key;

                    if (State.TryGetValue(key, out var existingModel))
                    {
                        BeforeModelMerged(key, existingModel, newModel.Value);
                        State[key] = newModel.Value;
                    }
                    else
                    {
                        State[key] = newModel.Value;
                        AfterModelAdded(key, newModel.Value);
                    }
                }
            }
        }

        public virtual void Update(KeyValuePair<int, T> newState)
        {
            Update(new Dictionary<int, T> { { newState.Key, newState.Value } } );
        }

        protected virtual void BeforeModelMerged(int key, T existingModel, T newModel) { }
        protected virtual void AfterModelAdded(int key, T newModel) { }

        public virtual T? GetModel(int id)
        {
            if (this.State.TryGetValue(id, out var model))
                return model;

            return null;
        }

        public virtual IEnumerable<T> GetModels(IEnumerable<int> keys)
        {
            var results = new List<T>();

            foreach (var key in keys)
            {
                if (State.TryGetValue(key, out var value))
                {
                    results.Add(value);
                }
            }
            return results;
        }
    }
}
