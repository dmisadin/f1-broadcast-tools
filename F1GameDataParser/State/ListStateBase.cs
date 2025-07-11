﻿using F1GameDataParser.Models;

namespace F1GameDataParser.State
{
    public abstract class ListStateBase<TModel>
        where TModel : class, IMergeable<TModel>
    {
        protected readonly object _lock = new();
        public Dictionary<int, TModel> State { get; private set; } = new();

        public virtual void Update(IEnumerable<TModel> newState)
        {
            lock (_lock)
            {
                int index = 0;
                foreach (var newModel in newState)
                {
                    if (State.TryGetValue(index, out var existingModel))
                    {
                        existingModel.MergeFrom(newModel);
                        OnModelMerged(index, existingModel, newModel);
                    }
                    else
                    {
                        State[index] = newModel;
                        OnModelAdded(index, newModel);
                    }

                    index++;
                }
            }
        }

        protected virtual void OnModelMerged(int key, TModel existingModel, TModel newModel) { }
        protected virtual void OnModelAdded(int key, TModel newModel) { }

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
