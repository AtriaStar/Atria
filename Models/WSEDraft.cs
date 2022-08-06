using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class WSEDraft {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public Uri Link { get; set; } = null!;
    public string FullDescription { get; set; } = null!;
    public Uri DocumentationLink { get; set; } = null!;
    public string Changelog { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
}
