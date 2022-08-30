using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class WseDraft {
    private string? _documentationLink;
    private string? _link;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? ShortDescription { get; set; }

    [Url]
    public string? Link {
        get => _link;
        set => _link = string.IsNullOrEmpty(value) ? null : value;
    }

    public string? FullDescription { get; set; }

    [Url]
    public string? DocumentationLink {
        get => _documentationLink;
        set => _documentationLink = string.IsNullOrEmpty(value) ? null : value;
    }

    public string? Documentation { get; set; }
    public string? ChangeLog { get; set; }

    [MaxLength(20)]
    public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
