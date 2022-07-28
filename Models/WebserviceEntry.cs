namespace Models; 

public class WebserviceEntry {
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public Uri Link { get; set; }
    public string FullDescription { get; set; }
    public Uri DocumentationLink { get; set; }
    public string Changelog { get; set; }
    public int ViewCount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public User ContactPerson { get; set; }
    public Question[] Questions { get; set; }
    public Tag[] Tags { get; set; }
    public Review[] Reviews { get; set; }
    public Dictionary<User, WSERole> Collaborators { get; set; }
}
