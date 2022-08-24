using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models; 

public class WebserviceEntry {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public Uri Link { get; set; } = null!;
    public string? FullDescription { get; set; }
    public Uri? DocumentationLink { get; set; }
    public string? Changelog { get; set; }
    public int ViewCount { get; set; }
    public long ContactPersonId { get; set; }
    [JsonIgnore]
    public virtual User ContactPerson { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
