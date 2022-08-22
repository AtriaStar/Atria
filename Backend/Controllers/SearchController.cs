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

    private IEnumerable<WebserviceEntry> GetWseBaseEnumerable(WSESearchParam param)
        => _context.WebserviceEntries
            .Where(x => x.Reviews.Average(y => (int) y.StarCount) >= (int) param.MinReviewAvg);

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WSESearchParam param, [FromQuery] Pagination pagination) =>
        GetWseBaseEnumerable(param)
            .OrderBy(x => param.Order.GetMapper().Invoke(x) * FuzzingService.CalculateScore(param.Query, x))
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WSESearchParam param) => GetWseBaseEnumerable(param).LongCount();

    [HttpGet("user")]
    public IEnumerable<User> GetUserList(string? query, [FromQuery] Pagination pagination)
        => _context.Users
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    [HttpGet("user/count")]
    public Task<long> GetUserCount(string? query) => _context.WebserviceEntries.LongCountAsync();
}
