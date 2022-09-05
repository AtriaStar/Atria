using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Models;
public class Collaborator
{
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(Wse))]
    public long WseId { get; set; }
    [JsonIgnore]
    public WebserviceEntry Wse { get; set; } = null!;

    public WseRights Rights { get; set; } = WseRights.Default;

    public override bool Equals(object? obj)
        => obj is Collaborator other && UserId == other.UserId && WseId == other.WseId && Rights == other.Rights;

    public override int GetHashCode()
        => HashCode.Combine(UserId, WseId, Rights);
}