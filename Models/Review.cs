using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models;

public class Review {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Key]
    public long WseId { get; set; }
    public WebserviceEntry Wse { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public StarCount StarCount { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public long CreatorId { get; set; }
    [JsonIgnore]
    public virtual User Creator { get; set; } = null!;
}
