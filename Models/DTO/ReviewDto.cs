namespace Models.DTO;

public class ReviewDto {

    public int WseId { get; }
    public string Title { get; }
    public string? Description { get; }
    public StarCount StarCount { get; }
    public DateTimeOffset CreationTime { get; }

    public ReviewDto(int wseId, string title, string? description, StarCount starCount, DateTimeOffset creationTime) {
        WseId = wseId;
        Title = title;
        Description = description;
        StarCount = starCount;
        CreationTime = creationTime;
    }
}