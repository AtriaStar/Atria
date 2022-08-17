namespace Models.DTO; 

public class WseSummaryDto {
    
    public string Name { get; }
    public string ShortDescription { get; }
    public ICollection<Tag> Tags { get; }
    public DateTimeOffset CreationDate { get; }
    public bool IsOnline { get; }
    public double AverageRating { get; }
    public int ViewCount { get; }
    public Uri Link { get; }

    public WseSummaryDto(string name, string shortDescription, ICollection<Tag> tags, DateTimeOffset creationDate, bool isOnline, double averageRating, int viewCount, Uri link) {
        Name = name;
        ShortDescription = shortDescription;
        Tags = tags;
        CreationDate = creationDate;
        IsOnline = isOnline;
        AverageRating = averageRating;
        ViewCount = viewCount;
        Link = link;
    }
}