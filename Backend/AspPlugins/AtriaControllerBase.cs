using Microsoft.AspNetCore.Mvc;

namespace Backend.AspPlugins; 

public class AtriaControllerBase : ControllerBase {
    [Obsolete("Use Forbidden", true)]
    public override ForbidResult Forbid() => throw new NotImplementedException();
    [Obsolete("Use Forbidden", true)]
    public override ForbidResult Forbid(params string[] _) => throw new NotImplementedException();

    public ActionResult Forbidden() => StatusCode(403);
    public ActionResult Forbidden(object? obj) => StatusCode(403, obj);
}
