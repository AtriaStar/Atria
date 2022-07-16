using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("/tag")]
public class TagController : ControllerBase {

    [HttpGet]
    public IReadOnlyList<Tag> GetAll() => null!;

    [HttpPut]
    public void Create(string tagName) { }
}
