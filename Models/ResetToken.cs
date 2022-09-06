using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models; 

public class ResetToken {
    [Key]
    public byte[] Token { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    [JsonIgnore]
    public User User { get; set; } = null!;
}
