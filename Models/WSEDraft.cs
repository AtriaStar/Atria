using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class WSEDraft {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    [Url]
    public string Link { get; set; } = null!;
    public string FullDescription { get; set; } = null!;
    [Url]
    public string? DocumentationLink { get; set; } = null!;
    public string? Documentation { get; set; } = null!;
    public string ChangeLog { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
