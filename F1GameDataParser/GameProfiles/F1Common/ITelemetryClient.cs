namespace F1GameDataParser.GameProfiles.F1Common
{
    public interface ITelemetryClient
    {
        bool Connected { get; }

        event Action<bool>? OnConnectedStatusChange;

        void Start();
        void Stop();
    }
}
