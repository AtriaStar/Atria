namespace DatabaseMocker; 

public static class Extensions {
    public static T RandomElement<T>(this IEnumerable<T> enumerable) {
        var list = enumerable.ToArray();
        return list[Random.Shared.Next(list.Length)];
    }
}
