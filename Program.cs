using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.AddConsoleExporter();
});

// The scopes will show up as expected in the console if UseAzureMonitor is removed
builder.Services.AddOpenTelemetry().UseAzureMonitor();

builder.Services.AddSingleton<TestService>();

var app = builder.Build();

app.MapGet("/", async (TestService service) =>
    await service.GetStuff());

app.Run();

class TestService(ILogger<TestService> logger)
{
    private readonly ILogger<TestService> _logger = logger;

    public Task<string> GetStuff()
    {
        List<KeyValuePair<string, object>> context =
        [
            new("test", "context")
        ];

        using var scope = _logger.BeginScope(context);
        _logger.LogWarning("This log line should have scopes, but does not");
        return Task.FromResult("Hello world!");
    }
}
