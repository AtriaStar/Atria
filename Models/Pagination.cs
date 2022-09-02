namespace Models; 

public class Pagination {
    public int Page { get; set; }
    public int EntriesPerPage { get; set; } = 20;
}

public static class PaginationExtensions {
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> queryable, Pagination pagination)
        => queryable.Skip(pagination.Page * pagination.EntriesPerPage)
            .Take(pagination.EntriesPerPage);
}
