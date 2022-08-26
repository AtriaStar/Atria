using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models; 

public class Question {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Key]
    public long WseId { get; set; }
    [JsonIgnore]
    public WebserviceEntry Wse { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreationTime { get; set; }
    public long CreatorId { get; set; }
    [JsonIgnore]
    public virtual User Creator { get; set; } = null!;
}
