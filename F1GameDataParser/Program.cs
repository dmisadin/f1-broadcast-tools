using F1GameDataParser.Database;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123;
using F1GameDataParser.GameProfiles.F123.Handlers;
using F1GameDataParser.GameProfiles.F125;
using F1GameDataParser.GameProfiles.F125.Handlers;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.Services;
using F1GameDataParser.Services.GameSwitch;
using F1GameDataParser.Startup;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.State.WidgetStates;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure services
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddKeyedTransient<ITelemetryClient, F123TelemetryClient>(PacketFormat.F123);
builder.Services.AddKeyedTransient<ITelemetryClient, F125TelemetryClient>(PacketFormat.F125);
builder.Services.AddSingleton<GameManager>();

builder.Services.AddSingleton<MotionState>();
builder.Services.AddSingleton<ParticipantsState>();
builder.Services.AddSingleton<SessionState>();
builder.Services.AddSingleton<CarTelemetryState>();
builder.Services.AddSingleton<CarStatusState>();
builder.Services.AddSingleton<LapState>();
builder.Services.AddSingleton<SessionHistoryState>();
builder.Services.AddSingleton<CarDamageState>();
builder.Services.AddSingleton<LobbyInfoState>();

builder.Services.AddSingleton<DriverDetailsBroadcastService>();

builder.Services.AddF123Handlers();
builder.Services.AddF125Handlers();

builder.Services.AddSingleton<DriverOverrideState>();
builder.Services.AddSingleton<PersonalBestLapState>();
builder.Services.AddSingleton<LatestLapTimeState>();
builder.Services.AddSingleton<DriversOnFlyingLapState>();
builder.Services.AddSingleton<FastestSectorTimeState>();
builder.Services.AddSingleton<SectorTimingComparisonState>();

builder.Services.AddTransient<TimingTowerFactory>();
builder.Services.AddTransient<MinimapFactory>();
builder.Services.AddTransient<StopwatchFactory>();
builder.Services.AddTransient<HaloTelemetryDashboardFactory>();
builder.Services.AddTransient<WeatherForecastFactory>();
builder.Services.AddTransient<SectorTimingComparisonFactory>();
builder.Services.AddTransient<SpeedDifferenceFactory>();
builder.Services.AddTransient<DriverOverrideService>(); 
builder.Services.AddTransient<DriverDetailService>();

builder.Services.AddSingleton<WebSocketConnectionManager>();
builder.Services.AddSingleton<WebSocketBroadcastService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<WebSocketBroadcastService>());


// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

// Register services
builder.Services.AddScoped(typeof(PlayerService));

builder.Services.AddSharedServices();

await DataAccess.InitializeAndMigrateDatabase();

// Register DbContext

string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "F1BroadcastTools");
string dbpath = Path.Combine(folderPath, "f1BroadcastTools.db");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(dbpath)); // Use SQLite

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var motionState = services.GetRequiredService<MotionState>();
    var participantsState = services.GetRequiredService<ParticipantsState>();
    var sessionState = services.GetRequiredService<SessionState>();
    var carTelemetryState = services.GetRequiredService<CarTelemetryState>();
    var lapState = services.GetRequiredService<LapState>();
    var sessionFastestLapsState = services.GetRequiredService<PersonalBestLapState>();
    var latestLapTimeState = services.GetRequiredService<LatestLapTimeState>();
    var sessionHistoryState = services.GetRequiredService<SessionHistoryState>();
    var carDamageState = services.GetRequiredService<CarDamageState>();
    var carStatusState = services.GetRequiredService<CarStatusState>();
    var lobbyInfoState = services.GetRequiredService<LobbyInfoState>();
    var driverOverrideState = services.GetRequiredService<DriverOverrideService>();

    var manager = services.GetRequiredService<GameManager>();
    GameDetector.Initialize(manager);
    GameDetector.Start();

}

app.UseWebSockets();
app.UseRouting();
app.MapControllers();
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

await app.RunAsync();
