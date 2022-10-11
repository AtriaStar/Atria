using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;

    public override bool Equals(object? obj)
    {
        return Equals(obj as Tag);
    }

    private bool Equals(Tag? other) {
        return other != null &&
               Name.Equals(other.Name, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}
