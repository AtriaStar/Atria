using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("")]
public class WseDraftController : ControllerBase {

    [HttpGet("user/{userId}/draft/{draftId}")]
    public WseDraft Get(int draftId) => null!;

    [HttpPost("user/{userId}/draft/{draftId}")]
    public void Edit(WseDraft wseDraft) { }

    [HttpPost("user/{userId}/draft/{draftId}/publish")]
    public void Publish(int draftId) { }

    [HttpPut("user/draft")]
    public int Create(WseDraft wseDraft) { return 0; }

    [HttpDelete("user/{userId}/draft/{draftId}")]
    public void Delete(int draftId) { }
}
