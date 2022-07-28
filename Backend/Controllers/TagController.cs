using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("tag")]
public class TagController : ControllerBase {

    [HttpGet]
    public IReadOnlyList<Tag> GetAll() => null!;

    [HttpPut]
    public void Create(string tagName) { }
}
