using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("")]
public class WSEDraftController : ControllerBase {

    [HttpGet("user/{userId}/draft/{draftId}")]
    public WSEDraft Get(int draftId) => null!;

    [HttpPost("user/{userId}/draft/{draftId}")]
    public void Edit(WSEDraft wseDraft) { }

    [HttpPost("user/{userId}/draft/{draftId}/publish")]
    public void Publish(int draftId) { }

    [HttpPut("user/draft")]
    public int Create(WSEDraft wseDraft) { return 0; }

    [HttpDelete("user/{userId}/draft/{draftId}")]
    public void Delete(int draftId) { }
}
