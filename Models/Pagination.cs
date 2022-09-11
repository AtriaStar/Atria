namespace Models; 

public class Pagination {
    public int Page { get; set; }
    public int EntriesPerPage { get; set; } = 20;
}

public static class PaginationExtensions {
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, Pagination pagination)
        => queryable.Skip(Math.Max(0, pagination.Page - 1) * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> queryable, Pagination pagination)
        => queryable.Skip(Math.Max(0, pagination.Page - 1) * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);
}
