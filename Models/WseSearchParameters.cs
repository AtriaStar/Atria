namespace Models;

public record WseSearchParam(
    string Query,
    bool IsOnline,
    bool HasBookmark,
    List<Tag> Tags,
    StarCount MinReviewAvg,
    Order Order
);
