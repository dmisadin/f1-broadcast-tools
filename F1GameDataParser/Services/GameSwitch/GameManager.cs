using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123;
using F1GameDataParser.GameProfiles.F125;
using F1GameDataParser.GameProfiles.F1Common;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.Services.GameSwitch
{
    public class GameManager
    {
        private readonly IServiceProvider provider;
        private ITelemetryClient? activeClient;
        private PacketFormat? activeFormat;

        public GameManager(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public bool SwitchGame(PacketFormat format)
        {
            if (activeFormat.HasValue && activeFormat.Value == format)
                return false;

            ITelemetryClient newTelemetryClient;

            switch (format)
            {
                case PacketFormat.F123:
                    newTelemetryClient = provider.GetRequiredService<F123TelemetryClient>();
                    break;
                case PacketFormat.F125:
                    newTelemetryClient = provider.GetRequiredService<F125TelemetryClient>();
                    break;
                default:
                    return false;
            }

            if (activeClient != null)
            {
                try
                {
                    activeClient.Stop();  // should halt BeginReceive loop before dispose
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[GameManager] Error stopping old client: {ex.Message}");
                }
                activeClient = null;
                activeFormat = null;
            }

            activeClient = newTelemetryClient;

            activeClient.Start();
            activeFormat = format;

            Console.WriteLine($"[GameManager] Switched to {format}.");
            return true;
        }

        public void Stop()
        {
            if (activeClient != null)
            {
                try
                {
                    activeClient.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[GameManager] Error stopping client: {ex.Message}");
                }
                activeClient = null;
                activeFormat = null;
            }
        }
    }
}
