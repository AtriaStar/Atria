using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class WSEDraft {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? ShortDescription { get; set; }
    [Url]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string? Link { get; set; }
    public string? FullDescription { get; set; }

    [Url]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string? DocumentationLink { get; set; }
    public string? Documentation { get; set; }
    public string? ChangeLog { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
