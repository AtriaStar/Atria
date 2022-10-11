using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;
    public long UseCount => WebserviceEntries.Count;
    [JsonIgnore]
    public virtual IList<WebserviceEntry> WebserviceEntries { get; set; } = new List<WebserviceEntry>();
}
