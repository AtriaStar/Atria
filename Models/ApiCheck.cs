using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Models; 

public class ApiCheck {
    public const HttpStatusCode ClientSideFailure = (HttpStatusCode) 1023;
    public const HttpStatusCode InvalidWseConfiguration = (HttpStatusCode) 1024;

    [Key]
    public DateTimeOffset CheckedAt { get; set; } = DateTimeOffset.UtcNow;

    public HttpStatusCode Status { get; set; }
}
