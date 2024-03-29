﻿namespace Models;

public record WseSearchParameters(
    string? Query = null,
    bool? IsOnline = null,
    bool? HasBookmark = null,
    string[]? Tags = null,
    StarCount MinReviewAvg = StarCount.One,
    Order Order = Order.Relevance,
    bool Ascending = false
);
