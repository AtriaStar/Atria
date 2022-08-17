using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Index(nameof(Email), IsUnique = true)]
public class User {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string? Title { get; set; }
    public string FirstNames { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Uri? ProfilePicture { get; set; }
    public string? Biography { get; set; }
    public string SignUpIp { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = null!;
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = null!;
    public UserRights Rights { get; set; } = UserRights.Default;
    public ICollection<WebserviceEntry> Bookmarks { get; set; } = Array.Empty<WebserviceEntry>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
