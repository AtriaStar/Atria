using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;
    public long UseCount { get; set; }
}
