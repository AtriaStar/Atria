using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

// TODO: Notification ist abstract with getBaseMessage()
public class Notification {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string baseMessage { get; set; } = null!;
    public DateTimeOffset creationTime { get; set; }
}
