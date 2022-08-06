using System.ComponentModel.DataAnnotations;

namespace Models;

// TODO: Notification ist abstract with getBaseMessage()
public class Notification {
    [Key]
    public ulong Id { get; set; }
    public string baseMessage { get; set; } = null!;
    public DateTimeOffset creationTime { get; set; }
}
