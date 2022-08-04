using System.ComponentModel.DataAnnotations;

namespace Models;

public class WSEDraft {
    [Key]
    public ulong Snowflake { get; set; }
    public string Name { get; init; }
    public string ShortDescription { get; init; }
    public Uri Link { get; init; }
    public string FullDescription { get; init; }
    public Uri DocumentationLink { get; init; }
    public string Changelog { get; init; }
    public DateTimeOffset CreationDate { get; init; }
}
