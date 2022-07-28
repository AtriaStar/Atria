using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("notification")]
public class NotificationController : ControllerBase {

    [HttpPost("")]
    public void SetAllowedNotification(Notification notification, bool state) { }

    [HttpPost("email")]
    public void SetEmailNotification(bool state) { }
    
}
