using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models; 

public class WebserviceEntry {
    private string? _documentationLink;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    [Url]
    public string Link { get; set; } = null!;
    public string? FullDescription { get; set; }

    [Url]
    public string? DocumentationLink {
        get => _documentationLink;
        set => _documentationLink = string.IsNullOrEmpty(value) ? null : value;
    }

    public string? Documentation { get; set; }
    public string? ChangeLog { get; set; }

    public long ViewCount { get; set; }

    public long ContactPersonId { get; set; }
    [JsonIgnore]
    public virtual User ContactPerson { get; set; } = null!;
    
    [JsonIgnore]
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    
    [MaxLength(20)]
    public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();
    
    [JsonIgnore]
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
