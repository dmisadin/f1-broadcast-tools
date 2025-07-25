using F1GameDataParser.Database;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.GameProfiles;
using F1GameDataParser.GameProfiles.F123;
using F1GameDataParser.GameProfiles.F123.Handlers;
using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.Services;
using F1GameDataParser.Startup;
using F1GameDataParser.State;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using F1GameDataParser.GameProfiles.F125.Handlers;
using F1GameDataParser.GameProfiles.F125;
using F1GameDataParser.State.ComputedStates;

var builder = WebApplication.CreateBuilder(args);

// Configure services
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddSingleton<GameManager>();
builder.Services.AddSingleton<F123TelemetryClient>();
builder.Services.AddSingleton<F125TelemetryClient>();
builder.Services.AddSingleton<MotionState>();
builder.Services.AddSingleton<ParticipantsState>();
builder.Services.AddSingleton<SessionState>();
builder.Services.AddSingleton<CarTelemetryState>();
builder.Services.AddSingleton<CarStatusState>();
builder.Services.AddSingleton<LapState>();
builder.Services.AddSingleton<SessionHistoryState>();
builder.Services.AddSingleton<CarDamageState>();
builder.Services.AddSingleton<LobbyInfoState>();

/*
builder.Services.AddSingleton<ParticipantsHandler>();
builder.Services.AddSingleton<SessionHandler>();
builder.Services.AddSingleton<CarTelemetryHandler>();
builder.Services.AddSingleton<EventsHandler>();
builder.Services.AddSingleton<CarStatusHandler>();
builder.Services.AddSingleton<FinalClassificationHandler>();
builder.Services.AddSingleton<LapHandler>();
builder.Services.AddSingleton<SessionHistoryHandler>();
builder.Services.AddSingleton<CarDamageHandler>();
builder.Services.AddSingleton<LobbyInfoHandler>();

builder.Services.AddSingleton<F125.Handlers.ParticipantsHandler>();
builder.Services.AddSingleton<F125.Handlers.SessionHandler>();
builder.Services.AddSingleton<F125.Handlers.CarTelemetryHandler>();
builder.Services.AddSingleton<F125.Handlers.EventsHandler>();
builder.Services.AddSingleton<F125.Handlers.CarStatusHandler>();
builder.Services.AddSingleton<F125.Handlers.FinalClassificationHandler>();
builder.Services.AddSingleton<F125.Handlers.LapHandler>();
builder.Services.AddSingleton<F125.Handlers.SessionHistoryHandler>();
builder.Services.AddSingleton<F125.Handlers.CarDamageHandler>();
builder.Services.AddSingleton<F125.Handlers.LobbyInfoHandler>();
*/
builder.Services.AddF123Handlers();
builder.Services.AddF125Handlers();


builder.Services.AddSingleton<DriverOverrideState>();
builder.Services.AddSingleton<PersonalBestLapState>();
builder.Services.AddSingleton<LatestLapTimeState>();

builder.Services.AddTransient<TimingTowerFactory>();
builder.Services.AddTransient<MinimapFactory>();
builder.Services.AddSingleton<StopwatchFactory>();
builder.Services.AddTransient<DriverOverrideService>();

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

// Configure WebSocket options
app.UseWebSockets();

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


    var gameManager = services.GetRequiredService<GameManager>();

    var selectedGame = GameManager.PromptUserForGame();
    gameManager.SwitchGame(selectedGame, services);
}

app.UseRouting();
app.MapControllers();
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

// Run the application
await app.RunAsync();
