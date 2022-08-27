using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models; 

public class WebserviceEntry {
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
    public string? FullDescription { get; set; } = null!;

    [Url]
    public string? DocumentationLink { get; set; } = null!;

    public string? Documentation { get; set; } = null!;
    public string? ChangeLog { get; set; } = null!;

    public int ViewCount { get; set; }

    public long ContactPersonId { get; set; }
    public User ContactPerson { get; set; } = null!;

    [MaxLength(20)]
    public ICollection<Tag> Tags { get; set; } = null!;

    public ICollection<Question> Questions { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = null!;

    [MaxLength(20)]
    public ICollection<Collaborator> Collaborators { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
