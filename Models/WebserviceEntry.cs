using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models; 

public class WebserviceEntry {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    [Url]
    public string Link { get; set; } = null!;
    public string FullDescription { get; set; } = null!;

    [Url]
    public string DocumentationLink { get; set; } = null!;

    public string Documentation { get; set; } = null!;
    public string ChangeLog { get; set; } = null!;
    public int ViewCount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public User ContactPerson { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = null!;

    [MaxLength(20)]
    public ICollection<Tag> Tags { get; set; } = null!;

    [RegularExpression(@"^[a-zA-Z0-9-.]*(,[a-zA-Z0-9-.]+)*$", ErrorMessage = "Tags are not separated with a comma.")]
    public string NewTags { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = null!;
    public ICollection<Collaborator> Collaborators { get; set; } = null!;
}
