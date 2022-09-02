using System.ComponentModel.DataAnnotations;

namespace Models;

public class WseRegistration
{
    public WseRegistration()
        {
            Tags = Array.Empty<string>();
        }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string ShortDescription { get; set; } = null!;

    [MaxLength(20)]
    public string[] Tags { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9-.]*(,[a-zA-Z0-9-.]+)*$", ErrorMessage = "Tags are not separated with a comma.")]
    public string NewTags { get; set; } = null!;

    [Required]
    [Url]
    public string Link { get; set; } = null!;
    
    public string Documentation { get; set; } = null!;

    [Url]
    public string DocumentationLink { get; set; } = null!;

    public string FullDescription { get; set; } = null!;

    public string ChangeLog { get; set; } = null!;
}