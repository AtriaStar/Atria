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
/*  TEMPORARY REMOVE FOR TESTING PURPOSES (this implementation will be overwritten anyway)
    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WSESearchParam param, [FromQuery] Pagination pagination)
        => _context.WebserviceEntries
            .Where(x => x.Reviews.Average(y => (int)y.StarCount) >= (int)param.MinReviewAvg)
            .OrderBy(x => x, param.Order.GetComparer())
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);
*/
    [HttpGet("wse")]
    public List<WseSummaryDto> GetWseList() {
        var list = _context.WebserviceEntries.Take(10);
        List<WseSummaryDto> returnList = new List<WseSummaryDto>();
        foreach (var webservice in list) {
            returnList.Add(new WseSummaryDto() {
                Id = webservice.Id,
                Link = new Uri(webservice.Link),
                Name = webservice.Name,
                Tags = new List<Tag>(),
                AverageRating = 3,
                CreationDate = webservice.CreatedAt,
                IsBookmark = true,
                IsOnline = false,
                ShortDescription = webservice.ShortDescription,
                ViewCount = webservice.ViewCount
            });
        }

        return returnList;
    }
    
    [HttpGet("user")]
    public IEnumerable<User> GetUserList(string? query, [FromQuery] Pagination pagination)
        => _context.Users
            .Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    [HttpGet("count/wse")]
    public Task<long> GetWseCount(string? query) => _context.WebserviceEntries.LongCountAsync();

    [HttpGet("count/user")]
    public Task<long> GetUserCount(string? query) => _context.WebserviceEntries.LongCountAsync();
}
