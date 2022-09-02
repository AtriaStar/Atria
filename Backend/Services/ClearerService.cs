using Backend.AspPlugins;
using Timer = System.Timers.Timer;

namespace Backend.Services;

public class ClearerService : IHostedService {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Timer _timer;
    private readonly SessionService _sessionService;
    private readonly BackendOptions _options;

    public ClearerService(IServiceScopeFactory scopeFactory, SessionService sessionService, BackendOptions opt) {
        _scopeFactory = scopeFactory;
        _sessionService = sessionService;
        _options = opt;
        _timer = new(TimeSpan.FromMinutes(1).TotalMilliseconds);
        _timer.Elapsed += async (_, _) => await Clear();
    }

    private async Task Clear() {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AtriaContext>();
        // Using methods here isn't possible due to translating issues with EF
        db.Sessions.RemoveRange(db.Sessions
            .Where(x => DateTimeOffset.UtcNow - x.CreatedAt > _sessionService.ExpireDuration));
        db.ResetTokens.RemoveRange(db.ResetTokens
            .Where(x => DateTimeOffset.UtcNow - x.CreatedAt > _options.ResetTokenExpireDuration));
        await db.SaveChangesAsync();
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        _timer.Stop();
        return Task.CompletedTask;
    }
}
