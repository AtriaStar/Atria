using System.ComponentModel.DataAnnotations;

namespace Models;

public class ApiCheck
{
    [Key]
    public DateTimeOffset CheckedAt { get; set; } = DateTimeOffset.UtcNow;

    public bool Success { get; set; }
}