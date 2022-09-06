using Backend.AspPlugins;
using Backend.Authentication;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("tag")]
public class TagController : ControllerBase {
    private readonly AtriaContext _context;

    public TagController(AtriaContext context) {
        _context = context;
    }

    [HttpGet]
    public IQueryable<Tag> GetAll([FromQuery] Pagination pagination)
        => _context.Tags.Paginate(pagination);

    [HttpPut]
    [RequiresAuthentication]
    public async Task Create([FromBody] string tagName) {
        await _context.Tags.AddAsync(new() { Name = tagName });
        await _context.SaveChangesAsync();
    }

    [RequiresAuthentication]
    [RequiresUserRights(UserRights.ModerateTags)]
    [HttpPost("merge/{newTagName}/{oldTagName}")]
    public async Task<IActionResult> Merge(string newTagName, string oldTagName,
        [FromAuthentication] User _) {
        var newTag = await _context.Tags.FindAsync(newTagName);
        if (newTag == null) { return NotFound(nameof(newTagName)); }
        var oldTag = await _context.Tags.FindAsync(oldTagName);
        if (oldTag == null) { return NotFound(nameof(oldTagName)); }
        var affectedWse = _context.WebserviceEntries
            .Where(x => x.Tags.Contains(oldTag));
        foreach (var wse in affectedWse) {
            wse.Tags.Remove(oldTag);
            wse.Tags.Add(newTag);
        }
        _context.Tags.Remove(oldTag);
        _context.WebserviceEntries.UpdateRange(affectedWse);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [RequiresUserRights(UserRights.ModerateTags)]
    [HttpPost("set-description/{tagName}")]
    public async Task<IActionResult> SetDescription(string tagName, string description,
        [FromAuthentication] User _) {
        // TODO: [FromDatabase] with non-long keys.
        var tag = await _context.Tags.FindAsync(tagName);
        if (tag == null) { return NotFound(); }
        tag.Description = description;
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication]
    [RequiresUserRights(UserRights.ModerateTags)]
    [HttpDelete("{tagName}")]
    public async Task<IActionResult> Delete(string tagName, [FromAuthentication] User _) {
        var tag = await _context.Tags.FindAsync(tagName);
        if (tag == null) { return NotFound(); }
        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
