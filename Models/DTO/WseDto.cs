using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class WseDto
{
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
    public string? ChangeLog { get; set; } = null!;


    [Url]
    public string? DocumentationLink { get; set; } = null!;

    public string? Documentation { get; set; } = null!;

    public long? ContactPersonId { get; set; }
    public User? ContactPerson { get; set; } = null!;

    [MaxLength(20)]
    public ICollection<Tag>? Tags { get; set; } = null!;

    public ICollection<Collaborator>? Collaborators { get; set; } = null!;

}