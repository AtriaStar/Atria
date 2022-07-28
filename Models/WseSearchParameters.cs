namespace Models;

public class WseSearchParam {
    public string Query { get; set; }
    public bool IsOnline { get; set; }
    public bool HasBookmark { get; set; }
    public List<Tag> Tags { get; set; }
    public StarCount MinReviewAvg { get; set; }
    public Order Order { get; set; }
}
