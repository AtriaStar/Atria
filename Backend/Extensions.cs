namespace Backend; 

public static class Extensions {
    public static T RandomElement<T>(this IEnumerable<T> enumerable)
        => enumerable.ElementAt(Random.Shared.Next(enumerable.Count()));
}
