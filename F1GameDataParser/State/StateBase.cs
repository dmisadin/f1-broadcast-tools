namespace F1GameDataParser.State
{
    public abstract class StateBase<TModel> 
        where TModel : class
    {
        private readonly object _lock = new();

        public TModel? State { get; private set; }

        public virtual void Update(TModel newState) 
        {
            lock (_lock)
            {
                State = newState;
            }
        }
    }
}
