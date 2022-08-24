namespace Models;

public record WSESearchParam(
    string? Query,
    bool? IsOnline,
    bool? HasBookmark,
    ISet<Tag> Tags,
    StarCount MinReviewAvg,
    Order Order
);
