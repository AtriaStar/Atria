using System.ComponentModel.DataAnnotations;

namespace Models; 

public class WebserviceEntry {
    [Key]
    public string Name { get; init; }
    public string ShortDescription { get; init; }
    public Uri Link { get; init; }
    public string FullDescription { get; init; }
    public Uri DocumentationLink { get; init; }
    public string Changelog { get; init; }
    public int ViewCount { get; init; }
    public DateTimeOffset CreationDate { get; init; }
    public User ContactPerson { get; init; }
    public Question[] Questions { get; init; }
    public Tag[] Tags { get; init; }
    public Review[] Reviews { get; init; }
    //public Dictionary<User, WSERole> Collaborators { get; init; }
}
