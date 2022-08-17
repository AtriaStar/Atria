using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models; 

public class Session {
    [Key, JsonIgnore]
    public string Token { get; set; } = null!;
    [JsonIgnore]
    public User User { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public string Ip { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
