namespace Models;

public class WSEDraft {
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public Uri Link { get; set; }
    public string FullDescription { get; set; }
    public Uri DocumentationLink { get; set; }
    public string Changelog { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}

