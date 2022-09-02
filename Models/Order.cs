using System.ComponentModel;

namespace Models;

public enum Order : byte {
    Relevance,
    ViewCount,
    ReviewAverage,
    Recency,
}

public static class OrderExtensions {
    public static Func<WebserviceEntry, double> GetMapper(this Order order)
        => order switch {
            Order.Relevance => x =>
                (double)x.ViewCount / (DateTimeOffset.UtcNow - x.CreatedAt).Ticks
                * (x.Reviews.Any() ? x.Reviews.Average(y => (int)y.StarCount) : 2.5),
            Order.ViewCount => x => x.ViewCount,
            Order.ReviewAverage => x => x.Reviews.Any() ? x.Reviews.Average(y => (int)y.StarCount) : 2.5,
            Order.Recency => x => x.CreatedAt.UtcTicks,
            _ => throw new InvalidEnumArgumentException(nameof(order), (int)order, typeof(Order)),
        };
}
