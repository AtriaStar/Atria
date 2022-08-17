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
    public int GetWseCount(string query) => _context.WebserviceEntries.Count();

    [HttpGet("count/user")]
    public int GetUserCount(string query) => _context.WebserviceEntries.Count();
}
