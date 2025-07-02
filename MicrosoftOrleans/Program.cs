using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using MicrosoftOrleans;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure Orleans
builder.Services.AddOrleans(orleansBuilder =>
{
    orleansBuilder
        .UseLocalhostClustering() // For development - use memory clustering
        .ConfigureLogging(logging => logging.AddConsole());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Original weather forecast endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Orleans Counter API endpoints
app.MapGet("/counter/{id}", async (string id, IGrainFactory grainFactory) =>
{
    var counter = grainFactory.GetGrain<ICounterGrain>(id);
    var count = await counter.GetCountAsync();
    return Results.Ok(new { Id = id, Count = count });
})
.WithName("GetCounter")
.WithOpenApi();

app.MapPost("/counter/{id}/increment", async (string id, IGrainFactory grainFactory, int amount = 1) =>
{
    var counter = grainFactory.GetGrain<ICounterGrain>(id);
    var newCount = await counter.IncrementAsync(amount);
    return Results.Ok(new { Id = id, Count = newCount, Operation = "increment", Amount = amount });
})
.WithName("IncrementCounter")
.WithOpenApi();

app.MapPost("/counter/{id}/decrement", async (string id, IGrainFactory grainFactory, int amount = 1) =>
{
    var counter = grainFactory.GetGrain<ICounterGrain>(id);
    var newCount = await counter.DecrementAsync(amount);
    return Results.Ok(new { Id = id, Count = newCount, Operation = "decrement", Amount = amount });
})
.WithName("DecrementCounter")
.WithOpenApi();

app.MapPost("/counter/{id}/reset", async (string id, IGrainFactory grainFactory) =>
{
    var counter = grainFactory.GetGrain<ICounterGrain>(id);
    await counter.ResetAsync();
    return Results.Ok(new { Id = id, Count = 0, Operation = "reset" });
})
.WithName("ResetCounter")
.WithOpenApi();

app.MapGet("/counter/{id}/state", async (string id, IGrainFactory grainFactory) =>
{
    var counter = grainFactory.GetGrain<ICounterGrain>(id);
    var state = await counter.GetStateAsync();
    return Results.Ok(new { Id = id, State = state });
})
.WithName("GetCounterState")
.WithOpenApi();

app.MapGet("/orleans/stats", async (IGrainFactory grainFactory) =>
{
    // Simple stats endpoint
    return Results.Ok(new 
    { 
        Timestamp = DateTime.UtcNow,
        Message = "Orleans 9.1.2 is running",
        Version = "9.1.2"
    });
})
.WithName("OrleansStats");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
