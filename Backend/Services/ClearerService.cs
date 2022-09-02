using Backend.AspPlugins;
using Timer = System.Timers.Timer;

namespace Backend.Services;

public class ClearerService : IHostedService {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Timer _timer;
    private readonly SessionService _sessionService;

    public ClearerService(IServiceScopeFactory scopeFactory, SessionService sessionService) {
        _scopeFactory = scopeFactory;
        _sessionService = sessionService;
        _timer = new(TimeSpan.FromMinutes(1).TotalMilliseconds);
        _timer.Elapsed += async (_, _) => await Clear();
    }

    private async Task Clear() {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AtriaContext>();
        db.Sessions.RemoveRange(db.Sessions
            .Where(x => DateTimeOffset.UtcNow - x.CreatedAt > _sessionService.ExpireDuration));
        db.ResetTokens.RemoveRange(db.ResetTokens
            .Where(x => DateTimeOffset.UtcNow - x.CreatedAt > TimeSpan.FromMinutes(15))); // TODO: Add to config
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
