namespace Models.DTO; 

public class WseSummaryDto {
    
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
    public bool IsOnline { get; set; }
    public double AverageRating { get; set; }
    public int ViewCount { get; set; }
    public Uri Link { get; set; } = null!;
    public bool IsBookmark { get; set; }

}