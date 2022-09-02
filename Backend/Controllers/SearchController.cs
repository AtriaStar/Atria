using Backend.AspPlugins;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase {
    private readonly AtriaContext _context;
    private readonly BackendOptions _options;
    private readonly FuzzingService _fuzzer;

    public SearchController(AtriaContext context, BackendOptions opt, FuzzingService fuzzer) {
        _context = context;
        _options = opt;
        _fuzzer = fuzzer;
    }

    private IEnumerable<WebserviceEntry> GetBaseWse(WseSearchParameters parameters)
        => _context.WebserviceEntries
            .Where(x => (!x.Reviews.Any() || x.Reviews.Average(y => (int) y.StarCount) >= (int) parameters.MinReviewAvg)
            && (parameters.Tags == null || x.Tags.Intersect(parameters.Tags).Count() == parameters.Tags.Count)
            && (parameters.IsOnline == null || true /* TODO */)
            && (parameters.HasBookmark == null || true /* TODO */))
            .AsEnumerable()
            .Select(x => (wse: x, score: parameters.Query == null ? 1 : _fuzzer.CalculateScore1(parameters.Query, x)))
            .Where(x => x.score >= _options.MinimumWseScore)
            .Select(x => x with { score = x.score * parameters.Order.GetMapper().Invoke(x.wse) })
            .OrderBy(x => parameters.Ascending ? x.score : -x.score)
            .Select(x => {
                x.wse.ChangeLog = x.score.ToString();
                return x.wse;
            });

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WseSearchParameters parameters, [FromQuery] Pagination pagination)
        => GetBaseWse(parameters).Paginate(pagination);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WseSearchParameters parameters) => GetBaseWse(parameters).LongCount();

    private IEnumerable<User> GetBaseUsers(string query)
        => _context.Users
            .AsEnumerable()
            .Select(x => (user: x, score: _fuzzer.CalculateScore(query, x)))
            .OrderByDescending(x => x.score)
            .TakeWhile(x => x.score >= _options.MinimumUserScore)
            .Select(x => x.user);

    [HttpGet("user")]
    public IActionResult GetUserList(string query, [FromQuery] Pagination pagination)
        => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).Paginate(pagination));

    [HttpGet("user/count")]
    public IActionResult GetUserCount(string query) => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).LongCount());
}
