using F1GameDataParser.Handlers;
using F1GameDataParser.Startup;
using F1GameDataParser.State;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<TelemetryClient>(provider => new TelemetryClient(20777));
                services.AddSingleton<ParticipantsHandler>();
                services.AddSingleton<ParticipantsState>();

                services.AddSingleton<SessionHandler>();
                services.AddSingleton<SessionState>();

                services.AddSingleton<CarTelemetryHandler>();
                services.AddSingleton<CarTelemetryState>();

                services.AddSingleton<EventsHandler>();

                services.AddSingleton<CarStatusHandler>();
                services.AddSingleton<CarStatusState>();

                services.AddSingleton<FinalClassificationHandler>();

                services.AddSingleton<LapHandler>();
                services.AddSingleton<LapState>();

                services.AddSingleton<SessionHistoryHandler>();
                services.AddSingleton<SessionHistoryState>();

                services.AddSingleton<CarDamageHandler>();
                services.AddSingleton<CarDamageState>();

                services.AddSharedServices();
            })
            .Build();

        var telemetryClient = host.Services.GetRequiredService<TelemetryClient>();
        var participantsHandler = host.Services.GetRequiredService<ParticipantsHandler>();
        var participantsState = host.Services.GetRequiredService<ParticipantsState>();
        var sessionHandler = host.Services.GetRequiredService<SessionHandler>();
        var sessionState = host.Services.GetRequiredService<SessionState>();
        var carTelemetryHandler = host.Services.GetRequiredService<CarTelemetryHandler>();
        var carTelemetryState = host.Services.GetRequiredService<CarTelemetryState>();
        var eventsHandler = host.Services.GetRequiredService<EventsHandler>();
        var carStatusHandler = host.Services.GetRequiredService<CarStatusHandler>();
        var carStatusState = host.Services.GetRequiredService<CarStatusState>();
        var finalClassificationHandler = host.Services.GetRequiredService<FinalClassificationHandler>();
        var lapHandler = host.Services.GetRequiredService<LapHandler>();
        var lapState = host.Services.GetRequiredService<LapState>();
        var sessionHistoryHandler = host.Services.GetRequiredService<SessionHistoryHandler>();
        var sessionHistoryState = host.Services.GetRequiredService<SessionHistoryState>();
        var carDamageHandler = host.Services.GetRequiredService<CarDamageHandler>();
        var carDamageState = host.Services.GetRequiredService<CarDamageState>();
        //client.OnCarDamageDataReceive += Client_OnCarDamageDataReceive;
        Console.Read();

    }
    /*
    private static void Client_OnCarDamageDataReceive(CarDamagePacket packet)
    {
        int index = 0;
        Console.Clear();
        foreach (CarDamageData data in packet.carDamageData)
        {
            Console.WriteLine($"INDEX: {index}");
            Console.WriteLine($"{data}");
            Console.WriteLine("----");
            index++;
            if (index == 5)
            {
                break;
            }
        }

        Console.WriteLine($"{packet.carDamageData[packet.header.playerCarIndex]}");
    }
    */
}