using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("search")]
public class SearchController : ControllerBase {
    private readonly AtriaContext _context;

    public SearchController(AtriaContext context) {
        _context = context;
    }

    private IQueryable<WebserviceEntry> GetBaseWses(WSESearchParam param)
        => _context.WebserviceEntries
            .Where(x => x.Reviews.Average(y => (int) y.StarCount) >= (int) param.MinReviewAvg);

    [HttpGet("wse")]
    public IQueryable<WebserviceEntry> GetWseList([FromQuery] WSESearchParam param, [FromQuery] Pagination pagination)
        => GetBaseWses(param)
            .OrderBy(x => param.Order.GetMapper().Invoke(x)
                          * (param.Query == null ? 1 : FuzzingService.CalculateScore(param.Query, x)))
            .Paginate(pagination);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WSESearchParam param) => GetBaseWses(param).LongCount();

    private IQueryable<User> GetBaseUsers(string query)
        => _context.Users
            .Select(x => new { user = x, score = FuzzingService.CalculateScore(query, x) })
            .OrderBy(x => x.score)
            .TakeWhile(x => x.score > 0.5)
            .Select(x => x.user);

    [HttpGet("user")]
    public IQueryable<User> GetUserList(string query, [FromQuery] Pagination pagination)
        => GetBaseUsers(query).Paginate(pagination);

    [HttpGet("user/count")]
    public Task<long> GetUserCount(string query) => GetBaseUsers(query).LongCountAsync();
}
