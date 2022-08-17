using Backend.Services;
using Timer = System.Timers.Timer;

namespace Backend.Authentication; 

public class SessionClearerService : IHostedService {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Timer _timer;
    private readonly SessionService _sessionService;

    public SessionClearerService(IServiceScopeFactory scopeFactory, SessionService sessionService) {
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
