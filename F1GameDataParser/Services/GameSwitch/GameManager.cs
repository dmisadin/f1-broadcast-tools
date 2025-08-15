using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.Services.GameSwitch
{
    public class GameManager
    {
        private readonly IServiceProvider serviceProvider;
        private ITelemetryClient? activeClient;
        private PacketFormat? activeFormat;

        public GameManager(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public bool SwitchGame(PacketFormat format)
        {
            if (activeFormat.HasValue && activeFormat.Value == format)
                return false;

            activeClient?.Stop();
            activeClient = serviceProvider.GetRequiredKeyedService<ITelemetryClient>(format);
            activeClient.Start();

            activeFormat = format;
            Console.WriteLine($"[GameManager] Switched to {format}.");
            return true;
        }

        public void Stop()
        {
            if (activeClient == null)
                return;

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
