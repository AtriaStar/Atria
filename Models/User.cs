using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class User {
    public string Title { get; init; }
    public string FirstNames { get; init; }
    public string LastName { get; init; }
    [Key]
    public string Email { get; init; }
    public Uri ProfilePicture { get; init; }
    public string Biography { get; init; }
    [JsonIgnore]
    public string PasswordHash { get; init; }
    public WebserviceEntry[] Bookmarks { get; init; }
}
