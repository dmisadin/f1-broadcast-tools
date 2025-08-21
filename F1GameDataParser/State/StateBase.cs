using F1GameDataParser.Models;

namespace F1GameDataParser.State
{
    public abstract class StateBase<TModel> 
        where TModel : class, IMergeable<TModel>
    {
        protected readonly object _lock = new();

        public TModel? State { get; protected set; }

        public virtual void Update(TModel newState) 
        {
            lock (_lock)
            {
                if (State != null)
                {
                    BeforeModelMerged(State, newState);
                    State.MergeFrom(newState);
                }
                else
                {
                    State = newState;
                    AfterModelAdded(newState);
                }
            }
        }

        protected virtual void BeforeModelMerged(TModel existingModel, TModel newModel) { }
        protected virtual void AfterModelAdded(TModel newModel) { }
    }
}
