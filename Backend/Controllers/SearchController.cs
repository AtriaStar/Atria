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

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WSESearchParam param, [FromQuery] Pagination pagination)
        => _context.WebserviceEntries
            .Where(x => x.Reviews.Average(y => (int)y.StarCount) >= (int)param.MinReviewAvg)
            .OrderBy(x => x, param.Order.GetComparer())
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    [HttpGet("user")]
    public IEnumerable<User> GetUserList(string query, [FromQuery] Pagination pagination)
        => _context.Users
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    [HttpGet("count/wse")]
    public Task<long> GetWseCount(string query) => _context.WebserviceEntries.LongCountAsync();

    [HttpGet("count/user")]
    public Task<long> GetUserCount(string query) => _context.WebserviceEntries.LongCountAsync();
}
