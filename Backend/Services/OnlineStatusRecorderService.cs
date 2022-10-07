using System.Net;
using Backend.AspPlugins;
using Models;

namespace Backend.Services; 

/// <summary>
/// Regularly checks and records the availability of all webservices.
/// </summary>
public class OnlineStatusRecorderService : BackgroundService {
    private readonly HttpClient _client = new();
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly BackendSettings _opt;

    public OnlineStatusRecorderService(IServiceScopeFactory scopeFactory, BackendSettings opt) {
        _scopeFactory = scopeFactory;
        _opt = opt;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            var waitUntil = DateTimeOffset.UtcNow.Add(_opt.MinimumTimeBetweenApiChecks);
            
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AtriaContext>();
            var chunks = db.WebserviceEntries.AsEnumerable()
                .Select<WebserviceEntry, Task<(WebserviceEntry, ApiCheck?)>>(async x => {
                    var time = DateTimeOffset.UtcNow;
                    HttpStatusCode status;
                    try {
                        using var res = await _client.GetAsync(x.ApiCheckUrl ?? x.Link, stoppingToken);
                        status = res.StatusCode;
                    } catch (InvalidOperationException) {
                        status = ApiCheck.InvalidWseConfiguration;
                    } catch (HttpRequestException) {
                        status = ApiCheck.ClientSideFailure;
                    } catch (TaskCanceledException) {
                        return (x, null);
                    }
                    return (x, new() { CheckedAt = time, Status = status });
                })
                .Chunk(_opt.MaxConcurrentApiChecks);
            foreach (var chunk in chunks) {
                foreach (var (wse, check) in await Task.WhenAll(chunk)) {
                    if (check != null) {
                        wse.ApiCheckHistory.Add(check);
                        wse.LatestCheckStatus = check.Status;
                    }
                }

                if (stoppingToken.IsCancellationRequested) {
                    break;
                }
            }
            await db.SaveChangesAsync();

            var waitTime = waitUntil - DateTimeOffset.UtcNow;
            if (waitTime.Ticks > 0) {
                await Task.Delay(waitTime, stoppingToken);
            }
        }
    }
}
