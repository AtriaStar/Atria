namespace Models;

public record WseSearchParam(
    string Query,
    bool IsOnline,
    bool HasBookmark,
    ICollection<Tag> Tags,
    StarCount MinReviewAvg,
    Order Order
);
