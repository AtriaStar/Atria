using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.ParameterHelpers;

/// <summary>
/// Causes the specified foreign key property to be loaded for the associated parameter marked with <see cref="FromDatabaseAttribute"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct,
    AllowMultiple = true)]
public class IncludeAttribute : Attribute {
    public string Name { get; }

    public IncludeAttribute(string name) {
        Name = name;
    }
}

public static class IncludeAttributeExtensions {
    public static async Task ApplyToAsync(this IEnumerable<IncludeAttribute> includes, DbContext db, object obj) {
        var entry = new Lazy<EntityEntry>(() => db.Entry(obj));
        foreach (var include in includes) {
            await entry.Value.IncludeAsync(include.Name);
        }
    }

    public static Task IncludeAsync(this EntityEntry entry, string name)
        => entry.Entity.GetType().GetProperty(name)!.PropertyType.GetInterface(nameof(IEnumerable)) == null
            ? entry.Reference(name).LoadAsync() : entry.Collection(name).LoadAsync();
}
