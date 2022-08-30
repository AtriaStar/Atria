namespace Models;

public record WseSearchParameters(
    string? Query,
    bool? IsOnline,
    bool? HasBookmark,
    HashSet<Tag>? Tags,
    StarCount MinReviewAvg,
    Order Order,
    bool Ascending
);
