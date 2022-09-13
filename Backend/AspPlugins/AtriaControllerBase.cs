using Microsoft.AspNetCore.Mvc;

namespace Backend.AspPlugins; 

public class AtriaControllerBase : ControllerBase {
    [Obsolete("Use Forbidden", true)]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    public override ForbidResult Forbid() => throw new NotImplementedException();
    [Obsolete("Use Forbidden", true)]
    public override ForbidResult Forbid(params string[] _) => throw new NotImplementedException();
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    public ActionResult Forbidden() => StatusCode(403);
    public ActionResult Forbidden(object? obj) => StatusCode(403, obj);
}
