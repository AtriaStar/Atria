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
    private string _email = null!;
    private string? _profilePicture;

    public string Email {
        get => _email;
        set => _email = value.ToLowerInvariant();
    }

    [Url]
    public string? ProfilePicture {
        get => _profilePicture;
        set => _profilePicture = string.IsNullOrEmpty(value) ? null : value;
    }

    public string? Biography { get; set; }
    public string SignUpIp { get; set; } = null!;

    [JsonIgnore]
    public virtual byte[] PasswordHash { get; set; } = null!;

    [JsonIgnore]
    public virtual byte[] PasswordSalt { get; set; } = null!;

    public UserRights Rights { get; set; } = UserRights.Default;

    [JsonIgnore]
    public virtual ISet<WebserviceEntry> Bookmarks { get; set; } = new HashSet<WebserviceEntry>();

    [JsonIgnore]
    public virtual ICollection<WseDraft> WseDrafts { get; set; } = new List<WseDraft>();

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
