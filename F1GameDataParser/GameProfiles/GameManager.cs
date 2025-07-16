using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123;
using F1GameDataParser.GameProfiles.F125;
using F1GameDataParser.GameProfiles.F1Common;
using Microsoft.Extensions.DependencyInjection;

namespace F1GameDataParser.GameProfiles
{
    public class GameManager
    {
        private ITelemetryClient? _activeClient;

        static public GameYear PromptUserForGame()
        {
            Console.WriteLine("Select F1 Game:");
            Console.WriteLine("1. F1 23");
            Console.WriteLine("2. F1 25");
            Console.Write("Enter choice: ");

            while (true)
            {
                var input = Console.ReadLine();
                return input?.Trim() switch
                {
                    "1" => GameYear.F123,
                    "2" => GameYear.F125,
                    _ => PromptRetry()
                };
            }

            static GameYear PromptRetry()
            {
                Console.Write("Invalid input. Please enter 1 or 2: ");
                return PromptUserForGame();
            }
        }

        public void SwitchGame(GameYear gameYear, IServiceProvider provider)
        {
            _activeClient?.Stop();
            _activeClient = null;

            switch (gameYear)
            {
                case GameYear.F123:
                    _activeClient = provider.GetRequiredService<F123TelemetryClient>();
                    break;
                case GameYear.F125:
                    _activeClient = provider.GetRequiredService<F125TelemetryClient>();
                    break;
                default:
                    Console.WriteLine("Unknown game selection.");
                    throw new InvalidOperationException($"Unknown game: {gameYear}");
            }

            _activeClient.Start();

            Console.WriteLine($"Switched to F1 {gameYear}.");
        }

        public void Stop()
        {
            _activeClient?.Stop();
            _activeClient = null;
        }
    }
}
