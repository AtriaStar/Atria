using Backend.AspPlugins;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace Backend.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase {
    private readonly AtriaContext _context;

    public SearchController(AtriaContext context) {
        _context = context;
    }

    private IEnumerable<WebserviceEntry> GetBaseWses(WseSearchParameters parameters)
        => _context.WebserviceEntries
            .Include(x => x.Tags)
            .Where(x => (!x.Reviews.Any() || x.Reviews.Average(y => (int) y.StarCount) >= (int) parameters.MinReviewAvg)
                && (parameters.Tags == null || x.Tags.Count(y => parameters.Tags.Contains(y)) == parameters.Tags.Count) // Fuck
                && (parameters.IsOnline == null || true /* TODO */)
                && (parameters.HasBookmark == null || true /* TODO */))
            .AsEnumerable()
            .Select(x => (wse: x, score: string.IsNullOrEmpty(parameters.Query) ? 1 : FuzzingService.CalculateScore1(parameters.Query, x)))
            .Where(x => x.score > 0.05)
            .Select(x => x with { score = x.score * parameters.Order.GetMapper().Invoke(x.wse) })
            .OrderBy(x => parameters.Ascending ? x.score : -x.score)
            .Select(x => {
                x.wse.ChangeLog = x.score.ToString();
                return x.wse;
            });

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WseSearchParameters parameters, [FromQuery] Pagination pagination)
        => GetBaseWses(parameters).Paginate(pagination);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WseSearchParameters parameters) => GetBaseWses(parameters).LongCount();

    private IEnumerable<User> GetBaseUsers(string query)
        => _context.Users
            .AsEnumerable()
            .Select(x => (user: x, score: FuzzingService.CalculateScore(query, x)))
            .OrderByDescending(x => x.score)
            .TakeWhile(x => x.score > 0.5)
            .Select(x => x.user);

    [HttpGet("user")]
    public IActionResult GetUserList(string query, [FromQuery] Pagination pagination)
        => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).Paginate(pagination));

    [HttpGet("user/count")]
    public IActionResult GetUserCount(string query) => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).LongCount());
}
