using F1GameDataParser.Database;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Handlers;
using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.Services;
using F1GameDataParser.Startup;
using F1GameDataParser.State;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure services
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddSingleton<TelemetryClient>(provider => new TelemetryClient(20777));
builder.Services.AddSingleton<ParticipantsHandler>();
builder.Services.AddSingleton<ParticipantsState>();

builder.Services.AddSingleton<SessionHandler>();
builder.Services.AddSingleton<SessionState>();

builder.Services.AddSingleton<CarTelemetryHandler>();
builder.Services.AddSingleton<CarTelemetryState>();

builder.Services.AddSingleton<EventsHandler>();

builder.Services.AddSingleton<CarStatusHandler>();
builder.Services.AddSingleton<CarStatusState>();

builder.Services.AddSingleton<FinalClassificationHandler>();

builder.Services.AddSingleton<LapHandler>();
builder.Services.AddSingleton<LapState>();

builder.Services.AddSingleton<SessionHistoryHandler>();
builder.Services.AddSingleton<SessionHistoryState>();

builder.Services.AddSingleton<CarDamageHandler>();
builder.Services.AddSingleton<CarDamageState>();

builder.Services.AddSingleton<LobbyInfoHandler>();
builder.Services.AddSingleton<LobbyInfoState>();

builder.Services.AddTransient<TimingTowerFactory>();

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
    var telemetryClient = services.GetRequiredService<TelemetryClient>();

    var participantsState = services.GetRequiredService<ParticipantsState>();
    var sessionState = services.GetRequiredService<SessionState>();
    var carTelemetryState = services.GetRequiredService<CarTelemetryState>();
    var lapState = services.GetRequiredService<LapState>();
    var sessionHistoryState = services.GetRequiredService<SessionHistoryState>();
    var carDamageState = services.GetRequiredService<CarDamageState>();
    var carStatusState = services.GetRequiredService<CarStatusState>();
    var lobbyInfoState = services.GetRequiredService<LobbyInfoState>();

    var participantsHandler = services.GetRequiredService<ParticipantsHandler>();
    var sessionHandler = services.GetRequiredService<SessionHandler>();
    var carTelemetryHandler = services.GetRequiredService<CarTelemetryHandler>();
    var eventsHandler = services.GetRequiredService<EventsHandler>();
    var carStatusHandler = services.GetRequiredService<CarStatusHandler>();
    var finalClassificationHandler = services.GetRequiredService<FinalClassificationHandler>();
    var lapHandler = services.GetRequiredService<LapHandler>();
    var sessionHistoryHandler = services.GetRequiredService<SessionHistoryHandler>();
    var carDamageHandler = services.GetRequiredService<CarDamageHandler>();
    var lobbyInfoHandler = services.GetRequiredService<LobbyInfoHandler>();
}

app.UseRouting();
app.MapControllers();
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

// Run the application
await app.RunAsync();
