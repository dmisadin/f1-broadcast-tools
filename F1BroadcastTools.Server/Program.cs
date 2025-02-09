using F1GameDataParser.Services;
using F1GameDataParser.Startup;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSharedServices();
//builder.Services.AddTransient<TimingTowerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var myService = scope.ServiceProvider.GetRequiredService<ITimingTowerService>();
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Apply camelCase naming
        WriteIndented = true // Optional: makes the output more readable
    };
    var test = myService.GetTimingTower();
    var test2 = JsonSerializer.Serialize(test, options);
    Console.WriteLine(test2);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
