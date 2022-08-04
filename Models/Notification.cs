using System.ComponentModel.DataAnnotations;

namespace Models;

// TODO: Notification ist abstract with getBaseMessage()
public class Notification {
    [Key]
    public ulong Id { get; set; }
    public string baseMessage { get; init; }
    public DateTimeOffset creationTime { get; init; }
}
