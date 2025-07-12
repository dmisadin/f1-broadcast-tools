using F1GameDataParser.GameProfiles.F123.ModelFactories;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public abstract class GenericHandler<TPacket, TModel>
        where TPacket : struct
        where TModel : class
    {
        // TO DO: Dependency Injection for inherited classes (TelemetryClient)
        protected abstract IModelFactory<TPacket, TModel> ModelFactory { get; }
        protected abstract void OnRecieved(TPacket packet);
    }
}
