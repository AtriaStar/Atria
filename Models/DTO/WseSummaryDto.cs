namespace Models.DTO; 

public class WseSummaryDto {
    
    public long Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsOnline { get; set; }
    public double AverageRating { get; set; }
    public int ViewCount { get; set; }
    public Uri Link { get; set; }
    public bool IsBookmark { get; set; } = false;

}