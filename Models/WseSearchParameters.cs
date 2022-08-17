namespace Models;

public record WSESearchParam(
    string Query,
    bool IsOnline,
    bool HasBookmark,
    ICollection<Tag> Tags,
    StarCount MinReviewAvg,
    Order Order
);
