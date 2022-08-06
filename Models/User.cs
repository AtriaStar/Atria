using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models;

public class User {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string Title { get; set; } = null!;
    public string FirstNames { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Uri ProfilePicture { get; set; } = null!;
    public string Biography { get; set; } = null!;
    [JsonIgnore]
    public string PasswordHash { get; set; } = null!;
    public ICollection<WebserviceEntry> Bookmarks { get; set; } = null!;
}
