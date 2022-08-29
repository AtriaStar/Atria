using Backend.AspPlugins;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase {
    private readonly AtriaContext _context;

    public SearchController(AtriaContext context) {
        _context = context;
    }

    private IEnumerable<WebserviceEntry> GetBaseWses(WSESearchParam param)
        => _context.WebserviceEntries
            .Where(x => (!x.Reviews.Any() || x.Reviews.Average(y => (int) y.StarCount) >= (int) param.MinReviewAvg)
            && (param.Tags == null || x.Tags.Intersect(param.Tags).Count() == param.Tags.Count)
            && (param.IsOnline == null || true /* TODO */)
            && (param.HasBookmark == null || true /* TODO */))
            .AsEnumerable()
            .Select(x => (wse: x, score: param.Order.GetMapper().Invoke(x)
                                         * (param.Query == null ? 1 : FuzzingService.CalculateScore1(param.Query, x))))
            .OrderByDescending(x => x.score)
            .TakeWhile(x => param.Query == null || x.score > 0.05)
            .Select(x => {
                x.wse.ChangeLog = x.score.ToString();
                return x.wse;
            });

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WSESearchParam param, [FromQuery] Pagination pagination)
        => GetBaseWses(param).Paginate(pagination);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WSESearchParam param) => GetBaseWses(param).LongCount();

    private IEnumerable<User> GetBaseUsers(string query)
        => _context.Users
            .AsEnumerable()
            .Select(x => (user: x, score: FuzzingService.CalculateScore(query, x)))
            .OrderByDescending(x => x.score)
            .TakeWhile(x => x.score > 0.5)
            .Select(x => x.user);

    [HttpGet("user")]
    public IEnumerable<User> GetUserList(string query, [FromQuery] Pagination pagination)
        => GetBaseUsers(query).Paginate(pagination);

    [HttpGet("user/count")]
    public long GetUserCount(string query) => GetBaseUsers(query).LongCount();
}
