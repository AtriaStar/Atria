using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Models;

namespace Backend; 

public static class Extensions {
    public static T RandomElement<T>(this IEnumerable<T> enumerable)
        => enumerable.ElementAt(Random.Shared.Next(enumerable.Count()));

    // TODO: Implement this and add lazy generators to the others
    private static readonly IComparer<WebserviceEntry> RelevanceComparer = Comparer<WebserviceEntry>.Create(
        (_, _) => Guid.NewGuid().GetHashCode()
    );

    private static readonly IComparer<WebserviceEntry> ViewCountComparer
        = ComparerFromSelector<WebserviceEntry, int>(x => x.ViewCount);

    private static readonly IComparer<WebserviceEntry> ReviewAverageComparer
        = ComparerFromSelector<WebserviceEntry, double>(x => x.Reviews.Average(y => (byte)y.StarCount));

    private static readonly IComparer<WebserviceEntry> RecencyComparer
        = ComparerFromSelector<WebserviceEntry, DateTimeOffset>(x => x.CreatedAt);

    private static IComparer<T> ComparerFromSelector<T, TSel>(Func<T, TSel> selector)
        where TSel : IComparable
        => Comparer<T>.Create((x, y) => selector(x).CompareTo(selector(y)));

    public static IComparer<WebserviceEntry> GetComparer(this Order order)
        => order switch {
            Order.Relevance => RelevanceComparer,
            Order.ViewCount => ViewCountComparer,
            Order.ReviewAverage => ReviewAverageComparer,
            Order.Recency => RecencyComparer,
            _ => throw new InvalidEnumArgumentException(nameof(order), (int)order, typeof(Order)),
        };

    public static IEnumerable<ParameterInfo> GetParametersWithAttribute<T>(this ActionExecutingContext context)
        => (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo.GetParameters()
            .Where(x => x.Name != null && x.CustomAttributes
                .Any(y => y.AttributeType == typeof(T)))
            ?? Enumerable.Empty<ParameterInfo>();

    public static void UseCentralRoutePrefix(this MvcOptions opt, IRouteTemplateProvider routeAttribute) {
        opt.Conventions.Insert(0, new RouteConvention(routeAttribute));
    }
}
