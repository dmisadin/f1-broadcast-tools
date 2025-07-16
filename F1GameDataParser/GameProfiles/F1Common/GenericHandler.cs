namespace F1GameDataParser.GameProfiles.F1Common
{
    public abstract class GenericHandler<TPacket, TModel>
        where TPacket : struct
        where TModel : class
    {
        // TO DO: Dependency Injection for inherited classes (TelemetryClient)
        protected abstract IModelFactory<TPacket, TModel> ModelFactory { get; }
        public abstract void OnReceived(TPacket packet);
    }
}
