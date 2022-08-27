﻿using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Models;

namespace Backend; 

public static class Extensions {
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> queryable, Pagination pagination)
        => queryable.Skip(pagination.Page * pagination.EntriesPerPage)
                    .Take(pagination.EntriesPerPage);

    public static Func<WebserviceEntry, double> GetMapper(this Order order)
        => order switch {
            Order.Relevance => x =>
                (double)x.ViewCount / (DateTimeOffset.UtcNow - x.CreatedAt).Ticks
                * x.Reviews.Average(y => (int)y.StarCount),
            Order.ViewCount => x => x.ViewCount,
            Order.ReviewAverage => x => x.Reviews.Average(y => (int)y.StarCount),
            Order.Recency => x => x.CreatedAt.UtcTicks,
            _ => throw new InvalidEnumArgumentException(nameof(order), (int)order, typeof(Order)),
        };


    public static IEnumerable<ParameterInfo> GetBasicParameters(this ActionExecutingContext context)
        => (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo.GetParameters()
            .Where(x => x.Name != null)
            ?? Enumerable.Empty<ParameterInfo>();

    public static IEnumerable<ParameterInfo> GetParametersWithAttribute<T>(this ActionExecutingContext context)
        where T : Attribute
        => context.GetBasicParameters().Where(x => x.GetCustomAttributes(typeof(T)).Any());


    public static void UseCentralRoutePrefix(this MvcOptions opt, IRouteTemplateProvider routeAttribute) {
        opt.Conventions.Insert(0, new RouteConvention(routeAttribute));
    }
}
